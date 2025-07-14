import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { PaginatedResponse } from '../models/api-response.model';
import { Category } from '../models/interfaces/category.interface';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private apiUrl = 'http://localhost:5014/api/Category/getAllCategoriesAsync';

  constructor(private http: HttpClient) { }

  getCategories(pageNumber: number, pageSize: number): Observable<PaginatedResponse<Category>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    console.log('Making request to:', this.apiUrl, 'with params:', params);

    return this.http.get<PaginatedResponse<Category>>(this.apiUrl, { params }).pipe(
      tap({
        next: (response) => console.log('API Response:', response),
        error: (error) => console.error('API Error:', error)
      })
    );
  }
}