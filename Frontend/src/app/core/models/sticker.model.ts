export interface Sticker {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  isAvailable: boolean;
  categoryId: number;
  category?: Category;
}
export interface Category {
  id: number;
  name: string;
}
export interface ComingSoonItem {
  id: string;
  title: string;
  blurb: string;
  image: string;
  tag: string;
}
