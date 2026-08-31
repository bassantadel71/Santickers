import { Component, OnInit, signal } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { LucideAngularModule, Eye, EyeOff, Lock, Mail } from 'lucide-angular';
import { AuthIllustration } from '../../shared/auth-illustration/auth-illustration';
import { AuthField } from '../../shared/auth-field/auth-field';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule, LucideAngularModule, AuthIllustration, AuthField],
  templateUrl: './login.html',
})
export class Login implements OnInit {
  readonly MailIcon = Mail;
  readonly LockIcon = Lock;
  readonly EyeIcon = Eye;
  readonly EyeOffIcon = EyeOff;
  readonly showPassword = signal(false);
  readonly submitting = signal(false);
  readonly errorMessage = signal<string | null>(null);

  form!: ReturnType<FormBuilder['group']>;

  private returnUrl = '/';

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });

    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') ?? '/';

    if (this.auth.isAuthenticated()) {
      this.router.navigateByUrl(this.returnUrl);
    }
  }

  togglePassword(): void {
    this.showPassword.update((v) => !v);
  }

  login(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting.set(true);
    this.errorMessage.set(null);

    const { email, password } = this.form.getRawValue();

    this.auth.login({ email: email!, password: password! }).subscribe({
      next: () => {
        this.submitting.set(false);
        this.router.navigateByUrl(this.returnUrl);
      },
      error: () => {
        this.submitting.set(false);
        this.errorMessage.set(
          "We couldn't log you in. Please check your credentials and try again.",
        );
      },
    });
  }

  fieldInvalid(name: string): boolean {
    const control = this.form.get(name);
    return !!control && control.invalid && (control.dirty || control.touched);
  }
}
