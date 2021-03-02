import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MancalaAboutComponent } from './mancala-about.component';

describe('MancalaAboutComponent', () => {
  let component: MancalaAboutComponent;
  let fixture: ComponentFixture<MancalaAboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MancalaAboutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MancalaAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
