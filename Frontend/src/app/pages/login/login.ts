import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Eye, EyeOff, Lock, Mail } from 'lucide-angular';
import { AuthIllustration } from '../../shared/auth-illustration/auth-illustration';
import { AuthField } from '../../shared/auth-field/auth-field';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, AuthIllustration, AuthField],
  templateUrl: './login.html',
})
export class Login {
  readonly MailIcon = Mail;
  readonly LockIcon = Lock;
  readonly EyeIcon = Eye;
  readonly EyeOffIcon = EyeOff;
  readonly showPassword = signal(false);

  togglePassword(): void {
    this.showPassword.update((v) => !v);
  }
}
