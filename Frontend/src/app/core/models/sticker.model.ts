export interface Sticker {
  id: number;
  name: string;
  description: string | null;
  price: number;
  imageUrl: string | null;
  isAvailable: boolean;
  categoryId: number;
  categoryName: string;
}
export interface Category {
  id: number;
  name: string;
  description: string | null;
  imageUrl: string | null;
}
export interface ComingSoonItem {
  id: string;
  title: string;
  blurb: string;
  image: string;
  tag: string;
}