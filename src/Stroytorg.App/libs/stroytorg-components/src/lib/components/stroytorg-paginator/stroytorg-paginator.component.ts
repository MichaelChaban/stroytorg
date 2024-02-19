/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  Component,
  Input,
  OnInit,
  ViewEncapsulation,
  ChangeDetectorRef,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { PAGE_SIZE_OPTIONS, PaginatorPageModel } from './stroytorg-paginator.models';
import { StroytorgButtonComponent } from '../stroytorg-button';
import { Icon } from '@stroytorg/shared';
import { StroytorgSelectComponent } from '../stroytorg-select';
import { InputSize } from '../stroytorg-base-form';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'stroytorg-paginator',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent, StroytorgSelectComponent],
  templateUrl: './stroytorg-paginator.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class StroytorgPaginatorComponent implements OnInit, OnChanges {
  @Input()
  rows!: number;

  @Input()
  totalRecords!: number;

  @Input()
  currentPage = 1;

  @Output() pageChanged: EventEmitter<void> = new EventEmitter();

  icon = Icon;

  paginatorState: any;

  pageCount!: number;

  pageSizeOptions = PAGE_SIZE_OPTIONS;

  selectSize = InputSize;

  constructor(private cdRef: ChangeDetectorRef) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalRecords'] || changes['currentPage']) {
      this.updatePaginatorState();
    }
  }

  ngOnInit() {
    this.updatePaginatorState();
  }

  getPage(): number {
    return this.currentPage;
  }

  isFirstPage() {
    return this.getPage() == 1;
  }

  activePage(page: number): boolean {
    return this.getPage() == page;
  }

  isLastPage(): boolean {
    const pageArray = this.getPageArray();
    const lastPage = pageArray[pageArray.length - 1]?.page ?? 1;
    return this.getPage() === lastPage;
  }

  getPageCount(): number {
    this.pageCount = Math.ceil(this.totalRecords / this.rows);
    return this.pageCount;
  }

  getPageArray(): PaginatorPageModel[] {
    const pageArray: PaginatorPageModel[] = [];
    const startPage = Math.max(
      1,
      this.currentPage >= this.pageCount
        ? this.currentPage - 2
        : this.currentPage - 1
    );
    const endPage = Math.min(
      this.currentPage <= 1 ? this.currentPage + 2 : this.currentPage + 1,
      this.pageCount
    );

    for (let i = startPage; i <= endPage; i++) {
      pageArray.push({ page: i, visible: true });
    }

    if (startPage > 1) {
      pageArray.unshift({ page: 0, space: true, visible: true });
    }

    if (endPage < this.pageCount) {
      pageArray.push({ page: 0, space: true, visible: true });
    }

    return pageArray;
  }

  pageChange(event: any) {
    if (event.target.innerText.includes('...')) {
      return;
    }
    this.currentPage = Number.parseInt(event.target.innerText);
    this.updatePaginatorState();
  }

  nextPage(): void {
    this.currentPage += 1;
    this.updatePaginatorState();
  }

  prevPage(): void {
    this.currentPage -= 1;
    this.updatePaginatorState();
  }

  firstPage(): void {
    this.currentPage = 1;
    this.updatePaginatorState();
  }

  lastPage(): void {
    this.currentPage = this.pageCount;
    this.updatePaginatorState();
  }

  updatePaginatorState(emit = true) {
    this.paginatorState = {
      page: this.getPage(),
      pageCount: this.getPageCount(),
      rows: this.rows,
      totalRecords: this.totalRecords,
    };
    this.pageChanged.emit(this.paginatorState);
    if (emit) {
      this.cdRef.detectChanges();
    }
  }
}
