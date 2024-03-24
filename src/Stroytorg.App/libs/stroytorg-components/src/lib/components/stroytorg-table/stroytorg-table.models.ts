/* eslint-disable @typescript-eslint/no-unused-vars */
import { InjectionToken, TemplateRef } from '@angular/core';
import { Params } from '@angular/router';
import { Icon, Sort, SortDirection, keyOrFn } from '@stroytorg/shared';
import { ButtonPalette, TooltipDefinition } from '../stroytorg-button';

export interface ColumnDefinition<T, _KEY = keyof T> {
  id: string;
  value?: ((row?: T) => string | undefined) | string;
  headerName?: string;
  icon?: Icon;
  sortable?: boolean;
  routerLink?: ((row?: T) => string) | string;
  queryParams?: Params;
  columnClass?: string;
  columnActions?: ColumnActionDefinition[];
  template?: TemplateRef<T>;
}

export interface ColumnActionDefinition{
  buttonPalette?: ButtonPalette;
  tooltip?: TooltipDefinition;
  icon?: Icon;
  title?: string;
  buttonClass?: string;
  onClick?: (row: any) => any;
  routerLink?: string;
  queryParams?: { [key: string]: any };
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
  columnActions?: ColumnActionDefinition[];
  sortable?: boolean;
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

    if (this.sortable) {
      this.sortable = this.configuration.sortable;
    }

    if (this.configuration.columnActions) {
      this.columnActions = this.configuration.columnActions;
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
