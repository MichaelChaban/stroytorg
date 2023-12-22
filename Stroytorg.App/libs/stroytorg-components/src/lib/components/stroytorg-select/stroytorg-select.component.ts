import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgSelectComponent {}
