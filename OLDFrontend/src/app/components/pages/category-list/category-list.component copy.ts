import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { catchError, delay, retry, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { ApiResponse } from '../../../models/api-response.model';
import { Category } from '../../../models/category/category.interface';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    //MatSnackBar,
    MatListModule
  ],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent {
  private http = inject(HttpClient);
  private snackBar = inject(MatSnackBar);

  categories: Category[] = [];
  loading = true;
  error: string | null = null;
  retryCount = 0;

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.loading = true;
    this.error = null;
    
    this.http.get<ApiResponse<Category[]>>('http://localhost:5014/api/category/getAllCategoriesAsync')
      .pipe(
        delay(500),
        retry(2),
        tap(() => this.retryCount = 0),
        catchError(err => {
          const errorMessage = err.message || 'Failed to load categories';
          this.error = errorMessage;
          this.loading = false;
          this.retryCount++;
          this.showErrorSnackbar(errorMessage);
          return of(null);
        })
      )
      .subscribe({
        next: (response) => {
          if (response?.success) {
            this.categories = response.data;
          } else if (response === null) {
            // error handled
          } else {
            this.error = 'Unexpected response';
            this.showErrorSnackbar(this.error);
          }
          this.loading = false;
        }
      });
  }

  private showErrorSnackbar(message: string) {
    this.snackBar.open(message, 'Dismiss', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }

  retry() {
    this.loadCategories();
  }
}