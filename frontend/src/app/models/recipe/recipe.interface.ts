export interface Recipe {
    id: number;
    name: string;
    description?: string;
    instructions: string;
    imagePath?: string;
    prepTime?: string;
    cookTime?: string;
    servings: number;
    difficulty: number;
    categoryId: number;
    ingredients: string[];
    tags: string[];
}