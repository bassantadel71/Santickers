import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, ArrowRight, Sparkles } from 'lucide-angular';
import { StickerService } from '../../core/services/sticker.service';
import { Sticker, ComingSoonItem } from '../../core/models/sticker.model';
import { StickerCard } from '../../shared/sticker-card/sticker-card';
import { Curve } from '../../shared/curve/curve';

const TOP_CATEGORIES = ['Anime', 'el ragol el 3enab'];
const MAX_TOP_STICKERS = 6;

const COMING_SOON: ComingSoonItem[] = [
  {
    id: 'posters',
    title: 'Posters',
    blurb: 'Big, bold wall prints in the Santickers palette.',
    image: 'assets/coming/posters.jpg',
    tag: 'Fall 2026',
  },
  {
    id: 'packs',
    title: 'Sticker Packs',
    blurb: 'Curated bundles of six, wrapped and ready to gift.',
    image: 'assets/coming/packs.jpg',
    tag: 'Very soon',
  },
  {
    id: 'collections',
    title: 'New Collections',
    blurb: 'Seasonal drops designed with our favorite artists.',
    image: 'assets/coming/collections.jpg',
    tag: 'In the studio',
  },
];

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, StickerCard, Curve],
  templateUrl: './home.html',
})
export class Home implements OnInit {
  readonly SparklesIcon = Sparkles;
  readonly ArrowRightIcon = ArrowRight;

  private readonly stickerService = inject(StickerService);

  readonly comingSoon = COMING_SOON;
  // top: Sticker[] = [];
  top = signal<Sticker[]>([]);

  ngOnInit(): void {
    this.stickerService.getStickers().subscribe({
      next: (stickers) => {
        console.log(
          'All stickers:',
          stickers.length,
          stickers.map((s) => s.categoryName),
        );

        const filteredTop = stickers
          .filter((s) => TOP_CATEGORIES.includes(s.categoryName))
          .slice(0, MAX_TOP_STICKERS);

        this.top.set(filteredTop);

        console.log('Filtered top:', filteredTop);
      },
      error: (err) => console.error('Failed to load top stickers', err),
    });
  }
}
