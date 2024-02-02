import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StroytorgPaginatorComponent } from './stroytorg-paginator.component';

describe('StroytorgPaginatorComponent', () => {
  let component: StroytorgPaginatorComponent;
  let fixture: ComponentFixture<StroytorgPaginatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgPaginatorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgPaginatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
