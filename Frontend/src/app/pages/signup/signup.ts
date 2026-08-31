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
  readonly showConfirm = signal(false);

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  fullName = '';
  email = '';
  password = '';
  confirmPassword = '';

  errorMessage = '';
  isLoading = false;

  register(): void {
    this.errorMessage = '';

    if (!this.fullName.trim()) {
      this.errorMessage = 'Please enter your full name.';
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

    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    const nameParts = this.fullName.trim().split(/\s+/);

    const firstName = nameParts[0];
    const lastName = nameParts.slice(1).join(' ') || '';

    this.isLoading = true;

    this.authService
      .register({
        email: this.email.trim(),
        password: this.password,
        firstName,
        lastName,
      })
      .subscribe({
        next: (res) => {
          this.isLoading = false;

          if (!res.succeeded || !res.data) {
            this.errorMessage = res.errors?.join(', ') || 'Registration failed.';
            return;
          }

          // Save the returned authentication session
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
