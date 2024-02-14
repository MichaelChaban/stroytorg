/* eslint-disable @typescript-eslint/no-unused-vars */
import { InjectionToken, TemplateRef } from '@angular/core';
import { Params } from '@angular/router';
import { Sort, SortDirection, keyOrFn } from '@stroytorg/shared';

export interface ColumnDefinition<T, _KEY = keyof T> {
  id: string;
  value?: ((row?: T) => string | undefined) | string;
  headerName?: string;
  icon?: string | ((row?: T) => string);
  type?: string;
  sortable?: boolean;
  routerLink?: ((row?: T) => string) | string;
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
  routerLink?: ((row: T) => string);
  valueGetter!: (row: T) => string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  headerGetter!: string;
  template?: TemplateRef<T>;

  constructor(readonly configuration: ColumnDefinition<T>) {
    if (this.configuration.value) {
      this.valueGetter = (row) => keyOrFn(this.configuration.value!, row);
    }

    if (this.configuration.routerLink) {
      this.routerLink = (row) => keyOrFn(this.configuration.routerLink!, row);
    }

    if (this.configuration.columnClass) {
      this.columnClass = this.configuration.columnClass;
    }

    if (this.configuration.template) {
      this.template = this.configuration.template;
    }

    if (this.configuration.headerName) {
      this.headerGetter = this.configuration.headerName;
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