import { Injectable, effect, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EMPTY, Observable, catchError, finalize, map, tap, throwError } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';
import { ToastService } from './toast.service';

@Injectable({ providedIn: 'root' })
export class FavoritesService {
  private readonly http = inject(HttpClient);
  private readonly auth = inject(AuthService);
  private readonly toast = inject(ToastService);

  private readonly apiUrl = `${environment.apiUrl}/Favorites`;

  private readonly _favorites = signal<string[]>([]);
  private readonly _pending = signal<string[]>([]);

  readonly favorites = this._favorites.asReadonly();

  readonly pending = this._pending.asReadonly();

  constructor() {
    effect(() => {
      if (this.auth.token()) {
        this.refresh();
      } else {
        this._favorites.set([]);
        this._pending.set([]);
      }
    });
  }

  isFavorite(id: string): boolean {
    return this._favorites().includes(id);
  }

  isPending(id: string): boolean {
    return this._pending().includes(id);
  }

  refresh(): void {
    if (!this.auth.isAuthenticated()) return;

    this.http.get<number[]>(this.apiUrl).subscribe({
      next: (ids) => this._favorites.set(ids.map(String)),
      error: () => this._favorites.set([]),
    });
  }

  toggleFavorite(id: string): Observable<void> {
    if (this.isPending(id)) return EMPTY;

    if (!this.auth.isAuthenticated()) {
      this.toast.show('Please log in to save favorites.');
      return throwError(() => new Error('Not authenticated'));
    }

    const wasFavorite = this.isFavorite(id);
    this._pending.update((prev) => [...prev, id]);

    const request = wasFavorite
      ? this.http.delete<void>(`${this.apiUrl}/${id}`)
      : this.http.post<void>(`${this.apiUrl}/${id}`, null);

    return request.pipe(
      tap(() => {
        this._favorites.update((prev) =>
          wasFavorite
            ? prev.filter((f) => f !== id)
            : prev.includes(id)
              ? prev
              : [...prev, id],
        );
      }),
      catchError(() => {
        this.toast.show(
          wasFavorite
            ? 'Could not remove favorite. Please try again.'
            : 'Could not add favorite. Please try again.',
        );
        return throwError(() => new Error('Favorite request failed'));
      }),
      finalize(() => {
        this._pending.update((prev) => prev.filter((f) => f !== id));
      }),
      map(() => undefined),
    );
  }
}