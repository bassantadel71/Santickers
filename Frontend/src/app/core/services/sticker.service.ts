import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Sticker } from '../models/sticker.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StickerService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/Stickers`;

  getStickers(): Observable<Sticker[]> {
    return this.http.get<Sticker[]>(this.apiUrl);
  }

  getSticker(id: number): Observable<Sticker> {
    return this.http.get<Sticker>(`${this.apiUrl}/${id}`);
  }
}
