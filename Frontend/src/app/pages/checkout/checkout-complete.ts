import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { LucideAngularModule, CheckCircle2, XCircle } from 'lucide-angular';

@Component({
  selector: 'app-checkout-complete',
  standalone: true,
  imports: [RouterLink, LucideAngularModule],
  templateUrl: './checkout-complete.html',
})
export class CheckoutComplete implements OnInit {
  readonly CheckIcon = CheckCircle2;
  readonly XIcon = XCircle;
  readonly success = signal(false);
  readonly orderId = signal<string | null>(null);
  readonly paymentMethod = signal<string | null>(null);

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      this.success.set(params.get('success') === 'true');
      this.orderId.set(params.get('merchant_order_id'));
      this.paymentMethod.set(params.get('method'));
    });
  }
}
