import { Component, inject } from '@angular/core';
import { LucideAngularModule, X } from 'lucide-angular';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './toast.html',
})
export class Toast {
  readonly XIcon = X;

  readonly toastService = inject(ToastService);

  dismiss(id: number): void {
    this.toastService.dismiss(id);
  }
}