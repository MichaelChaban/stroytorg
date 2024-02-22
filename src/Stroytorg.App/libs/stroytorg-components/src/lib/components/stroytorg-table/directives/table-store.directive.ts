import { AfterViewInit, Directive, inject, Input, OnInit } from '@angular/core';
import { createFeatureSelector, createSelector, Store } from '@ngrx/store';
import { UnsubscribeControlComponent } from '@stroytorg/shared';
import { debounceTime, Observable, skip, take, takeUntil } from 'rxjs';
import { StroytorgBaseFormComponent } from '../../stroytorg-base-form';
import { StroytorgButtonComponent } from '../../stroytorg-button';
import { createGetPageTableAction, createStaticFilterTableAction } from '../state-helpers/actions';
import { TableState } from '../state-helpers/models';
import { StroytorgTableComponent } from '../stroytorg-table.component';

@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: 'stroytorg-table[useTableStore]',
  exportAs: 'stroytorgTableStore',
  standalone: true,
})
export class TableStoreDirective
  extends UnsubscribeControlComponent
  implements OnInit, AfterViewInit
{
  store = inject(Store);

  table = inject(StroytorgTableComponent, { self: true });

  @Input() autostart = false;
  @Input() ngrxFeatureKey!: string;
  @Input() submitButton!: StroytorgButtonComponent;
  @Input() formChanged$!: Observable<any>;
  @Input() debounceTime!: number;
  @Input() staticFilter!: Partial<any>;
  @Input() externalFilterComponent!: StroytorgBaseFormComponent<any>;
  @Input() externalFilterComponent2!: StroytorgBaseFormComponent<any>;
  @Input() markedIdsChange$!: Observable<any[]>;

  private tableState!: TableState<any>;

  //TODO: napojit loading

  ngOnInit(): void {
    if (!this.table.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }
    this.loadInitedTableState();
    this.listenSearchPageChange();
    this.listenExternalComponent();
    this.listenExternalComponent2();
    this.initStaticFilter();
    this.listenIncomeData();
    this.listenPageChange();
    this.listenFormChanged();
    this.listenSorter();
    this.listenLoadingData();
    // this.listenMarkedIdsChanged();
  }

  ngAfterViewInit(): void {
    if (this.autostart) {
      this.submit(true);
    }
  }
  listenMarkedIdsChanged() {
    this.markedIdsChange$
      .pipe(takeUntil(this.destroyed$), skip(1))
      .subscribe((x) => {
        this.table.markedIds = x;
        this.table.markRows();
      });
  }

  listenFormChanged() {
    this.formChanged$
      ?.pipe(debounceTime(this.debounceTime ?? 50), takeUntil(this.destroyed$))
      .subscribe(() => {
        this.submit(true);
      });
  }

  submit(resetPagination: boolean) {
    const filter = {
      ...this.externalFilterComponent?.createCompleteData(),
      ...this.externalFilterComponent2?.createCompleteData(),
      ...this.staticFilter,
    };
    const sorters = this.table.sorters;
    this.store.dispatch(
      createGetPageTableAction(this.table.tableRepository)({
        page: resetPagination ? 1 : this.table.currentPage,
        size: this.table.pageSize,
        filter: filter,
        sort: sorters,
      })
    );
  }

  private listenSearchPageChange() {
    const selectRequestParams = createSelector(
      createFeatureSelector<any>(this.ngrxFeatureKey),
      (state: any) => {
        return state?.[this.table.tableRepository]
          ?.searchPage;
      }
    );
    this.store
      .select(selectRequestParams)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((page) => {
        if (this.table.currentPage !== page && page !== undefined) {
          this.table.currentPage = page;
          this.table.pageChange.emit(page);
        }
      });
  }

  private initStaticFilter() {
    if (this.staticFilter) {
      this.store.dispatch(
        createStaticFilterTableAction(this.table.tableRepository)({
          filter: this.staticFilter,
        })
      );
    }
  }

  private listenPageChange(): void {
    this.table.pageChange
      .pipe(takeUntil(this.destroyed$), skip(1))
      .subscribe((page) => {
        if (page === this.table.page?.number) {
          return;
        }
        const filter = {
          ...this.externalFilterComponent?.createCompleteData(),
          ...this.staticFilter,
        };
        const sorters = this.table.sorters;
        this.store.dispatch(
          createGetPageTableAction(this.table.tableRepository)({
            page: page,
            size: this.table.pageSize,
            filter: filter,
            sort: sorters,
          })
        );
      });
  }

  private listenIncomeData() {
    const selectIncomeData = createSelector(
      createFeatureSelector<any>(this.ngrxFeatureKey),
      (state: any) => {
        return (state?.[this.table.tableRepository] as TableState<any>)?.gPage;
      }
    );
    this.store
      .select(selectIncomeData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((page) => {
        this.table.page = page
          ? { ...page, size: this.table.pageSize }
          : undefined;
        this.table.updateData();
      });
  }

  private loadInitedTableState() {
    const selectTableState = createSelector(
      createFeatureSelector<any>(this.ngrxFeatureKey),
      (state: any) => {
        return state?.[this.table.tableRepository] as TableState<any>;
      }
    );
    this.store
      .select(selectTableState)
      .pipe(take(1))
      .subscribe((tableState) => {
        this.tableState = tableState;
        this.table.pageSize = tableState.requestParams?.size || 50;
        this.table.currentPage = tableState.requestParams?.page || 1;
      });
  }

  private listenExternalComponent() {
    if (this.externalFilterComponent && !this.formChanged$) {
      this.externalFilterComponent.formSubmit
        .pipe(takeUntil(this.destroyed$))
        .subscribe(() => {
          this.submit(true);
        });
    }
  }

  private listenExternalComponent2() {
    if (this.externalFilterComponent2 && this.formChanged$) {
      this.externalFilterComponent2.formSubmit
        .pipe(takeUntil(this.destroyed$))
        .subscribe(() => {
          this.submit(true);
        });
    }
  }

  private listenSorter() {
    this.table.sortChange
      .pipe(takeUntil(this.destroyed$))
      .subscribe((sorter) => {
        this.table.sorters = sorter;
        this.submit(false);
      });
  }

  private listenLoadingData() {
    const selectLoadingData = createSelector(
      createFeatureSelector<any>(this.ngrxFeatureKey),
      (state: any) => {
        return state?.[this.table.tableRepository] as TableState<any>;
      }
    );

    this.store
      .select(selectLoadingData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((tableState) => {
        this.table.loading = tableState.dataLoading;
        this.table.cdRef.detectChanges();
      });
  }
}
