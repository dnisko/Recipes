import { Routes } from '@angular/router';
import { HomeComponent } from './components/pages/home/home.component';
import { CategoryListComponent } from './components/pages/category-list/category-list.component';
import { RecipeListComponent } from './components/pages/recipe-list/recipe-list.component';
import { SearchComponent } from './components/pages/search/search.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'recipes', component: RecipeListComponent },
    { path: 'search', component: SearchComponent }
];
