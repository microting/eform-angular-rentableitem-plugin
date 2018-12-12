import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractInspectionsUpdateComponent } from './contract-inspections-update.component';

describe('InspectionsUpdateComponent', () => {
  let component: ContractInspectionsUpdateComponent;
  let fixture: ComponentFixture<ContractInspectionsUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractInspectionsUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractInspectionsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
