import { TemplateRef } from '@angular/core';
import { ButtonDefinition } from '../stroytorg-button';

export interface CardDefinition {
  display?: 'flex';
  title?: string | ((row: any) => string);
  content?: string | ((row: any) => string);
  imageName?: string;
  actions?: ButtonDefinition[];
  template?: TemplateRef<any>;
  classes?: string;
}