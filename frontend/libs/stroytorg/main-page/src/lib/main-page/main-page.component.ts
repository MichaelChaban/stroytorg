import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from '@frontend/shared/table';
import { ColumnsDefinition, PagedData } from '@frontend/shared/domain';
import { getMainPageTableColumnDefinitions } from './main-page.columnDefinition';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [CommonModule, TableComponent],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent implements OnInit{
  pagedData: PagedData<any> = { number: 0, page: 0, pagedData: {total: 2, data: [{ id: 1, name: 'Hello' }, { id: 2, name: 'Hello' }]} }
  columnDefinitions!: ColumnsDefinition[];

  ngOnInit(){
    this.columnDefinitions = getMainPageTableColumnDefinitions();
  }
}
