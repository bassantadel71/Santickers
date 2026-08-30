import { Injectable, signal } from '@angular/core';

export type ToastKind = 'error' | 'info';

export interface ToastMessage {
  id: number;
  message: string;
  kind: ToastKind;
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private readonly _toasts = signal<ToastMessage[]>([]);

  readonly toasts = this._toasts.asReadonly();

  private nextId = 1;

  show(message: string, kind: ToastKind = 'error'): void {
    const id = this.nextId++;
    this._toasts.update((prev) => [...prev, { id, message, kind }]);
    setTimeout(() => this.dismiss(id), 4000);
  }

  dismiss(id: number): void {
    this._toasts.update((prev) => prev.filter((t) => t.id !== id));
  }
}