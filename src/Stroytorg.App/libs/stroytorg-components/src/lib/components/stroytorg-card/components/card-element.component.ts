/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { KeyOrFunctionPipe } from '@stroytorg/shared';
import { StroytorgButtonComponent } from '../../stroytorg-button';
import { CardDefinition } from '../stroytorg-card.models';

@Component({
  selector: 'stroytorg-card-element',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent, KeyOrFunctionPipe],
  templateUrl: './card-element.component.html',
})
export class StroytorgCardElementComponent<T> {
  @Input()
  cardDefinition!: CardDefinition[];

  @Input()
  cardData!: T;

  baseImagePath = '';

  showContent = false;

  isSet(element: any) {
    if (element) {
      return element;
    }
  }
}
