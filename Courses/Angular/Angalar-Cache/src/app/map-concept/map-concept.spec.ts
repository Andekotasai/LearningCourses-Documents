import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapConcept } from './map-concept';

describe('MapConcept', () => {
  let component: MapConcept;
  let fixture: ComponentFixture<MapConcept>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MapConcept]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MapConcept);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
