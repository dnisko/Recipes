import { Component } from '@angular/core';

import { RouterModule } from '@angular/router';
import { NavBarComponent } from '../shared/nav-bar/nav-bar';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterModule, NavBarComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {

}
