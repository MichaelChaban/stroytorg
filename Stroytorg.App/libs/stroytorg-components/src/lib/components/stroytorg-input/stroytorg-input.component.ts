import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-input',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-input.component.html',
  styleUrls: ['@stroytorg/shared-styles'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgInputComponent {}
