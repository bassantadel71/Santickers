import { Injectable, effect, signal } from '@angular/core';

const FAV_KEY = 'santickers.favorites';

@Injectable({ providedIn: 'root' })
export class FavoritesService {
  private readonly _favorites = signal<string[]>(this.loadFromStorage());

  readonly favorites = this._favorites.asReadonly();

  constructor() {
    effect(() => {
      try {
        localStorage.setItem(FAV_KEY, JSON.stringify(this._favorites()));
      } catch { /* ignore */ }
    });
  }

  private loadFromStorage(): string[] {
    try {
      const raw = localStorage.getItem(FAV_KEY);
      return raw ? JSON.parse(raw) : [];
    } catch {
      return [];
    }
  }

  isFavorite(id: string): boolean {
    return this._favorites().includes(id);
  }

  toggleFavorite(id: string): void {
    this._favorites.update((prev) =>
      prev.includes(id) ? prev.filter((f) => f !== id) : [...prev, id]
    );
  }
}
