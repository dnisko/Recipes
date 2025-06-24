//import { Category } from "./category.interface";

// export interface ApiResponse {
//   success: boolean;
//   message: string[];
//   data: Category[];
// }
export interface ApiResponse<T> {
  success: boolean;
  message: string[];
  data: T;
}