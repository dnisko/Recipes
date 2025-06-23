import { Component } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent {
  categories: Category[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private categoryService: CategoryService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.loading = true;
    this.error = null;
    
    this.categoryService.getAll().subscribe({
      next: (data) => {
        this.categories = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.message || 'Failed to load categories';
        this.loading = false;
        this.snackBar.open(this.error, 'Dismiss', { duration: 5000 });
      }
    });
  }
}