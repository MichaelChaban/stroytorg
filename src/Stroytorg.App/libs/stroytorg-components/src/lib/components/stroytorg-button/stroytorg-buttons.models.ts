import { Icon } from '@stroytorg/shared';

export type ButtonSize =
  | 'small-width'
  | 'default-width'
  | 'large-width'
  | 'x-large-width'
  | 'full-width'
  | 'fit-content-width'
  | 'auto-width';

export type ButtonPalette =
  | 'default-button'
  | 'primary-button'
  | 'outlined-button'
  | 'warning-button'
  | 'danger-button';

export type ButtonStyle =
  | 'basic-button'
  | 'raised-button'
  | 'stroked-button'
  | 'flat-button';

export interface ButtonDefinition {
  title?: string;
  icon?: Icon;
  buttonPalette?: ButtonPalette;
  buttonStyle?: ButtonStyle;
  routerLink?: string;
  queryParams?: string;
  tooltip?: TooltipDefinition;
  size: ButtonSize;
  onClick?: (row: unknown) => unknown;
}

export type TooltipDefinition = {
  tooltipText: string;
  tooltipPosition?: 'above' | 'left' | 'right' | 'below';
};
