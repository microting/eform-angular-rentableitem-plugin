import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInspectionPnAddComponent } from './vehicle-inspection-pn-add.component';

describe('VehicleInspectionPnAddComponent', () => {
  let component: VehicleInspectionPnAddComponent;
  let fixture: ComponentFixture<VehicleInspectionPnAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehicleInspectionPnAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehicleInspectionPnAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
