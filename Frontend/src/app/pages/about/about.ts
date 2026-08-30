import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Curve } from '../../shared/curve/curve';

interface AboutCard {
  img: string;
  title: string;
  text: string;
  tilt: string;
}

const CARDS: AboutCard[] = [
  {
    img: 'assets/stickers/cat.png',
    title: 'Drawn, not generated',
    text: "Every design starts as a sketch. If it doesn't make us smile, it doesn't get printed.",
    tilt: '-1.5deg',
  },
  {
    img: 'assets/stickers/quote.png',
    title: 'Built to survive',
    text: 'Waterproof vinyl, matte lamination, die-cut edges. Backpacks, coffee, and rain approved.',
    tilt: '1deg',
  },
  {
    img: 'assets/stickers/flowers.png',
    title: 'Yours to remix',
    text: "Upload your own art and we'll print it exactly the way we print ours. No minimums.",
    tilt: '-0.8deg',
  },
];

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [RouterLink, Curve],
  templateUrl: './about.html',
})
export class About {
  readonly cards = CARDS;
}