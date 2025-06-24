import { Routes } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/pages/home/home.component';
import { CategoryListComponent } from './components/pages/category-list/category-list.component';
import { RecipeListComponent } from './components/pages/recipe/recipe.-listcomponent';
import { SearchComponent } from './components/pages/search/search.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'categories', component: CategoryListComponent },
      { path: 'recipes', component: RecipeListComponent },
      { path: 'search', component: SearchComponent }
    ]
  }
];
