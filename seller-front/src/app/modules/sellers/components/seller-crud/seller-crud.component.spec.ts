import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerCrudComponent } from './seller-crud.component';

describe('SellerCrudComponent', () => {
  let component: SellerCrudComponent;
  let fixture: ComponentFixture<SellerCrudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerCrudComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
