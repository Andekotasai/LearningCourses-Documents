import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjectionTokenConcept } from './injection-token-concept';

describe('InjectionTokenConcept', () => {
  let component: InjectionTokenConcept;
  let fixture: ComponentFixture<InjectionTokenConcept>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InjectionTokenConcept]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InjectionTokenConcept);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
