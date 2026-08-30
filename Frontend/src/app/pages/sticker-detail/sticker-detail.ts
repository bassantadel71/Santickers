import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { LucideAngularModule, ArrowLeft, Heart, Minus, Plus, ShoppingBag, Truck } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { FavoritesService } from '../../core/services/favorites.service';
import { StickerService } from '../../core/services/sticker.service';
import { Sticker } from '../../core/models/sticker.model';
import { money } from '../../shared/money';
import { apiImageUrl } from '../../shared/api-image';

@Component({
  selector: 'app-sticker-detail',
  standalone: true,
  imports: [RouterLink, LucideAngularModule],
  templateUrl: './sticker-detail.html',
})
export class StickerDetail implements OnInit {
  readonly ArrowLeftIcon = ArrowLeft;
  readonly HeartIcon = Heart;
  readonly MinusIcon = Minus;
  readonly PlusIcon = Plus;
  readonly ShoppingBagIcon = ShoppingBag;
  readonly TruckIcon = Truck;
  readonly money = money;

  private readonly route = inject(ActivatedRoute);
  private readonly stickerService = inject(StickerService);
  private readonly cart = inject(CartService);
  private readonly favs = inject(FavoritesService);

  sticker: Sticker | null | undefined = undefined;
  qty = 1;

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        switchMap((params) => this.stickerService.getSticker(Number(params.get('stickerId')))),
      )
      .subscribe({
        next: (s) => (this.sticker = s ?? null),
        error: () => (this.sticker = null),
      });
  }

  imageUrl(): string {
    return this.sticker ? apiImageUrl(this.sticker.imageUrl) : '';
  }

  isFavorite(): boolean {
    return this.sticker ? this.favs.isFavorite(String(this.sticker.id)) : false;
  }

  isPending(): boolean {
    return this.sticker ? this.favs.isPending(String(this.sticker.id)) : false;
  }

  toggleFavorite(): void {
    if (this.sticker) {
      this.favs.toggleFavorite(String(this.sticker.id)).subscribe({
        error: () => {},
      });
    }
  }

  decreaseQty(): void {
    this.qty = Math.max(1, this.qty - 1);
  }

  increaseQty(): void {
    this.qty += 1;
  }

  addToCart(): void {
    if (!this.sticker) return;
    this.cart.addToCart(
      {
        key: String(this.sticker.id),
        name: this.sticker.name,
        price: this.sticker.price,
        image: apiImageUrl(this.sticker.imageUrl),
      },
      this.qty,
    );
  }
}