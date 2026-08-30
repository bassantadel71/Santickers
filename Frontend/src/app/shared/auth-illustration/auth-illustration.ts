import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-auth-illustration',
  standalone: true,
  templateUrl: './auth-illustration.html',
})
export class AuthIllustration {
  @Input() heading = '';
  @Input() subtext = '';
}
