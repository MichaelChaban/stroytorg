import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-home-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-home-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgHomePageComponent {}
