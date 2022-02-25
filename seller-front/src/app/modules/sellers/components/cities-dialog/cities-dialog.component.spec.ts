import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CitiesDialogComponent } from './cities-dialog.component';

describe('CitiesDialogComponent', () => {
  let component: CitiesDialogComponent;
  let fixture: ComponentFixture<CitiesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CitiesDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CitiesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
