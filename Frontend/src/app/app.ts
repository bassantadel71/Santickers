import { Component, signal } from '@angular/core';
import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { Navbar } from './layout/navbar/navbar';
import { AuthNavbar } from './layout/auth-navbar/auth-navbar';
import { Footer } from './layout/footer/footer';
import { CartSidebar } from './layout/cart-sidebar/cart-sidebar';
import { Toast } from './shared/toast/toast';

const AUTH_PATHS = new Set(['/login', '/signup']);

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, AuthNavbar, Footer, CartSidebar, Toast],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Frontend');
  protected readonly isAuthPage = signal(false);

  constructor(router: Router) {
    router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        this.isAuthPage.set(AUTH_PATHS.has(new URL(e.url, location.href).pathname));
      }
    });
  }
}
