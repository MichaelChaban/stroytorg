import { TemplateRef } from '@angular/core';
import { ButtonPropertiesModel, CardElementType } from '@frontend/shared/domain';

export interface CardDefinition {
  elementType: CardElementType;
  displayFlex: boolean;
  label?: string | ( (row: any) => string );
  content?: string | ( (row: any) => string );
  imageName?: string;
  actions?: ButtonPropertiesModel[];
  template?: TemplateRef<any>;
  classes?: string;
}