import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './layout/navbar/navbar';
import { Footer } from './layout/footer/footer';
import { CartSidebar } from './layout/cart-sidebar/cart-sidebar';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Footer, CartSidebar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Frontend');
}
