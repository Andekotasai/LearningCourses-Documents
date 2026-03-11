import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoWayBindingChildSignal } from './two-way-binding-child-signal';

describe('TwoWayBindingChildSignal', () => {
  let component: TwoWayBindingChildSignal;
  let fixture: ComponentFixture<TwoWayBindingChildSignal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TwoWayBindingChildSignal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TwoWayBindingChildSignal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
