import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoWayBindingSignal } from './two-way-binding-signal';

describe('TwoWayBindingSignal', () => {
  let component: TwoWayBindingSignal;
  let fixture: ComponentFixture<TwoWayBindingSignal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TwoWayBindingSignal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TwoWayBindingSignal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
