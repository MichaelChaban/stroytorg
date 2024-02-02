/* eslint-disable @angular-eslint/directive-selector */
import { ContentChild, Directive, Input, TemplateRef } from '@angular/core';
import { TABLE_COLUMN_PROVIDER, ColumnProvider, AdditionalColumnPosition, ColumnDefinition } from './stroytorg-table.models';

@Directive({
  selector: 'stroytorg-template-column',
  standalone: true,
  providers: [
    {
      provide: TABLE_COLUMN_PROVIDER,
      useExisting: TemplateColumnDirective,
      multi: true,
    },
  ],
})
export class TemplateColumnDirective<T> implements ColumnProvider<T> {
  @Input() position: AdditionalColumnPosition = 'end';

  @ContentChild(TemplateRef) template!: TemplateRef<T>;

  @Input() id!: string;

  @Input() columnClass!: string;

  @Input() sortable!: boolean;

  @Input() routerLink!: (row: T) => string;

  @Input() headerName!: (column: string, index?: number) => string | string;

  getDefinition(): ColumnDefinition<T> {
    return {
      id: this.id,
      columnClass: this.columnClass,
      template: this.template,
      headerName: this.headerName,
      routerLink: this.routerLink,
    };
  }
}
