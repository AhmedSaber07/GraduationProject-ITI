import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserinfoContainerComponent } from './userinfo-container.component';

describe('UserinfoContainerComponent', () => {
  let component: UserinfoContainerComponent;
  let fixture: ComponentFixture<UserinfoContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserinfoContainerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserinfoContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
