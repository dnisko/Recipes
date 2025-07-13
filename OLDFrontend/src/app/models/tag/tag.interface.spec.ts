import { TestBed } from '@angular/core/testing';

import { TagInterface } from './tag.interface';

describe('TagInterface', () => {
  let service: TagInterface;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TagInterface);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
