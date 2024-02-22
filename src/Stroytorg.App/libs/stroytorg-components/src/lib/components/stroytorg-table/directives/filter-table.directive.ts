import { Directive, Input, OnInit, Self } from '@angular/core';
import { Store } from '@ngrx/store';
import { UnsubscribeControlComponent } from '@stroytorg/shared';
import { debounceTime, skip, takeUntil } from 'rxjs';
import { StroytorgBaseFormComponent } from '../../stroytorg-base-form';
import { createFilterPageTableAction } from '../state-helpers/actions';

@Directive({
  // eslint-disable-next-line @angular-eslint/directive-selector
  selector: '[tableFilterStore]',
  standalone: true,
})
export class TableFilterStoreDirective
  extends UnsubscribeControlComponent
  implements OnInit
{
  @Input()
  tableRepository!: string;

  @Input()
  store!: Store;

  constructor(@Self() readonly form: StroytorgBaseFormComponent<any>) {
    super();
  }

  ngOnInit(): void {
    if (!this.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }
    if (this.form) {
      this.init();
    }
  }

  private init() {
    this.form.dataChange
      .pipe(skip(1), debounceTime(600), takeUntil(this.destroyed$))
      .subscribe((data) => {
        this.store.dispatch(
          createFilterPageTableAction(this.tableRepository)({ filter: data })
        );
      });
  }
}
