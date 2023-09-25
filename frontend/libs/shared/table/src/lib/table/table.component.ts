/* eslint-disable @angular-eslint/component-selector */
import {
  Component,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColumnsDefinition, KeyOrFunctionPipe, PagedData } from '@frontend/shared/domain';
import { PaginatorComponent } from '@frontend/shared/paginator';
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
  useCheckbox = false;

  currentPage = 1;

  calculateTotalPages(): number {
    return Math.ceil(this.pagedData.pagedData.total / this.pagedData.number);
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
  }
}
