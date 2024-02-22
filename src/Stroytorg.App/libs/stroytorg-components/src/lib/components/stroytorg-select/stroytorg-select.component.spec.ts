import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgSelectComponent } from './stroytorg-select.component';

describe('StroytorgSelectComponent', () => {
  let component: StroytorgSelectComponent<unknown>;
  let fixture: ComponentFixture<StroytorgSelectComponent<unknown>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgSelectComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
