import { Icon } from '@stroytorg/shared';

export enum ButtonSize {
  SMALL = 'small',
  DEFAULT = '',
  LARGE = 'large',
  XLARGE = 'x-large',
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
  onClick?: (row: any) => any;
}

export type TooltipDefinition = {
  tooltipText: string;
  tooltipPosition?: 'above' | 'left' | 'right' | 'below';
};
