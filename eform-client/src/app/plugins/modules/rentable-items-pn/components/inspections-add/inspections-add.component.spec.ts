import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionsAddComponent } from './inspections-add.component';

describe('InspectionsAddComponent', () => {
  let component: InspectionsAddComponent;
  let fixture: ComponentFixture<InspectionsAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InspectionsAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
