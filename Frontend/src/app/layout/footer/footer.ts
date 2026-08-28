import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Instagram, Mail } from 'lucide-angular';
import { Curve } from '../../shared/curve/curve';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, Curve],
  templateUrl: './footer.html',
  styleUrl: './footer.css',
})
export class Footer {
  readonly InstagramIcon = Instagram;
  readonly MailIcon = Mail;
}
