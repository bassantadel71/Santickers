import { Component, inject } from '@angular/core';
import { LucideAngularModule, ImagePlus, ShoppingBag, Upload, X } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { money } from '../../shared/money';

interface Size {
  id: string;
  label: string;
  note: string;
  mult: number;
}

interface Finish {
  id: string;
  label: string;
  note: string;
  extra?: number;
}

const SIZES: Size[] = [
  { id: 's', label: 'Small', note: '5 × 5 cm', mult: 1 },
  { id: 'm', label: 'Medium', note: '8 × 8 cm', mult: 1.5 },
  { id: 'l', label: 'Large', note: '12 × 12 cm', mult: 2.2 },
];

const FINISHES: Finish[] = [
  { id: 'matte', label: 'Matte', note: 'Soft, no glare' },
  { id: 'glossy', label: 'Glossy', note: 'Bright & shiny' },
  { id: 'holo', label: 'Holographic', note: 'Rainbow shimmer', extra: 12 },
];

const BASE = 20;

@Component({
  selector: 'app-customize',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './customize.html',
})
export class Customize {
  readonly ImagePlusIcon = ImagePlus;
  readonly ShoppingBagIcon = ShoppingBag;
  readonly UploadIcon = Upload;
  readonly XIcon = X;
  readonly money = money;

  private readonly cart = inject(CartService);

  readonly sizes = SIZES;
  readonly finishes = FINISHES;

  size = SIZES[1];
  finish = FINISHES[0];
  qty = 3;
  notes = '';

  preview: string | null = null;
  dragging = false;

  get unit(): number {
    return Math.round(BASE * this.size.mult + (this.finish.extra ?? 0));
  }

  get total(): number {
    return this.unit * this.qty;
  }

  setQtyFromEvent(e: Event): void {
    this.qty = Number((e.target as HTMLInputElement).value);
  }

  setNotesFromEvent(e: Event): void {
    this.notes = (e.target as HTMLTextAreaElement).value;
  }

  handleFile(file: File | null | undefined): void {
    if (!file || !file.type.startsWith('image/')) return;
    this.preview = URL.createObjectURL(file);
  }

  onFileChange(e: Event): void {
    const input = e.target as HTMLInputElement;
    this.handleFile(input.files?.[0]);
  }

  onDragOver(e: DragEvent): void {
    e.preventDefault();
    this.dragging = true;
  }

  onDragLeave(): void {
    this.dragging = false;
  }

  onDrop(e: DragEvent): void {
    e.preventDefault();
    this.dragging = false;
    this.handleFile(e.dataTransfer?.files?.[0]);
  }

  removePreview(): void {
    if (this.preview) URL.revokeObjectURL(this.preview);
    this.preview = null;
  }

  addToCart(): void {
    if (!this.preview) return;
    this.cart.addToCart(
      {
        key: `custom-${this.size.id}-${this.finish.id}-${Date.now()}`,
        name: 'Custom Sticker',
        price: this.unit,
        image: this.preview,
        meta: `${this.size.label} · ${this.finish.label}${this.notes ? ' · with notes' : ''}`,
      },
      this.qty,
    );
  }
}