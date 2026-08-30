import { Component, inject, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Heart, ShoppingBag } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { FavoritesService } from '../../core/services/favorites.service';
import { Sticker } from '../../core/models/sticker.model';
import { money } from '../money';
import { apiImageUrl } from '../api-image';

@Component({
  selector: 'app-sticker-card',
  standalone: true,
  imports: [RouterLink, LucideAngularModule],
  templateUrl: './sticker-card.html',
})
export class StickerCard {
  readonly HeartIcon = Heart;
  readonly ShoppingBagIcon = ShoppingBag;
  readonly money = money;

  private readonly cart = inject(CartService);
  private readonly favs = inject(FavoritesService);

  @Input({ required: true }) sticker!: Sticker;

  imageUrl(): string {
    return apiImageUrl(this.sticker.imageUrl);
  }

  isFavorite(): boolean {
    return this.favs.isFavorite(String(this.sticker.id));
  }

  isPending(): boolean {
    return this.favs.isPending(String(this.sticker.id));
  }

  toggleFavorite(): void {
    this.favs.toggleFavorite(String(this.sticker.id)).subscribe({
      error: () => {},
    });
  }

  addToCart(): void {
    this.cart.addToCart({
      key: String(this.sticker.id),
      name: this.sticker.name,
      price: this.sticker.price,
      image: apiImageUrl(this.sticker.imageUrl),
    });
  }
}