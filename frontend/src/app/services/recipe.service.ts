import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResponse } from '../models/api-response.model';
import { Recipe } from '../models/interfaces/recipe.interface';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RecipeService {
  private apiUrl = 'http://localhost:5014/api/Recipe';

  constructor(private http: HttpClient) { }

  getRecipes(pageNumber: number, pageSize: number): Observable<PaginatedResponse<Recipe>> {
    // Convert to HttpParams properly
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PaginatedResponse<Recipe>>(`${this.apiUrl}/getAllWithDetails`, { params });
  }

  getRecipe(id: number) {
    return this.http.get<Recipe>(`${this.apiUrl}/getRecipeAsync/${id}`);
  }

  createRecipe(recipe: Recipe) {
    return this.http.post(`${this.apiUrl}/createRecipeAsync`, recipe);
  }

  updateRecipe(id: number, recipe: Recipe) {
    return this.http.put(`${this.apiUrl}/updateRecipeAsync/${id}`, recipe);
  }

  deleteRecipe(id: number) {
    return this.http.delete(`${this.apiUrl}/deleteRecipeAsync/${id}`);
  }
}