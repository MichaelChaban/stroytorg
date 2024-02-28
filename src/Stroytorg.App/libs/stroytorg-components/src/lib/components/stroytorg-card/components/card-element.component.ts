/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { KeyOrFunctionPipe } from '@stroytorg/shared';
import { ButtonSize, StroytorgButtonComponent } from '../../stroytorg-button';
import { CardRowDefinition } from '../stroytorg-card.models';
import { environment } from 'apps/stroytorg.app/src/environments/environment';

@Component({
  selector: 'stroytorg-card-element',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent, KeyOrFunctionPipe],
  templateUrl: './card-element.component.html',
})
export class StroytorgCardElementComponent<T> {

  @Input()
  cardRowDefinition!: CardRowDefinition[];

  @Input()
  cardData!: T;

  ButtonSize = ButtonSize;

  baseImagePath = environment.baseImagePath;

  isContentShown = false;

  isSet(element: any) {
    if (element) {
      return element;
    }
  }
}
