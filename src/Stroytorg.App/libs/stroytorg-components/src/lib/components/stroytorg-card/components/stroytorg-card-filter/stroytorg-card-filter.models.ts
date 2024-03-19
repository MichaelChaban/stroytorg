import { TemplateRef } from "@angular/core";

export type FilterType = 'boolean' | 'select' | 'range' | 'text' | 'date' | 'multiselect';

export interface FilterOption {
    label: string;
    value: any;
}

export interface FilterDefinition {
    type: FilterType;
    label: string;
    value: any;
    options?: FilterOption[];
    rangeMinValue?: number;
    rangeMaxValue?: number;
    template: TemplateRef<any>;
}