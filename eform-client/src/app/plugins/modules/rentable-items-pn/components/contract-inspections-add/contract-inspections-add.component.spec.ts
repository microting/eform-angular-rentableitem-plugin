import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractInspectionsAddComponent } from './contract-inspections-add.component';

describe('InspectionsAddComponent', () => {
  let component: ContractInspectionsAddComponent;
  let fixture: ComponentFixture<ContractInspectionsAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractInspectionsAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractInspectionsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
