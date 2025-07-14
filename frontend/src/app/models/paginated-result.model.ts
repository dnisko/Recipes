export interface PaginatedResult<T> {
  items: T[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
}