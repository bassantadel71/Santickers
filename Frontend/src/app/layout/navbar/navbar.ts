import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LucideAngularModule, Heart, Menu, Search, ShoppingBag, User, X } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { FavoritesService } from '../../core/services/favorites.service';
import { Wordmark } from '../../shared/wordmark/wordmark';

const LINKS = [
  { to: '/', label: 'Home' },
  { to: '/stickers', label: 'Stickers' },
  { to: '/customize', label: 'Customize' },
  { to: '/about', label: 'About' },
];

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, LucideAngularModule, Wordmark],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  readonly links = LINKS;
  readonly open = signal(false);
  readonly HeartIcon = Heart;
  readonly MenuIcon = Menu;
  readonly SearchIcon = Search;
  readonly ShoppingBagIcon = ShoppingBag;
  readonly UserIcon = User;
  readonly XIcon = X;

  constructor(public cart: CartService, public favs: FavoritesService) {}

  toggleMenu(): void { this.open.update((v) => !v); }
  closeMenu(): void { this.open.set(false); }
}
