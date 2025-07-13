import { Recipe } from "./recipe.interface";

export interface Category {
  id: number;
  name: string;
  recipes?: Recipe[]; // optional, only filled when included
}