// src/app/services/category.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Category } from '../models/category.interface';
import { ApiResponse } from '../models/api-response.model';

@Injectable({ providedIn: 'root' })

export class CategoryService {
  private apiUrl = 'http://localhost:5014/api/category';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<ApiResponse<Category[]>>(`${this.apiUrl}/getAll`).pipe(
      map(response => {
        if (response.success)return response.data;
        throw new Error(response.message.join(', '));
      }),
      catchError(error => {
        console.error('Error fetching categories:', error);
        return throwError(() => error);
      })
    );
  }
}