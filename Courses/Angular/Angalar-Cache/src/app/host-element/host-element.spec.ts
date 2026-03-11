import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostElement } from './host-element';

describe('HostElement', () => {
  let component: HostElement;
  let fixture: ComponentFixture<HostElement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HostElement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HostElement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
