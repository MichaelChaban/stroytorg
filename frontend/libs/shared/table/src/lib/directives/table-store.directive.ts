import { AfterViewInit, Directive, inject, Input, OnInit } from '@angular/core';
import { UnsubscribeControlComponent } from '@frontend/shared/domain';
import { TableComponent } from '../table/table.component';
import { ButtonComponent } from '@frontend/shared/button';
import { takeUntil } from 'rxjs';


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

  table = inject(TableComponent, { self: true });

  @Input() autostart = false;
  @Input() ngrxFeatureKey!: string;
  @Input() submitButton!: ButtonComponent;

  @Input() staticFilter!: Partial<any>;


  ngOnInit(): void {
    if (!this.table.tableRepository) {
      throw new Error('Property tableRepository must be defined.');
    }
    this.listenPageChange();
  }

  ngAfterViewInit(): void {
    if (this.autostart) {
      this.submit();
    }
  }


  listenPageChange() {
    const filter = { ...this.staticFilter };
    this.table.pageChange.pipe(takeUntil(this.destroyed$))
    .subscribe(x => console.log('Page Changed: ' + x + ' ' + JSON.stringify(filter)))
  }

  submit() {
    console.log(this.staticFilter + ' ' + this.table.tableRepository);
  }
}