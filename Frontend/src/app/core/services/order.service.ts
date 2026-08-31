import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateOrder, CreateOrderResult, Order } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private readonly baseUrl = `${environment.apiUrl}/Orders`;

  constructor(private http: HttpClient) {}

  createOrder(payload: CreateOrder): Observable<CreateOrderResult> {
    return this.http.post<CreateOrderResult>(this.baseUrl, payload);
  }

  getById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.baseUrl}/${id}`);
  }
}
