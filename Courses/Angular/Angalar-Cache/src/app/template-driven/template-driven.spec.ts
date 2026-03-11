import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateDriven } from './template-driven';

describe('TemplateDriven', () => {
  let component: TemplateDriven;
  let fixture: ComponentFixture<TemplateDriven>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TemplateDriven]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TemplateDriven);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
