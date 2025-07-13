// src/app/services/category.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Category, PaginatedCategoryResponse } from '../models/category/category.interface';
import { ApiResponse } from '../models/api-response.model';

@Injectable({ providedIn: 'root' })

export class CategoryService {
  private apiUrl = 'http://localhost:5014/api/category';

  constructor(private http: HttpClient) { }

  // getAll(): Observable<Category[]> {
  //   return this.http.get<ApiResponse<Category[]>>(`${this.apiUrl}/getAll`).pipe(
  //     map(response => {
  //       if (response.success)return response.data;
  //       throw new Error(response.message.join(', '));
  //     }),
  //     catchError(error => {
  //       console.error('Error fetching categories:', error);
  //       return throwError(() => error);
  //     })
  //   );
  // }

  getCategories(params: {
  pageNumber: number,
  pageSize: number,
  searchKeyword?: string,
  hasRecipe?: boolean,
  id?: number,
  sortBy?: string,
  sortDirection?: string
}): Observable<{ data: PaginatedCategoryResponse }> {
  let httpParams = new HttpParams()
    .set('pageNumber', params.pageNumber)
    .set('pageSize', params.pageSize);

  if (params.searchKeyword) httpParams = httpParams.set('searchKeyword', params.searchKeyword);
  if (params.hasRecipe !== undefined) httpParams = httpParams.set('hasRecipe', params.hasRecipe.toString());
  if (params.id) httpParams = httpParams.set('id', params.id);
  if (params.sortBy) httpParams = httpParams.set('sortBy', params.sortBy);
  if (params.sortDirection) httpParams = httpParams.set('sortDirection', params.sortDirection);

  return this.http.get<{ data: PaginatedCategoryResponse }>(`${this.apiUrl}/GetAllCategoriesAsync`, { params: httpParams });
}
}