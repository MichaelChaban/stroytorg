import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StroytorgTimeComponent } from './stroytorg-time.component';

describe('StroytorgTimeComponent', () => {
  let component: StroytorgTimeComponent;
  let fixture: ComponentFixture<StroytorgTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgTimeComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
