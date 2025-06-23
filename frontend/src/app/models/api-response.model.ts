import { Category } from "./category.interface";

export interface ApiResponse {
  success: boolean;
  message: string[];
  data: Category[];
}