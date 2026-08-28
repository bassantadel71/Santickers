import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-curve',
  standalone: true,
  template: `
    <svg viewBox="0 0 200 60" fill="none" [attr.class]="className" aria-hidden="true">
      <path d="M4 52C24 16 62 4 100 4s76 12 96 48" stroke="currentColor" stroke-width="7" stroke-linecap="round" />
    </svg>
  `,
})
export class Curve {
  @Input() className = '';
}
