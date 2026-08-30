import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, ArrowRight, Sparkles } from 'lucide-angular';
import { StickerService } from '../../core/services/sticker.service';
import { Sticker, ComingSoonItem } from '../../core/models/sticker.model';
import { StickerCard } from '../../shared/sticker-card/sticker-card';
import { Curve } from '../../shared/curve/curve';

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
  top: Sticker[] = [];

  ngOnInit(): void {
    this.stickerService.getStickers().subscribe((s) => {
      this.top = s.slice(0, 3);
    });
  }
}