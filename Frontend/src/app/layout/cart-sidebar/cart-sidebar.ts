import { Component } from '@angular/core';
import { LucideAngularModule, Minus, Plus, ShoppingBag, Trash2, X } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
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

  constructor(public cart: CartService) {}
}
