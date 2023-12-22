import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-date',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-date.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgDateComponent {}
