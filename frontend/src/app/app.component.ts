import { Component, signal } from '@angular/core';
import { MatChipsModule } from '@angular/material/chips';

import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [
    RouterOutlet,
    MatChipsModule
    ]
})
export class AppComponent {
  protected readonly title = signal('frontend');
}
