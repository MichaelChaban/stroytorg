/* eslint-disable @typescript-eslint/no-explicit-any */
import { TemplateRef } from '@angular/core';
import { ButtonDefinition } from '../stroytorg-button';

export interface CardRowDefinition {
  title?: string | ((row: any) => string);
  content?: string | ((row: any) => string);
  imageName?: string;
  cardRowActions?: ButtonDefinition[];
  template?: TemplateRef<any>;
  cardRowClass?: string;
  isContentShown?: boolean;
  cardDisabled?: ((data: any) => boolean) | boolean;
}

export interface CardDefinition {
  cardRowDefinition: CardRowDefinition[];
  cardDisabled?: ((data: any) => boolean) | boolean;
}
