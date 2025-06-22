import { Component } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-list',
  template: `
    <h2>Categories</h2>
    <ul>
      <li *ngFor="let category of categories">
        {{ category.name }}
      </li>
    </ul>
  `
})
export class CategoryListComponent {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.categoryService.getAll().subscribe(
      data => this.categories = data,
      error => console.error('Error fetching categories', error)
    );
  }
}