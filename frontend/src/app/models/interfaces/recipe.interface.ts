export interface Recipe {
  id: number;
  name: string;
  description: string;
  prepTime: number;
  cookTime: number;
  servings: number;
  difficulty: 'Easy' | 'Medium' | 'Hard';
  categoryId: number;
  ingredients: RecipeIngredient[];
  steps: RecipeStep[];
}

export interface RecipeIngredient {
  id: number;
  name: string;
  amount: number;
  unit: string;
}

export interface RecipeStep {
  id: number;
  stepNumber: number;
  instruction: string;
}