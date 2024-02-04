import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StroytorgCardElementComponent } from './components/card-element.component';
import { CardDefinition } from './stroytorg-card.models';

@Component({
  selector: 'stroytorg-card',
  standalone: true,
  imports: [CommonModule, StroytorgCardElementComponent],
  templateUrl: './stroytorg-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgCardComponent<T> {
  @Input()
  cardDefinition!: CardDefinition[];

  @Input()
  data!: T[];
}
