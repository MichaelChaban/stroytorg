import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent, TableStoreDirective } from '@frontend/shared/table';
import { ColumnsDefinition, PagedData } from '@frontend/shared/domain';
import { getMainPageTableColumnDefinitions } from './main-page.columnDefinition';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [CommonModule, TableComponent, TableStoreDirective],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent implements OnInit {
  pagedData: PagedData<any> = {
    size: 0,
    page: 1,
    pagedData: {
      total: 2,
      data: [
        { id: 1, name: 'Hello', age: '1', year: '2022' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
        { id: 2, name: 'Hello', age: '2', year: '2021' },
      ],
    },
  };
  columnDefinitions!: ColumnsDefinition[];

  ngOnInit() {
    this.columnDefinitions = getMainPageTableColumnDefinitions();
  }
}
