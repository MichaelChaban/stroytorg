import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-input',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-text-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgTextInputComponent {}
