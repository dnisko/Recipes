import { Ingredient } from "../ingredient/ingredient.interface";
import { Tag } from "../tag/tag.interface";
export interface Recipe {
    id: number;
    name: string;
    description?: string;
    instructions: string;
    imagePath?: string;
    prepTime?: string;
    cookTime?: string;
    servings: number;
    difficulty: number;// 0 = Easy, 1 = Medium, 2 = Hard
    categoryId: number;
    ingredients: Ingredient[];
    tags: Tag[];
}

export interface PaginatedRecipeResponse {
  items: Recipe[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
}