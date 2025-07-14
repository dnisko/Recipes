export interface PaginatedResponse<T> {
  success: boolean;
  message: string[];
  data: {
    items: T[];
    totalRecords: number;
    pageNumber: number;
    pageSize: number;
  };
}