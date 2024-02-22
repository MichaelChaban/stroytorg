import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgCheckboxComponent } from './stroytorg-checkbox.component';

describe('StroytorgCheckboxComponent', () => {
  let component: StroytorgCheckboxComponent;
  let fixture: ComponentFixture<StroytorgCheckboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgCheckboxComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgCheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
