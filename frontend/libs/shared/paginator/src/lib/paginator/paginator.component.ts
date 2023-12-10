import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@frontend/shared/button';
import { PaginatorState } from './paginator.model';
import { PagedData } from '@frontend/shared/domain';

@Component({
  selector: 'stroytorg-paginator',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginatorComponent implements OnChanges {
  @Output() pageChange = new EventEmitter<PaginatorState>();

  @Input()
  page!: PagedData<any>;

  protected paginatorState: PaginatorState = { page: 0, size: 0, total: 0 };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['page']) {
      this.updatePaginatorState();
    }
  }

  previousPage() {
    if (this.paginatorState.page > 1) {
      this.paginatorState.page--;
      this.pageChange.emit(this.paginatorState);
    }
  }

  nextPage() {
    if (this.paginatorState.page < this.paginatorState.total) {
      this.paginatorState.page++;
      this.pageChange.emit(this.paginatorState);
    }
  }

  private updatePaginatorState() {
    this.paginatorState.page = this.page?.page;
    this.paginatorState.total = this.page?.pagedData?.total;
    this.paginatorState.size = this.page?.size;
  }
}
