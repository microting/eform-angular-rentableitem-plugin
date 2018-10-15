import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesPnImportComponent } from './vehicles-pn-import.component';

describe('VehiclesPnImportComponent', () => {
  let component: VehiclesPnImportComponent;
  let fixture: ComponentFixture<VehiclesPnImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehiclesPnImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesPnImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
