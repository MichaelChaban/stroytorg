import { ButtonStyle, Icon } from "../enums";
import { TooltipModel } from "./tooltipProperties.model";

export interface ButtonPropertiesModel{
    label?: string;
    icon?: Icon;
    buttonStyle?: ButtonStyle;
    routerLink?: string;
    queryParams?: string;
    tooltip?: TooltipModel;
    width?: number;
    onClick?: (row: any) => any;
}