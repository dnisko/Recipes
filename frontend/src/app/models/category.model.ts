export interface Category {
  id: number;
  name: string;
  recipes: any[]; // adjust type if you want to model recipes too
}