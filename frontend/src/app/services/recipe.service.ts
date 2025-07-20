import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse, PaginatedResult } from '../models/api-response.model';
import { Recipe } from '../models/recipe/recipe.interface';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private baseUrl = 'http://localhost:5014/api/Recipe';

  constructor(private http: HttpClient) { }

  getAllRecipes(params?: {
    searchKeyword?: string,
    pageNumber?: number,
    pageSize?: number}) :
    Observable<ApiResponse<PaginatedResult<Recipe>>> {
      let httpParams = new HttpParams();
      if (params?.searchKeyword) {
        httpParams = httpParams.set('searchKeyword', params.searchKeyword);
      }
      if (params?.pageNumber) {
        httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
      }
      if (params?.pageSize) {
        httpParams = httpParams.set('pageSize', params.pageSize.toString());
      }
      return this.http.get<ApiResponse<PaginatedResult<Recipe>>>(`${this.baseUrl}/getAllWithDetails`, { params: httpParams });
    }
}
