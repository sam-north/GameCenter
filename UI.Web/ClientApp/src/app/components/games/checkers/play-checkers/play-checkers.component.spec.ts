import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayCheckersComponent } from './play-checkers.component';

describe('PlayCheckersComponent', () => {
  let component: PlayCheckersComponent;
  let fixture: ComponentFixture<PlayCheckersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayCheckersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayCheckersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
