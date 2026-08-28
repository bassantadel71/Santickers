import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-wordmark',
  standalone: true,
  templateUrl: './wordmark.html',
  styleUrl: './wordmark.css',
})
export class Wordmark {
  @Input() className = '';
  @Input() tagline = false;
}
