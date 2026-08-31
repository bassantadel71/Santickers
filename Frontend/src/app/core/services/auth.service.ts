import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { AuthResponse, LoginRequest } from '../models/auth.model';
import { environment } from '../../../environments/environment';

const TOKEN_KEY = 'santickers.authToken';
const USER_KEY = 'santickers.authUser';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/Auth`;

  private readonly _token = signal<string | null>(this.loadToken());
  private readonly _user = signal<string | null>(this.loadUser());
  private readonly _isAuthenticated = signal<boolean>(!!this._token());

  readonly token = this._token.asReadonly();
  readonly user = this._user.asReadonly();
  readonly isAuthenticated = this._isAuthenticated.asReadonly();

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap((res) => this.setSession(res))
    );
  }

  logout(): void {
    this._token.set(null);
    this._user.set(null);
    this._isAuthenticated.set(false);
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
  }

  private setSession(res: AuthResponse): void {
    this._token.set(res.token);
    this._user.set(res.email);
    this._isAuthenticated.set(true);
    localStorage.setItem(TOKEN_KEY, res.token);
    if (res.email) localStorage.setItem(USER_KEY, res.email);
  }

  private loadToken(): string | null {
    try {
      return localStorage.getItem(TOKEN_KEY);
    } catch {
      return null;
    }
  }

  private loadUser(): string | null {
    try {
      return localStorage.getItem(USER_KEY);
    } catch {
      return null;
    }
  }
}
