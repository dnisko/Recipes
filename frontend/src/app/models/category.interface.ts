// src/app/models/category.model.ts
export interface Category {
  id: number;
  name: string;
  recipes: any[]; // or Recipe[] if you have a Recipe interface
}