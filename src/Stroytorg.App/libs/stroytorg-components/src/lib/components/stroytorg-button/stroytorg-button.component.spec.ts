import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GovButtonComponent } from './stroytorg-button.component';

describe('GovButtonComponent', () => {
  let component: GovButtonComponent;
  let fixture: ComponentFixture<GovButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GovButtonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GovButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
