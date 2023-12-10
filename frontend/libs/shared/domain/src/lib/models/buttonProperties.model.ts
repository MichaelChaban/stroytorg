import { ButtonStyle, Icon } from "../enums";
import { TooltipProperties } from "./tooltipProperties.model";

export interface ButtonProperties{
    label?: string;
    icon?: Icon;
    buttonStyle?: ButtonStyle;
    routerLink?: string;
    queryParams?: string;
    tooltip?: TooltipProperties;
    width?: number;
    onClick?: (row: any) => any;
}