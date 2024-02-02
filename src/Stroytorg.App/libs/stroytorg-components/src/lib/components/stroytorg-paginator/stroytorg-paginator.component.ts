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
import { PaginatorPageModel } from './stroytorg-paginator.models';

@Component({
  selector: 'stroytorg-paginator',
  standalone: true,
  imports: [CommonModule],
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

  paginatorState: any;

  pageCount!: number;

  constructor(private cdRef: ChangeDetectorRef) {}

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['totalRecords'] || changes['currentPage']) {
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
    const startPage = Math.max(this.currentPage - 1, 1);
    const endPage = Math.min(this.currentPage + 1, this.pageCount);

    for (let i = startPage; i <= endPage; i++) {
      pageArray.push({ page: i, visible: true });
    }
    return pageArray;
  }


  pageChange(event: any) {
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
