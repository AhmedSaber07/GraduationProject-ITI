import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayProductsByCategoryIdComponent } from './display-products-by-category-id.component';

describe('DisplayProductsByCategoryIdComponent', () => {
  let component: DisplayProductsByCategoryIdComponent;
  let fixture: ComponentFixture<DisplayProductsByCategoryIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayProductsByCategoryIdComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DisplayProductsByCategoryIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
