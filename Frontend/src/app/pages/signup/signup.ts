import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { LucideAngularModule, Eye, EyeOff, Lock, Mail, User } from 'lucide-angular';

import { AuthIllustration } from '../../shared/auth-illustration/auth-illustration';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [RouterLink, FormsModule, LucideAngularModule, AuthIllustration],
  templateUrl: './signup.html',
})
export class Signup {
  readonly UserIcon = User;
  readonly MailIcon = Mail;
  readonly LockIcon = Lock;
  readonly EyeIcon = Eye;
  readonly EyeOffIcon = EyeOff;

  readonly showPassword = signal(false);

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  firstName = '';
  lastName = '';
  email = '';
  password = '';

  errorMessage = '';
  isLoading = false;

  register(): void {
    this.errorMessage = '';

    if (!this.firstName.trim()) {
      this.errorMessage = 'Please enter your first name.';
      return;
    }

    if (!this.lastName.trim()) {
      this.errorMessage = 'Please enter your last name.';
      return;
    }

    if (!this.email.trim()) {
      this.errorMessage = 'Please enter your email.';
      return;
    }

    if (!this.password) {
      this.errorMessage = 'Please enter a password.';
      return;
    }

    this.isLoading = true;

    this.authService
      .register({
        firstName: this.firstName.trim(),
        lastName: this.lastName.trim(),
        email: this.email.trim(),
        password: this.password,
      })
      .subscribe({
        next: (res) => {
          this.isLoading = false;

          if (!res.succeeded || !res.data) {
            this.errorMessage = res.errors?.join(', ') || 'Registration failed.';
            return;
          }

          localStorage.setItem('santickers.authToken', res.data.token);

          localStorage.setItem('santickers.authUser', res.data.email);

          this.router.navigate(['/']);
        },

        error: (err) => {
          this.isLoading = false;

          this.errorMessage =
            err?.error?.errors?.join(', ') ||
            err?.error?.message ||
            'Registration failed. Please try again.';
        },
      });
  }
}
