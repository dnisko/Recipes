import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CategoryService } from './category.service';
import { Category } from '../models/category.interface';
import { ApiResponse } from '../models/api-response.model';

describe('CategoryService', () => {
  let service: CategoryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoryService]
    });

    service = TestBed.inject(CategoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch categories', () => {
    const mockCategories: Category[] = [
      { id: 1, name: 'Category 1', recipes: [] },
      { id: 2, name: 'Category 2', recipes: [] }
    ];
    
    const mockResponse: ApiResponse<Category[]> = { success: true, message: [], data: mockCategories };

    service.getAll().subscribe(categories => {
      expect(categories.length).toBe(2);
      expect(categories).toEqual(mockCategories);
    });
    const req = httpMock.expectOne('http://localhost:5014/api/category/getAllCategoriesAsync');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
