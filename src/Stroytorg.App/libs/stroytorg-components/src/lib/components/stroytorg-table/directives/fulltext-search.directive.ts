import {
  Directive,
  inject,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { UnsubscribeControlComponent, CurrentSearch } from '@stroytorg/shared';
import { takeUntil } from 'rxjs';
import { StroytorgBaseFormComponent } from '../../stroytorg-base-form';
import { createGetPageTableAction } from '../state-helpers/actions';
import { StroytorgTableComponent } from '../stroytorg-table.component';

@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: 'stroytorg-table[useTableFullTextSearch]',
  exportAs: 'stroytorgTableStore',
  standalone: true,
})
export class FullTextSearchDirective
  extends UnsubscribeControlComponent
  implements OnChanges, OnInit
{
  store = inject(Store);

  table = inject(StroytorgTableComponent, { self: true });

  @Input() staticFilter!: Partial<any>;
  @Input() externalFilterComponent!: StroytorgBaseFormComponent<any>;
  @Input() externalFilterComponent2!: StroytorgBaseFormComponent<any>;
  @Input() currentSearch!: CurrentSearch;

  ngOnInit(): void {
    if (!this.table.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }
    this.listenPageChanged();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentSearch'] && this.table.currentPage) {
      if (this.currentSearch.page !== this.table.currentPage) {
        this.callPage(this.currentSearch.page);
      }
    }
    if (
      changes['currentSearch']?.currentValue !==
      changes['currentSearch']?.previousValue
    ) {
      this.showRowBorder();
    }
  }

  callPage(page: number) {
    const filter = {
      ...this.externalFilterComponent?.createCompleteData(),
      ...this.externalFilterComponent2?.createCompleteData(),
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
  }

  listenPageChanged() {
    this.table.pageChange.pipe(takeUntil(this.destroyed$))
    .subscribe(() => {
      this.showRowBorder();
    })
  }

  showRowBorder() {
    this.table.tableRows.forEach((row, index) => {
      row.row[this.table.entityId] === this.currentSearch.id &&
      (this.table.getIndex(index) === this.currentSearch.index)
        ? (row.focused = true)
        : (row.focused = false);
    });
    const elementWithIndex = document.querySelector(
      `[row-index="${this.currentSearch.index}"]`
    );
    if (elementWithIndex) {
      const elementToScroll = elementWithIndex;
      const isOutOfSight =
        elementToScroll.getBoundingClientRect().bottom > window.innerHeight;
      isOutOfSight
        ? elementToScroll.scrollIntoView({
            behavior: 'smooth',
            block: 'center',
          })
        : elementToScroll.getBoundingClientRect().top < 0 ? elementToScroll.scrollIntoView({
          behavior: 'smooth',
          block: 'center',
        }) : () => { return }
    }
  }
  
}
