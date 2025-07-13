import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecipeService } from '../../../services/recipe.service';
import { PaginatedListComponent } from '../../shared/paginated-list/paginated-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDeleteComponent } from '../../shared/confirm-delete/confirm-delete.component';
import { MatDialog } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [
    CommonModule,
    PaginatedListComponent,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    RouterModule
  ],
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {
  isLoading = false;
  recipes: any[] = [];
  pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0
  };

  constructor(
    private recipeService: RecipeService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes(): void {
    this.isLoading = true;
    this.recipeService.getRecipes(
      this.pagination.pageNumber,
      this.pagination.pageSize
    ).subscribe({
      next: (response) => {
        this.recipes = response.data.items; // Now properly typed
        this.pagination.totalRecords = response.data.totalRecords;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.snackBar.open('Failed to load recipes', 'Dismiss', {
          duration: 3000
        });
        console.error('Error:', error);
      }
    });
  }

  onPageChange(pageNumber: number): void {
    this.pagination.pageNumber = pageNumber;
    this.loadRecipes();
  }
  confirmDelete(recipeId: number): void {
    const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: '350px',
      data: { 
        title: 'Delete Recipe',
        message: 'Are you sure you want to delete this recipe? This action cannot be undone.' 
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteRecipe(recipeId);
      }
    });
  }

  private deleteRecipe(recipeId: number): void {
    this.isLoading = true;
    this.recipeService.deleteRecipe(recipeId).subscribe({
      next: () => {
        this.snackBar.open('Recipe deleted successfully', 'Close', {
          duration: 3000
        });
        this.loadRecipes(); // Refresh the list
      },
      error: (error) => {
        this.isLoading = false;
        this.snackBar.open('Failed to delete recipe', 'Dismiss', {
          duration: 3000
        });
        console.error('Error:', error);
      }
    });
  }
}