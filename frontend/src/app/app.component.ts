import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
 // Remove this if present
import { CategoryService } from './services/category.service';
import { Category } from './models/category.interface';

@Component({
  selector: 'app-root',
  standalone: true, // Critical for standalone apps
  imports: [CommonModule], // Add other modules as needed
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class AppComponent {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.categoryService.getAll().subscribe({
      next: (data) => this.categories = data,
      error: (err) => console.error('Failed to load categories', err)
    });
  }
}