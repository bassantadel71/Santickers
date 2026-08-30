import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Eye, EyeOff, Lock, Mail, User } from 'lucide-angular';
import { AuthIllustration } from '../../shared/auth-illustration/auth-illustration';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, AuthIllustration],
  templateUrl: './signup.html',
})
export class Signup {
  readonly UserIcon = User;
  readonly MailIcon = Mail;
  readonly LockIcon = Lock;
  readonly EyeIcon = Eye;
  readonly EyeOffIcon = EyeOff;
  readonly showPassword = signal(false);
  readonly showConfirm = signal(false);
}
