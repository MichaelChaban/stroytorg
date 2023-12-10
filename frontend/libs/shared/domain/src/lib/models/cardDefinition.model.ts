import { TemplateRef } from '@angular/core';
import { ButtonProperties } from '@frontend/shared/domain';

export interface CardDefinition {
  elementType: CardElementType;
  display?: 'flex';
  label?: string | ( (row: any) => string );
  content?: string | ( (row: any) => string );
  imageName?: string;
  actions?: ButtonProperties[];
  template?: TemplateRef<any>;
  classes?: string;
}

export enum CardElementType {
  LABEL = 'label',
  CONTENT = 'content',
  ACTION = 'actions',
  IMAGE = 'image',
  TEMPLATE = 'template',
}