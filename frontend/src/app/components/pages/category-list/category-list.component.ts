import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category/category.interface';
import { CategoryService } from '../../../services/category.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss'
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];
  loading = true;
  error: string | null = null;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.loadCategories();
  }
  
  loadCategories() {
    this.loading = true;
    this.categoryService.getAllCategories({pageNumber: 1, pageSize: 10}).subscribe({
      next: (response) => {
        this.categories = response.data.items;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load categories';
        console.error(err);
        this.loading = false;
      }
    });
  }
}
