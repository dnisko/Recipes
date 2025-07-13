import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginatedListComponent } from '../../shared/paginated-list/paginated-list.component';
import { CategoryService } from '../../../services/category.service';
import { Category } from '../../../models/interfaces/category.interface';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [
    CommonModule,
    PaginatedListComponent,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  isLoading = false;
  items: Category[] = [];
  pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0
  };

  constructor(
    private categoryService: CategoryService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.isLoading = true;
    this.categoryService.getCategories(
      this.pagination.pageNumber,
      this.pagination.pageSize
    ).subscribe({
      next: (response) => {
        this.items = response.data.items;
        this.pagination.totalRecords = response.data.totalRecords;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.snackBar.open('Failed to load categories', 'Dismiss', {
          duration: 3000
        });
        console.error('Error:', error);
      }
    });
  }

  onPageChange(pageNumber: number): void {
    this.pagination.pageNumber = pageNumber;
    this.loadCategories();
  }
  confirmDelete(categoryId: number): void {
  // Add your delete confirmation logic here
  // You can use MatDialog for a confirmation modal
}
}