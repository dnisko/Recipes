import { TestBed } from '@angular/core/testing';

import { Recipe } from './recipe.service';

describe('Recipe', () => {
  let service: Recipe;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Recipe);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
