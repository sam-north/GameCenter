import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayMancalaComponent } from './play-mancala.component';

describe('PlayMancalaComponent', () => {
  let component: PlayMancalaComponent;
  let fixture: ComponentFixture<PlayMancalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayMancalaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayMancalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
