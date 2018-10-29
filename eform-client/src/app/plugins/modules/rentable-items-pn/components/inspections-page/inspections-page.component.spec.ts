import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionsPageComponent } from './inspections-page.component';

describe('InspectionsPageComponent', () => {
  let component: InspectionsPageComponent;
  let fixture: ComponentFixture<InspectionsPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InspectionsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
