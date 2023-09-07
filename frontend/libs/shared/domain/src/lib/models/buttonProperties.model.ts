import { ButtonStyle, Icons } from "../enums";
import { TooltipPropertiesModel } from "./tooltipProperties.model";

export interface ButtonPropertiesModel{
    label?: string;
    icon?: Icons;
    buttonStyle?: ButtonStyle;
    routerLink?: string;
    queryParams?: string;
    tooltip?: TooltipPropertiesModel;
    onClick?: (row: any) => any;
}