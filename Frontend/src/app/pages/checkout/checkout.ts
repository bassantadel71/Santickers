import { Component, signal } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LucideAngularModule, Lock, Mail, MapPin, Truck } from 'lucide-angular';
import { CartService } from '../../core/services/cart.service';
import { OrderService } from '../../core/services/order.service';
import { CreateOrderItem } from '../../core/models/order.model';
import { money } from '../../shared/money';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [ReactiveFormsModule, LucideAngularModule],
  templateUrl: './checkout.html',
})
export class Checkout {
  readonly MailIcon = Mail;
  readonly TruckIcon = Truck;
  readonly LockIcon = Lock;
  readonly MapPinIcon = MapPin;

  readonly money = money;

  readonly submitting = signal(false);
  readonly errorMessage = signal<string | null>(null);
  readonly paymentMethod = signal<'paymob' | 'cod'>('paymob');

  // Paymob iframe URL
  readonly safeIframeUrl = signal<SafeResourceUrl | null>(null);
  readonly shippingFee = 30;
  form!: ReturnType<FormBuilder['group']>;

  constructor(
    public cart: CartService,
    private fb: FormBuilder,
    private orderService: OrderService,
    private router: Router,
    private sanitizer: DomSanitizer,
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^01[0-2,5]{1}[0-9]{8}$/)]],
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      streetAddress: ['', [Validators.required, Validators.minLength(5)]],
      city: ['', Validators.required],
      governorate: ['', Validators.required],
      postalCode: ['', [Validators.required, Validators.pattern(/^\d{4,6}$/)]],
    });
  }

  get total(): number {
    return this.cart.subtotal() + this.shippingFee;
  }

  submit(): void {
    // Don't submit if form is invalid
    // or cart is empty
    if (this.form.invalid || this.cart.cart().length === 0) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting.set(true);
    this.errorMessage.set(null);

    const items: CreateOrderItem[] = this.cart.cart().map((c) => ({
      stickerId: Number(c.key),
      quantity: c.qty,
    }));

    const { email, phoneNumber, fullName, streetAddress, city, governorate, postalCode } =
      this.form.getRawValue();

    this.orderService
      .createOrder({
        email: email!,
        phoneNumber: phoneNumber!,
        fullName: fullName!,
        streetAddress: streetAddress!,
        city: city!,
        governorate: governorate!,
        postalCode: postalCode!,
        paymentMethod: this.paymentMethod(),
        items,
      })
      .subscribe({
        next: (result) => {
          this.submitting.set(false);

          if (this.paymentMethod() === 'cod') {
            this.cart.clear();
            this.router.navigate(['/checkout/complete'], {
              queryParams: {
                success: 'true',
                merchant_order_id: result.order.id,
                method: 'cod',
              },
            });
            return;
          }

          // Paymob: trust the iframe URL returned by our backend
          this.safeIframeUrl.set(
            this.sanitizer.bypassSecurityTrustResourceUrl(result.paymentIframeUrl),
          );
        },
        error: () => {
          this.submitting.set(false);
          this.errorMessage.set(
            "We couldn't place your order right now. Please check your details and try again.",
          );
        },
      });
  }

  fieldInvalid(name: string): boolean {
    const control = this.form.get(name);
    return !!control && control.invalid && (control.dirty || control.touched);
  }
}
