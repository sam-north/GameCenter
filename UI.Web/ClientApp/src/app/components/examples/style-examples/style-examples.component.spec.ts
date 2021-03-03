import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StyleExamplesComponent } from './style-examples.component';

describe('StyleExamplesComponent', () => {
  let component: StyleExamplesComponent;
  let fixture: ComponentFixture<StyleExamplesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StyleExamplesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StyleExamplesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
