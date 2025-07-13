import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category/category.interface';
import { CategoryService } from '../../../services/category.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
  imports: [
    CommonModule,
    RouterModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    //MatSnackBar,
    MatListModule
  ],
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];

  // Pagination
  pageNumber = 1;
  pageSize = 10;
  totalRecords = 0;

  // Optional filters
  searchKeyword = '';
  hasRecipe: boolean | undefined = undefined;

  loading = false;
  errorMessage = '';

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading = true;
    this.errorMessage = '';

    this.categoryService.getCategories({
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchKeyword: this.searchKeyword,
      hasRecipe: this.hasRecipe
    }).subscribe({
      next: (response) => {
        this.categories = response.data.items;
        this.totalRecords = response.data.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load categories.';
        console.error(error);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.pageNumber = 1;
    this.loadCategories();
  }

  onPageChange(newPage: number): void {
    this.pageNumber = newPage;
    this.loadCategories();
  }
}
