import { Component, inject, OnInit } from '@angular/core';
import { LucideAngularModule, Search } from 'lucide-angular';
import { CategoryService } from '../../core/services/category.service';
import { StickerService } from '../../core/services/sticker.service';
import { Sticker } from '../../core/models/sticker.model';
import { StickerCard } from '../../shared/sticker-card/sticker-card';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-sticker-catalog',
  standalone: true,
  imports: [LucideAngularModule, StickerCard],
  templateUrl: './sticker-catalog.html',
})
export class StickerCatalog implements OnInit {
  readonly SearchIcon = Search;

  private readonly stickerService = inject(StickerService);
  private readonly categoryService = inject(CategoryService);

  private readonly cdr = inject(ChangeDetectorRef);
  
  categories: string[] = ['All'];

  private all: Sticker[] = [];
  results: Sticker[] = [];

  cat = 'All';
  query = '';

  ngOnInit(): void {
    this.stickerService.getStickers().subscribe((s) => {
      console.log('STICKERS:', s);
      console.log('CAT:', this.cat);

      this.all = s;
      this.applyFilter();
      console.log('RESULTS:', this.results);
      this.cdr.detectChanges();
    });

    this.categoryService.getCategories().subscribe((c) => {
      const names = c.map((x) => x.name).filter((n) => n && n !== 'All');
      this.categories = ['All', ...names];
    });
  }

  chipClass(c: string): string {
    return c === this.cat
      ? 'bg-navy text-cream shadow-[var(--shadow-soft)]'
      : 'bg-beige text-navy/70 hover:bg-peach hover:text-navy';
  }

  setCat(c: string): void {
    this.cat = c;
    this.applyFilter();
  }

  onSearch(e: Event): void {
    this.query = (e.target as HTMLInputElement).value;
    this.applyFilter();
  }

  private applyFilter(): void {
    const q = this.query.trim().toLowerCase();
    this.results = this.all.filter((s) => {
      const matchCat = this.cat === 'All' || s.categoryName === this.cat;
      const matchQuery =
        !q || s.name.toLowerCase().includes(q) || s.categoryName.toLowerCase().includes(q);
      return matchCat && matchQuery;
    });
  }
}
