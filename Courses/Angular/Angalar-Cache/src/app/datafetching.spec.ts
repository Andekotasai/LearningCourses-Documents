import { TestBed } from '@angular/core/testing';

import { Datafetching } from './datafetching';

describe('Datafetching', () => {
  let service: Datafetching;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Datafetching);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
