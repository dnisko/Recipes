import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../../../services/recipe.service';
import { Recipe } from '../../../models/recipe/recipe.interface';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatChipsModule
  ],
  animations: [
    trigger('expandCollapse', [
      state('collapsed', style({
        height: '0',
        opacity: 0,
        overflow: 'hidden',
        padding: '0 1rem',
      })),
      state('expanded', style({
        height: '*',
        opacity: 1,
        padding: '0.5rem 1rem',
      })),
      transition('collapsed <=> expanded', [animate('300ms ease')])
    ])
  ],
  templateUrl: './recipe-list.component.html',
  styleUrl: './recipe-list.component.scss'
})
export class RecipeListComponent implements OnInit {
  recipes: Recipe[] = [];
  loading = true;
  error: string | null = null;
  expandedIngredientsMap = new Map<number, Set<number>>();

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
  toggleIngredient(recipeId: number, index: number) {
    const expandedSet = this.expandedIngredientsMap.get(recipeId) || new Set<number>();
    if (expandedSet.has(index)) {
      expandedSet.delete(index);
    } else {
      expandedSet.add(index);
    }
    this.expandedIngredientsMap.set(recipeId, expandedSet);
  }
  isIngredientExpanded(recipeId: number, index: number): boolean {
    return this.expandedIngredientsMap.get(recipeId)?.has(index) ?? false;
  }
}
