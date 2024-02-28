import { Icon } from '@stroytorg/shared';

export enum ButtonSize {
  SMALL = 'small-width',
  DEFAULT = 'default-width',
  LARGE = 'large-width',
  XLARGE = 'x-large-width',
  FULL = 'full-width',
  FIT_CONTENT = 'fit-content-width',
}

export enum ButtonStyle {
  DEFAULT = 'default-button',
  PRIMARY = 'primary-button',
  OUTLINED = 'outlined-button',
  WARNING = 'warning-button',
  DANGER = 'danger-button',
}

export interface ButtonDefinition {
  title?: string;
  icon?: Icon;
  buttonStyle?: string;
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
