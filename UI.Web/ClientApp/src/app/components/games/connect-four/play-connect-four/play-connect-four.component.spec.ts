import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayConnectFourComponent } from './play-connect-four.component';

describe('PlayConnectFourComponent', () => {
  let component: PlayConnectFourComponent;
  let fixture: ComponentFixture<PlayConnectFourComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayConnectFourComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayConnectFourComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
