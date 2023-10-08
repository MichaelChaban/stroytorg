import { ButtonStyle, Icons } from "../enums";
import { TooltipProperties } from "./tooltipProperties.model";

export interface ButtonProperties{
    label?: string;
    icon?: Icons;
    buttonStyle?: ButtonStyle;
    routerLink?: string;
    queryParams?: string;
    tooltip?: TooltipProperties;
    width?: number;
    onClick?: (row: any) => any;
}