import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Home } from 'lucide-angular';
import { Wordmark } from '../../shared/wordmark/wordmark';

@Component({
  selector: 'app-auth-navbar',
  standalone: true,
  imports: [RouterLink, LucideAngularModule, Wordmark],
  templateUrl: './auth-navbar.html',
})
export class AuthNavbar {
  readonly HomeIcon = Home;
}
