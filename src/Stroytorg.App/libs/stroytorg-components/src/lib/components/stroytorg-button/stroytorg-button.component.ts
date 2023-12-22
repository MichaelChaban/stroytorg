import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-button.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgButtonComponent {
  @Input()
  title!: string;
}
