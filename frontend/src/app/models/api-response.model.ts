export interface ApiResponse<T> {
  success: boolean;
  message: string[];
  data: T;
}

export interface PaginatedResult<T> {
  items: T[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
}