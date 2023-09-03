export interface CardElementsDefinition<T>{
    columnsNumber: number;
    data: T[];
    elements: ElementDefinition[];
}

export interface ElementDefinition{
    type: any;
}