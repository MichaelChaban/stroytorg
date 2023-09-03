/* eslint-disable @angular-eslint/component-selector */
import {
  Component,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColumnsDefinition, PagedData } from '@frontend/shared/domain';
import { PaginatorComponent } from '@frontend/shared/paginator';
@Component({
  selector: 'stroytorg-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  standalone: true,
  imports: [CommonModule, PaginatorComponent],
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
    // Calculate the total number of pages based on pagedData and page size
    // For example:
    return Math.ceil(this.pagedData.pagedData.total / this.pagedData.number);
  }

  onPageChange(newPage: number) {
    // Handle page change here, e.g., fetch data for the new page
    // Update this.pagedData with the new data
    // For example:
    this.currentPage = newPage;
    // Fetch new data here using the updated page number (this.currentPage)
  }
}
