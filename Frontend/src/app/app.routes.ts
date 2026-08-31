import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/home/home').then((m) => m.Home) },
  {
    path: 'stickers',
    loadComponent: () =>
      import('./pages/sticker-catalog/sticker-catalog').then((m) => m.StickerCatalog),
  },
  {
    path: 'stickers/:stickerId',
    loadComponent: () =>
      import('./pages/sticker-detail/sticker-detail').then((m) => m.StickerDetail),
  },
  {
    path: 'customize',
    loadComponent: () => import('./pages/customize/customize').then((m) => m.Customize),
  },
  {
    path: 'favorites',
    loadComponent: () => import('./pages/favorites/favorites').then((m) => m.Favorites),
  },
  { path: 'about', loadComponent: () => import('./pages/about/about').then((m) => m.About) },
  { path: 'login', loadComponent: () => import('./pages/login/login').then((m) => m.Login) },
  { path: 'signup', loadComponent: () => import('./pages/signup/signup').then((m) => m.Signup) },
  {
    path: 'checkout',
    loadComponent: () => import('./pages/checkout/checkout').then((m) => m.Checkout),
  },
  {
    path: 'checkout/complete',
    loadComponent: () =>
      import('./pages/checkout/checkout-complete').then((m) => m.CheckoutComplete),
  },
  {
    path: '**',
    loadComponent: () => import('./pages/not-found/not-found').then((m) => m.NotFound),
  },
];
