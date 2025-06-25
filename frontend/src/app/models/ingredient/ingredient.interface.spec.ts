import { TestBed } from '@angular/core/testing';

import { IngredientInterface } from './ingredient.interface';

describe('IngredientInterface', () => {
  let service: IngredientInterface;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IngredientInterface);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
