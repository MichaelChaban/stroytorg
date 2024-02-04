import { Icon } from "@stroytorg/shared";

export enum ButtonSize {
  small = 'small',
  normal = '',
  large = 'large',
  xlarge = 'x-large',
}

export enum ButtonType {
  default = '',
  primary = 'primary',
  outlined = 'primary-outlined',
}

export interface ButtonDefinition{
    title?: string;
    icon?: Icon;
    buttonType?: string;
    routerLink?: string;
    queryParams?: string;
    tooltip?: TooltipDefinition;
    size: ButtonSize;
    onClick?: (row: any) => any;
}

export interface TooltipDefinition{
  tooltipText: string;
  tooltipPosition: 'above' | 'left' | 'right' | 'below';
}