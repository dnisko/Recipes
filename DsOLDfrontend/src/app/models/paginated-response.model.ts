export interface PaginatedResponse1<T> {
  success: boolean;
  message: string[];
  data: {
    items: T[];
    totalRecords: number;
    pageNumber: number;
    pageSize: number;
  };
}