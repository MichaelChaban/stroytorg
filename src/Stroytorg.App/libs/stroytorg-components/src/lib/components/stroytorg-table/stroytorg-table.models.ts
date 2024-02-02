/* eslint-disable @typescript-eslint/no-unused-vars */
import { InjectionToken, TemplateRef } from '@angular/core';
import { Params } from '@angular/router';
import { Sort, SortDirection } from '@stroytorg/shared';

export interface ColumnDefinition<T, _KEY = keyof T> {
  id: string;
  value?: (row: T, index?: number) => string;
  headerName?: (column: string, index?: number) => string | string;
  icon?: string | ((row?: T | undefined) => string);
  type?: string;
  sortable?: boolean;
  routerLink?: (row: T) => string | string;
  queryParams?: Params;
  columnClass?: string;
  template?: TemplateRef<T>;
}

export function LinkRenderer<T>(text: string, href: string) {
  return (row: T, index: number | undefined) => {
    return '<a href="' + href + '">' + text + '</a>';
  };
}

export function routerRenderer<T>(link: string, text: string) {
  return (row: T, index?: number | undefined) => {
    return { text: text, link: link };
  };
}

export function Renderer<T>(fnc: (row: T) => string) {
  return (row: T) => {
    return fnc(row);
  };
}

export class ColumnModel<T> {
  sortDirection: SortDirection | undefined = undefined;
  sortColumn: Sort<T> | undefined = undefined;
  columnClass?: string;
  queryParams?: Params;
  routerLink?: (row: T) => string | string;
  valueGetter!: (row: T, index: number) => string;
  headerGetter!: (column: string, index: number) => string | string;
  template?: TemplateRef<T>;

  get headerName(): string {
    return this.configuration.headerName?.toString() ?? 'undefined';
  }

  constructor(readonly configuration: ColumnDefinition<T>) {
    if (this.configuration.value) {
      this.valueGetter = (row, index) =>
        this.configuration.value?.(row, index) ?? '';
    } else {
      this.valueGetter = (row) =>
        ((row as { [key: string]: string })?.[
          this.configuration.id
        ] as string) ?? '';
    }

    if (this.configuration.routerLink) {
      this.routerLink = this.configuration.routerLink;
    }

    if (this.configuration.columnClass) {
      this.columnClass = this.configuration.columnClass;
    }

    if (this.configuration.template) {
      this.template = this.configuration.template;
    }

    if (this.configuration.headerName) {
      this.headerGetter = (column, index) =>
        this.configuration.headerName?.(column, index) ?? '';
    } else {
      this.headerGetter = (column, index) => column[index];
    }

    if (this.configuration.queryParams) {
      this.queryParams = this.configuration.queryParams;
    }

    if (this.routerLink) {
      this.columnClass += ' ' + 'link';
    }
  }
}

export declare type AdditionalColumnPosition = 'start' | 'end';

export interface ColumnProvider<T> {
  getDefinition(): ColumnDefinition<T>;
  position: AdditionalColumnPosition;
}

export const TABLE_COLUMN_PROVIDER = new InjectionToken(
  'TABLE_COLUMN_PROVIDER'
);

export class RowModel<T> {
  row: T;
  index?: number;
  selected: boolean;
  marked?: boolean;
  focused?: boolean;
  fnc?: void;

  constructor(row: T, selected: boolean, marked?: boolean, index?: number, fnc?: void) {
    this.row = row;
    this.index = index;
    this.selected = selected;
    this.marked = marked;
    this.fnc = fnc;
  }
}