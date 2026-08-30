import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="mx-auto max-w-md px-4 py-24 text-center">
      <h1 class="text-7xl font-extrabold text-navy">404</h1>
      <h2 class="mt-4 text-xl font-bold text-navy">Page not found</h2>
      <p class="mt-2 text-navy/60">The page you're looking for doesn't exist or has been moved.</p>
      <a routerLink="/" class="mt-6 inline-block rounded-full bg-navy px-8 py-4 font-extrabold text-cream transition hover:bg-blue">
        Go home
      </a>
    </div>
  `,
})
export class NotFound {}