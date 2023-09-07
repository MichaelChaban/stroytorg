/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardDefinition } from '@frontend/shared/domain';
import { ButtonComponent } from '@frontend/shared/button';
import { CardElementComponent } from './components/card-element.component';

@Component({
  selector: 'stroytorg-card',
  standalone: true,
  imports: [CommonModule, ButtonComponent, CardElementComponent ],
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardComponent<T> {
  @Input()
  elementsInRowCount!: number;

  @Input()
  cardDefinition!: CardDefinition[];

  @Input()
  data!: T[];
}