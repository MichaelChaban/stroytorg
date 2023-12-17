/* eslint-disable @angular-eslint/component-selector */
import {
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColumnsDefinition, KeyOrFunctionPipe, PagedData } from '@frontend/shared/domain';
import { PaginatorComponent } from '@frontend/shared/paginator';
import { outputAst } from '@angular/compiler';
@Component({
  selector: 'stroytorg-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  standalone: true,
  imports: [CommonModule, PaginatorComponent, KeyOrFunctionPipe],
})
export class TableComponent<T> {

  @Input()
  columnDefinitions!: ColumnsDefinition[];

  @Input()
  pagedData!: PagedData<T>;

  @Input()
  tableRepository!: string;

  @Input()
  data!: T[] | undefined;

  @Input()
  total!: number;

  @Input()
  loading!: boolean;

  @Input()
  pageSize!: number;

  @Input()
  currentPage!: number;

  @Input()
  showPaginator = true;

  @Input()
  useCheckbox = false;

  @Input()
  customText!: string;

  @Output()
  pageChange = new EventEmitter<number>();

  calculateTotalPages(): number {
    return Math.ceil(this.pagedData.pagedData.total / this.pagedData.size);
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
  }

  pageChanged(paginatorState: any): void {
    if (this.currentPage != paginatorState.page) {
      this.currentPage = paginatorState.page;
      this.total = paginatorState.total;
      this.pageSize = paginatorState.size;
    }
    this.pageChange.emit(this.currentPage);
  }

}
