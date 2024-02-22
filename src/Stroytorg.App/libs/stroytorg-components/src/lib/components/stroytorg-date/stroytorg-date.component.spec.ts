import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgDateComponent } from './stroytorg-date.component';

describe('StroytorgDateComponent', () => {
  let component: StroytorgDateComponent;
  let fixture: ComponentFixture<StroytorgDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgDateComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
