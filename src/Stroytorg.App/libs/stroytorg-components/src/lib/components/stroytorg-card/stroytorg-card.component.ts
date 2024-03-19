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
import { ButtonStyle, StroytorgButtonComponent } from '../stroytorg-button';
import { Icon } from '@stroytorg/shared';
import {
  FilterDefinition,
  StroytorgCardElementComponent,
  StroytorgCardFilterComponent,
} from './components';
import { CardRowDefinition } from './stroytorg-card.models';

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
  ],
  templateUrl: './stroytorg-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgCardComponent<T> {
  cdRef = inject(ChangeDetectorRef);

  @ViewChild('cardsContainer') cardsContainer!: ElementRef;

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

  @Input()
  filters: FilterDefinition[] = [];

  @Output()
  pageChange = new EventEmitter<number>();

  get totalPages(): number {
    return Math.ceil(this.total / this.pageSize);
  }

  buttonStyle = ButtonStyle;

  buttonIcon = Icon;

  pageChanged(paginatorState: any): void {
    if (this.currentPage != paginatorState.page) {
      this.currentPage = paginatorState.page;
      this.total = paginatorState.totalRecords;
      this.pageSize = paginatorState.rows;
    }
    this.pageChange.emit(this.currentPage);
    this.cdRef.detectChanges();
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
