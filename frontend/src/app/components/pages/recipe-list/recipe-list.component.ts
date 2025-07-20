import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Ingredient } from '../../../models/ingredient/ingredient.interface';
import { Tag } from '../../../models/tag/tag.interface';
import { RecipeService } from '../../../services/recipe.service';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recipe-list.component.html',
  styleUrl: './recipe-list.component.scss'
})
export class RecipeListComponent implements OnInit {
  ingredients: Ingredient[] = [];
  tag: Tag[] = [];
  loading: true;
  error: string | null = null;

  constructor(private recipeService: RecipeService) { }
  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes() {
    this.loading = true;
    this.recipeService.getAllRecipes({pageNumber: 1, pageSize: 10}).subscribe({
      next: (response) => {
        this.ingredients = response.data.items;
        this.loading = false;
      }
    })
  }

}
