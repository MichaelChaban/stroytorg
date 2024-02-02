/* eslint-disable @typescript-eslint/no-explicit-any */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgTableComponent } from './stroytorg-table.component';

describe('StroytorgTableComponent', () => {
  let component: StroytorgTableComponent<any>;
  let fixture: ComponentFixture<StroytorgTableComponent<any>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgTableComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
