import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { catchError, delay, retry, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatListModule
  ],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent {
  private http = inject(HttpClient);
  private snackBar = inject(MatSnackBar);

  // State variables
  categories: any[] = [];
  loading = true;
  error: string | null = null;
  retryCount = 0;

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.loading = true;
    this.error = null;
    
    this.http.get<any>('http://localhost:5014/api/category/getAll')
      .pipe(
        delay(500), // For demo purposes (remove in production)
        retry(2), // Auto-retry twice on failure
        tap(() => this.retryCount = 0),
        catchError((err) => {
          const errorMessage = err.message || 'Failed to load categories';
          this.error = errorMessage;
          this.loading = false;
          this.retryCount++;
          this.showErrorSnackbar(errorMessage); // Now guaranteed to be string
          return of(null);
        })
      )
      .subscribe({
        next: (response) => {
          if (response?.success) {
            this.categories = response.data;
          } else if (response === null) {
            // Error already handled
          } else {
            this.error = 'Invalid response format';
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