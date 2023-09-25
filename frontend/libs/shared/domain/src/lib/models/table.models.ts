import { ButtonStyle, Icons } from "@frontend/shared/domain";

export interface ColumnsDefinition{
    headerName?: string;
    headerAction?: ColumnActionDefinition[];
    value?: ((row: any) => any) | string;
    columnAction?: ColumnActionDefinition[];
    widthClass?: string;
}

export interface ColumnActionDefinition{
    buttonStyle: ButtonStyle;
    tooltip?: string;
    icon?: Icons;
    label?: string;
    onClick?: (row: any) => any;
    routerLink?: string;
    queryParams?: { [key: string]: any };
}

export interface PagedData<T>{
    page: number;
    number: number;
    pagedData: {
        total: number;
        data: T[];
    };
}