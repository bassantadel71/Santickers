import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LucideAngularModule, Minus, Plus, ShoppingBag, Trash2, X } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { AuthService } from '../../core/services/auth.service';
import { money } from '../../shared/money';

@Component({
  selector: 'app-cart-sidebar',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './cart-sidebar.html',
  styleUrl: './cart-sidebar.css',
})
export class CartSidebar {
  readonly MinusIcon = Minus;
  readonly PlusIcon = Plus;
  readonly ShoppingBagIcon = ShoppingBag;
  readonly Trash2Icon = Trash2;
  readonly XIcon = X;
  readonly money = money;

  constructor(public cart: CartService, private auth: AuthService, private router: Router) {}

  checkout(): void {
    this.cart.closeCart();
    if (this.auth.isAuthenticated()) {
      this.router.navigate(['/checkout']);
    } else {
      this.router.navigate(['/login'], { queryParams: { returnUrl: '/checkout' } });
    }
  }
}
