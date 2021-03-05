import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckersAboutComponent } from './checkers-about.component';

describe('CheckersAboutComponent', () => {
  let component: CheckersAboutComponent;
  let fixture: ComponentFixture<CheckersAboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckersAboutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckersAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
