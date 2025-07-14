import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { Category } from '../models/category/category.interface';
import { PaginatedResult } from '../models/paginated-result.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private baseUrl = 'http://localhost:5014/api/Category';
  
  constructor(private http: HttpClient) { }

  getAllCategories(params?: {
    searchKeyword?: string,
    pageNumber?: number,
    pageSize?: number})
  : Observable<ApiResponse<PaginatedResult<Category>>> {
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
    return this.http.get<ApiResponse<PaginatedResult<Category>>>(`${this.baseUrl}/getAllCategoriesAsync`, { params: httpParams });
  }
}