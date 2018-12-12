import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractInspectionsPageComponent } from './contract-inspections-page.component';

describe('InspectionsPageComponent', () => {
  let component: ContractInspectionsPageComponent;
  let fixture: ComponentFixture<ContractInspectionsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractInspectionsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractInspectionsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
