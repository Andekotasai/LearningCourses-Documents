import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalConcept } from './signal-concept';

describe('SignalConcept', () => {
  let component: SignalConcept;
  let fixture: ComponentFixture<SignalConcept>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignalConcept]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignalConcept);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
