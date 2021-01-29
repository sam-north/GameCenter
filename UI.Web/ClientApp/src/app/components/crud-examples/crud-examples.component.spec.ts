import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudExamplesComponent } from './crud-examples.component';

describe('CrudExamplesComponent', () => {
  let component: CrudExamplesComponent;
  let fixture: ComponentFixture<CrudExamplesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrudExamplesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CrudExamplesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
