import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StroytorgCardComponent } from './stroytorg-card.component';

describe('StroytorgCardComponent', () => {
  let component: StroytorgCardComponent<any>;
  let fixture: ComponentFixture<StroytorgCardComponent<any>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StroytorgCardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StroytorgCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});