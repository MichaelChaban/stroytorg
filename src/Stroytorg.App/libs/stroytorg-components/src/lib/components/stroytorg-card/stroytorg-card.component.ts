/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { StroytorgPaginatorComponent } from '../stroytorg-paginator';
import { StroytorgLoaderComponent } from '../stroytorg-loader';
import { StroytorgButtonComponent } from '../stroytorg-button';
import { KeyOrFunctionPipe, MobileService } from '@stroytorg/shared';
import {
  FilterDefinition,
  StroytorgCardElementComponent,
  StroytorgCardFilterComponent,
} from './components';
import { CardDefinition } from './stroytorg-card.models';

@Component({
  selector: 'stroytorg-card',
  standalone: true,
  imports: [
    CommonModule,
    StroytorgCardElementComponent,
    StroytorgPaginatorComponent,
    StroytorgLoaderComponent,
    StroytorgButtonComponent,
    StroytorgCardFilterComponent,
    KeyOrFunctionPipe,
  ],
  templateUrl: './stroytorg-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgCardComponent<T> {
  private readonly mobileService = inject(MobileService);
  private readonly cdRef = inject(ChangeDetectorRef);

  @ViewChild('cardsContainer') cardsContainer!: ElementRef;

  @Input()
  cardDefinition!: CardDefinition;

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

  @Input()
  filter: FilterDefinition[] = [];

  @Output()
  pageChange = new EventEmitter<number>();

  @Output()
  filterChange = new EventEmitter<any>();

  get totalPages(): number {
    return Math.ceil(this.total / this.pageSize);
  }

  get isMobile() {
    return this.mobileService.getIsMobile();
  }

  pageChanged(paginatorState: any): void {
    if (this.currentPage != paginatorState.page) {
      this.currentPage = paginatorState.page;
      this.total = paginatorState.totalRecords;
      this.pageSize = paginatorState.rows;
    }
    this.pageChange.emit(this.currentPage);
    this.cdRef.detectChanges();
  }

  filterValueChange(value: any) {
    this.filterChange.emit(value);
  }

  // @HostListener('window:scroll', ['$event'])
  // onScroll() {
  //   this.checkIfScrolledToBottom();
  // }

  // checkIfScrolledToBottom() {
  //   if (!this.cardsContainer || !this.cardsContainer.nativeElement) {
  //     return;
  //   }
  //   const cardsContainer = this.cardsContainer.nativeElement;
  //   const lastCard = cardsContainer.querySelector('.stroytorg-card:last-child');
  //   if (lastCard) {
  //     const lastCardRect = lastCard.getBoundingClientRect().top;
  //     if (window.scrollY >= lastCardRect) {
  //       if (!this.loading) {
  //         this.pageChange.emit(this.currentPage + 1);
  //       }
  //     }
  //   }
  // }
}
