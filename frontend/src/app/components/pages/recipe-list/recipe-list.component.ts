import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../../../services/recipe.service';
import { Recipe } from '../../../models/recipe/recipe.interface';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatIconModule
  ],
  templateUrl: './recipe-list.component.html',
  styleUrl: './recipe-list.component.scss'
})
export class RecipeListComponent implements OnInit {
  recipes: Recipe[] = [];
  loading = true;
  error: string | null = null;
  expandedIngredients: { [index: number]: boolean } = {};

  constructor(private recipeService: RecipeService) { }
  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes() {
    this.loading = true;    
    this.recipeService.getAllRecipes({pageNumber: 1, pageSize: 10}).subscribe({
      next: (response) => {
        // console.log("tuka");
        // console.log('API Response:', response);
        // console.log(response.data.items[0].ingredients);
        // console.log(response.data.items[0].tags);
        
        this.recipes = response.data.items;
        console.log('recipes:', this.recipes);
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load recipes';
        console.error(err);
        this.loading = false;
      }
    });
  }
  toggleIngredient(index: number): void {
    this.expandedIngredients[index] = !this.expandedIngredients[index];
  }
}
