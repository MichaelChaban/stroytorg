import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgFormComponent {}
