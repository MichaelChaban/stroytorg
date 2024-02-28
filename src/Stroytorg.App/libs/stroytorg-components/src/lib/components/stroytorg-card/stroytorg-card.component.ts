/* eslint-disable @typescript-eslint/no-explicit-any */
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StroytorgCardElementComponent } from './components/card-element.component';
import { CardRowDefinition } from './stroytorg-card.models';
import { StroytorgPaginatorComponent } from '../stroytorg-paginator';

@Component({
  selector: 'stroytorg-card',
  standalone: true,
  imports: [
    CommonModule,
    StroytorgCardElementComponent,
    StroytorgPaginatorComponent,
  ],
  templateUrl: './stroytorg-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgCardComponent<T> {

  cdRef = inject(ChangeDetectorRef);

  @Input()
  cardRowDefinition!: CardRowDefinition[];

  @Input()
  data!: T[];

  @Input()
  showPaginator = true;

  @Input()
  total!: number;

  @Input()
  loading!: boolean;

  @Input()
  pageSize!: number;

  @Input()
  currentPage!: number;

  @Output()
  pageChange = new EventEmitter<number>();

  pageChanged(paginatorState: any): void {
    if (this.currentPage != paginatorState.page) {
      this.currentPage = paginatorState.page;
      this.total = paginatorState.totalRecords;
      this.pageSize = paginatorState.rows;
    }
    this.pageChange.emit(this.currentPage);
    this.cdRef.detectChanges();
  }
}
