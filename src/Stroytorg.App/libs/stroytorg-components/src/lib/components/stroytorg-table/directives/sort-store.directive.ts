import {
  AfterViewInit,
  Directive,
  Input,
  OnInit,
  Optional,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { UnsubscribeControlComponent } from '@stroytorg/shared';
import { takeUntil } from 'rxjs';
import { createSortPageTableAction } from '../state-helpers/actions';
import { StroytorgTableComponent } from '../stroytorg-table.component';
import { Sort } from '../stroytorg-table.models';

@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: 'stroytorg-table[useSort]',
  standalone: true,
})
export class SortStoreDirective<T>
  extends UnsubscribeControlComponent
  implements OnInit, AfterViewInit
{
  @Input()
  tableRepository!: string;

  @Input()
  store!: Store;

  constructor(@Optional() private readonly table: StroytorgTableComponent<T>) {
    super();
  }

  ngOnInit(): void {
    if (!this.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }
  }

  ngAfterViewInit() {
    this.table.sortChange.pipe(takeUntil(this.destroyed$)).subscribe((data) => {
      return this.store.dispatch(
        createSortPageTableAction(this.tableRepository)({
          sort: data as Sort<unknown>[],
        })
      );
    });
  }
}
