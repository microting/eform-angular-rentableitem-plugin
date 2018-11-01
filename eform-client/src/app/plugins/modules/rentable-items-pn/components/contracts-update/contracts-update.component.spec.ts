import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractsUpdateComponent } from './contracts-update.component';

describe('ContractsUpdateComponent', () => {
  let component: ContractsUpdateComponent;
  let fixture: ComponentFixture<ContractsUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractsUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
