export interface CreateOrderItem {
  stickerId: number;
  quantity: number;
}

export interface CreateOrder {
  fullName: string;
  email: string;
  phoneNumber: string;
  streetAddress: string;
  city: string;
  governorate: string;
  postalCode: string;
  paymentMethod: 'paymob' | 'cod';
  items: CreateOrderItem[];
}

export interface OrderItem {
  id: number;
  stickerId: number;
  stickerName: string;
  unitPrice: number;
  quantity: number;
}

export interface Order {
  id: number;
  fullName: string;
  subtotal: number;
  shippingFee: number;
  total: number;
  status: string;
  createdAt: string;
  items: OrderItem[];
}

export interface CreateOrderResult {
  order: Order;
  paymentIframeUrl: string;
}
