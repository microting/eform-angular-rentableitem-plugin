import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInspectionsPnPageComponent } from './vehicle-inspections-pn-page.component';

describe('VehicleInspectionsPnPageComponent', () => {
  let component: VehicleInspectionsPnPageComponent;
  let fixture: ComponentFixture<VehicleInspectionsPnPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehicleInspectionsPnPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehicleInspectionsPnPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
