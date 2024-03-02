import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ContentChildren,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ChangeDetectionStrategy,
  OnChanges,
  SimpleChanges,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { MobileService, ObjectUtils, Page, Sort } from '@stroytorg/shared';
import { StroytorgCheckboxComponent } from '../stroytorg-checkbox';
import { StroytorgPaginatorComponent } from '../stroytorg-paginator';
import { TABLE_COLUMN_PROVIDER, ColumnProvider, ColumnDefinition, ColumnModel, RowModel } from './stroytorg-table.models';
import { StroytorgLoaderComponent } from '../stroytorg-loader';
import { StroytorgButtonComponent } from '../stroytorg-button';

@Component({
  selector: 'stroytorg-table',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    StroytorgPaginatorComponent,
    StroytorgCheckboxComponent,
    StroytorgLoaderComponent,
    StroytorgButtonComponent
  ],
  templateUrl: './stroytorg-table.component.html',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class StroytorgTableComponent<T> implements AfterViewInit, OnInit, OnChanges {
  sorters!: Sort<T>[];

  constructor(
    public cdRef: ChangeDetectorRef,
    readonly router: Router,
    readonly route: ActivatedRoute
  ) {}

  @ContentChildren(TABLE_COLUMN_PROVIDER) viewColumns!: ColumnProvider<T>[];

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
  sortMultiple = false;

  @Input()
  multipleSelect = false;

  @Input()
  autoIndex = false;

  @Input()
  page!: Page<T> | undefined;

  @Input()
  entityId!: string;

  @Input()
  searchIndex$!: Observable<number>;

  @Output()
  tableSelection = new EventEmitter<T[]>();

  sortColumn: Sort<T>[] = [];

  sorted!: boolean;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  @Input() selectedRows: any[] | null | undefined = [];

  private readonly mobileService = inject(MobileService);

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  get isMobile(){
    return this.mobileService.getIsMobile();
  }

  private _columnDefinitions!: ColumnDefinition<T>[];

  get columnDefinitions(): ColumnDefinition<T>[] {
    return this._columnDefinitions;
  }

  get areAllSelected(): boolean {
    return this.tableRows.every((x) => x.selected);
  }

  @Input()
  set columnDefinitions(value: ColumnDefinition<T>[]) {
    this._columnDefinitions = value;
    this.tableColumns = (value ?? []).map((x) => new ColumnModel(x));
  }

  @Output()
  pageChange = new EventEmitter<number>();

  @Output()
  sortChange = new EventEmitter<Sort<T>[]>();

  tableColumns: ColumnModel<T>[] = [];

  tableRows: RowModel<T>[] = [];

  ngOnInit(): void {
    // this.isMobileBlock = this.screenSizeService.isMobileBlock;
    this.updateData();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedRows']) {
      const newSelectedRows = changes['selectedRows'].currentValue || [];
      this.tableRows.forEach((x) => {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        x.selected = newSelectedRows.some((y: any) => ObjectUtils.objectsEqual(y, x.row))
      })
    }
  }


  updateData(): void {
    this.getPage(this.page);
    this.tableRows = (this.data ?? []).map((x) => new RowModel(x, false));
  }

  getIndex(index: number) {
    return (this.currentPage - 1) * this.pageSize + index + 1;
  }

  ngAfterViewInit(): void {
    if (this.viewColumns?.length) {
      this.viewColumns.forEach((x) => {
        if (x?.position === 'start') {
          this.columnDefinitions = [
            x?.getDefinition(),
            ...(this.columnDefinitions ?? []),
          ];
        } else {
          this.columnDefinitions = [
            ...(this.columnDefinitions ?? []),
            x?.getDefinition(),
          ];
        }
      });
      this.cdRef.detectChanges();
    }
  }

  sort(column: ColumnModel<T>) {
    column.sortDirection =
      column.sortDirection === undefined
        ? 'asc'
        : column.sortDirection === 'desc'
        ? undefined
        : 'desc';
    if (!this.sortMultiple) {
      this.tableColumns
        .filter((x) => x !== column)
        .forEach((x) => (x.sortDirection = undefined));
    }

    this.sortColumn = this.tableColumns
      .filter((x) => x.sortDirection)
      .map(
        (x) =>
          ({
            id: x.configuration.id,
            sortDirection: x.sortDirection,
          } as unknown as Sort<T>)
      );

    this.sortChange.emit(this.sortColumn);
    this.cdRef.detectChanges();
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  pageChanged(paginatorState: any): void {
    if (this.currentPage != paginatorState.page) {
      this.currentPage = paginatorState.page;
      this.total = paginatorState.totalRecords;
      this.pageSize = paginatorState.rows;
    }
    this.pageChange.emit(this.currentPage);
    this.cdRef.detectChanges();
  }

  routerRedirect(url: string) {
    this.router.navigate([url]);
  }

  selectAllRows(event: boolean) {
    this.tableRows.forEach((x) => (x.selected = event));
    this.selectedRows = this.tableRows
      .filter((row) => row.selected)
      .map((row) => row.row);
    this.tableSelection.emit(this.selectedRows);
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  selectRow(event: boolean, item: any) {
    this.tableRows
      .filter((x) => x.row === item)
      .forEach((x) => (x.selected = event));
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const sItems: any[] | undefined = event
      ? [...(this.selectedRows ? [...this.selectedRows] : []), item]
      : this.selectedRows?.filter((x) => !ObjectUtils.objectsEqual(x, item));

    this.tableSelection.emit(sItems);
  }

  getPage(page?: Page<T>) {
    if (page) {
      this.total = page.count;
      this.pageSize = page.size;
      this.data = page.list;
      this.currentPage = page.number;
    }
  }

  resetSelectedRows() {
    this.tableRows = this.tableRows.map((x: RowModel<T>) => {
      return { ...x, selected: false };
    });
    this.cdRef.detectChanges();
  }

  isSet(element: any) {
    if (element) {
      return element;
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-unused-vars
  protected comapareIds(a: any[], b: any): boolean {
    if (a && a.length === 0) {
      return false;
    }

    return true;
  }
}
