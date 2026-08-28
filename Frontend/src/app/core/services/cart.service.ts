import { Injectable, computed, signal } from '@angular/core';
import { CartItem } from '../models/cart-item.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  private readonly _cart = signal<CartItem[]>([]);
  private readonly _cartOpen = signal(false);

  readonly cart = this._cart.asReadonly();
  readonly cartOpen = this._cartOpen.asReadonly();

  readonly cartCount = computed(() =>
    this._cart().reduce((n, i) => n + i.qty, 0)
  );
  readonly subtotal = computed(() =>
    this._cart().reduce((n, i) => n + i.qty * i.price, 0)
  );

  addToCart(item: Omit<CartItem, 'qty'>, qty = 1): void {
    this._cart.update((prev) => {
      const found = prev.find((i) => i.key === item.key);
      if (found) {
        return prev.map((i) => (i.key === item.key ? { ...i, qty: i.qty + qty } : i));
      }
      return [...prev, { ...item, qty }];
    });
    this._cartOpen.set(true);
  }

  setQty(key: string, qty: number): void {
    this._cart.update((prev) =>
      qty <= 0
        ? prev.filter((i) => i.key !== key)
        : prev.map((i) => (i.key === key ? { ...i, qty } : i))
    );
  }

  removeItem(key: string): void {
    this._cart.update((prev) => prev.filter((i) => i.key !== key));
  }

  openCart(): void { this._cartOpen.set(true); }
  closeCart(): void { this._cartOpen.set(false); }
}
