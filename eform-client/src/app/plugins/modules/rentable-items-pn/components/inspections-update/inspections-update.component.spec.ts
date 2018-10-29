import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionsUpdateComponent } from './inspections-update.component';

describe('InspectionsUpdateComponent', () => {
  let component: InspectionsUpdateComponent;
  let fixture: ComponentFixture<InspectionsUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InspectionsUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
