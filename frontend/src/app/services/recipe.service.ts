import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Recipe } from '../models/recipe/recipe.interface';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})

export class RecipeService {
  private apiUrl = 'http://localhost:5014/api/recipe';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Recipe[]> {
  return this.http.get<ApiResponse<Recipe[]>>(`${this.apiUrl}/getAll`).pipe(
    map(response => {
      if (response.success) {
        return response.data;
      }
      throw new Error(response.message.join(', '));
    })
  );
}

  getById(id: number): Observable<Recipe> {
    return this.http.get<ApiResponse<Recipe>>(`${this.apiUrl}/getById/${id}`).pipe(
      map(response => {
      if (response.success) {
        return response.data;
      }
        throw new Error(response.message.join(', '));
      })
    );
  }

  getByCategory(categoryId: number): Observable<Recipe[]> {
    return this.http.get<ApiResponse<Recipe[]>>(`${this.apiUrl}/getByCategory/${categoryId}`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }
  
  search(searchTerm: string): Observable<Recipe[]> {
    return this.http.get<ApiResponse<Recipe[]>>(`${this.apiUrl}/search/?keyword=${searchTerm}`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }
  addRecipe(recipe: Recipe): Observable<Recipe> {
    return this.http.post<ApiResponse<Recipe>>(`${this.apiUrl}/add`, recipe).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }

  updateRecipe(recipe: Recipe): Observable<Recipe> {
    return this.http.put<ApiResponse<Recipe>>(`${this.apiUrl}/update`, recipe).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }

  deleteRecipe(id: number): Observable<void> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/delete/${id}`).pipe(
      map(response => {
        if (response.success) {
          return;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }

  getIngredientsByRecipeId(recipeId: number): Observable<Recipe[]> {
    return this.http.get<ApiResponse<Recipe[]>>(`${this.apiUrl}/getIngredientsByRecipeId/${recipeId}`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }

  getPopularTags(): Observable<string[]> {
    return this.http.get<ApiResponse<string[]>>(`${this.apiUrl}/getPopularTags`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }

  getTagsByRecipeId(recipeId: number): Observable<string[]> {
    return this.http.get<ApiResponse<string[]>>(`${this.apiUrl}/getTagsByRecipeId/${recipeId}`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        }
        throw new Error(response.message.join(', '));
      })
    );
  }
}
