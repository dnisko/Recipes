import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  navItems = [
    { path: '/categories', label: 'Categories' },
    { path: '/recipes', label: 'Recipes' },
    { path: '/search', label: 'Search' }
  ];
}