import { Component } from '@angular/core';

import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterModule],
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