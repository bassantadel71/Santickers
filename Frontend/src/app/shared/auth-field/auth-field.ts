import { Component, Input } from '@angular/core';
import { LucideAngularModule, type LucideIconData } from 'lucide-angular';

@Component({
  selector: 'app-auth-field',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './auth-field.html',
})
export class AuthField {
  @Input() label = '';
  @Input() icon!: LucideIconData;
  @Input() type: 'text' | 'email' | 'password' = 'text';
  @Input() placeholder = '';
  @Input() toggleable = false;
}
