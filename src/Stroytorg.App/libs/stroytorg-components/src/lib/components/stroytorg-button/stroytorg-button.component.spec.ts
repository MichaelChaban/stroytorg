import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgButtonComponent } from './stroytorg-button.component';

describe('StroytorgButtonComponent', () => {
  let component: StroytorgButtonComponent;
  let fixture: ComponentFixture<StroytorgButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgButtonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
