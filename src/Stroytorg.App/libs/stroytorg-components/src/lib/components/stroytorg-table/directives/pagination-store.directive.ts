import {
  ChangeDetectorRef,
  Directive,
  Input,
  OnChanges,
  OnInit,
  Optional,
  SimpleChanges,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { UnsubscribeControlComponent } from '@stroytorg/shared';
import { createGetPageTableAction } from '../state-helpers/actions';
import { StroytorgTableComponent } from '../stroytorg-table.component';
import { Page } from '../stroytorg-table.models';

@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: 'stroytorg-table[usePagination]',
  standalone: true,
})
export class PaginationStoreDirective<T>
  extends UnsubscribeControlComponent
  implements OnInit, OnChanges
{
  @Input()
  tableRepository!: string;

  @Input()
  gPage!: Page<T>;

  @Input()
  store!: Store;

  constructor(
    @Optional() private readonly table: StroytorgTableComponent<any>,
    private cd: ChangeDetectorRef
  ) {
    super();
  }

  ngOnInit(): void {
    if (!this.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }

    this.table.pageChange.subscribe((page) => {
      this.store.dispatch(
        createGetPageTableAction(this.tableRepository)({ page: page, size: 50 })
      );
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['page']) {
      this.table.currentPage = this.gPage.number;
      this.table.pageSize = this.gPage.size;
      this.table.total = this.gPage.count;
      this.table.data = this.gPage.list;
      this.cd.detectChanges();
    }
  }
}
