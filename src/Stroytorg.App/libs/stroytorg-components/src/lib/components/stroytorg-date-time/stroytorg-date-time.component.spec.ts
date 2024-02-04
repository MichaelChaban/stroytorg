import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgDateTimeComponent } from './stroytorg-date-time.component';

describe('StroytorgDateTimeComponent', () => {
  let component: StroytorgDateTimeComponent;
  let fixture: ComponentFixture<StroytorgDateTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgDateTimeComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgDateTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
