import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Heart } from 'lucide-angular';
import { FavoritesService } from '../../core/services/favorites.service';
import { StickerService } from '../../core/services/sticker.service';
import { Sticker } from '../../core/models/sticker.model';
import { StickerCard } from '../../shared/sticker-card/sticker-card';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, StickerCard],
  templateUrl: './favorites.html',
})
export class Favorites implements OnInit {
  readonly HeartIcon = Heart;

  private readonly favs = inject(FavoritesService);
  private readonly stickerService = inject(StickerService);

  private readonly all = signal<Sticker[]>([]);
  readonly saved = computed(() =>
    this.all().filter((s) => this.favs.favorites().includes(String(s.id))),
  );

  ngOnInit(): void {
    this.stickerService.getStickers().subscribe((s) => this.all.set(s));
  }
}