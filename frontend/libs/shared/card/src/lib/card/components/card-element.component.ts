/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ButtonComponent } from '@frontend/shared/button';
import { CardDefinition, KeyOrFunctionPipe } from '@frontend/shared/domain';

@Component({
  selector: 'card-element',
  standalone: true,
  imports: [CommonModule, ButtonComponent, KeyOrFunctionPipe, ButtonComponent],
  templateUrl: './card-element.component.html',
})
export class CardElementComponent<T> {

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