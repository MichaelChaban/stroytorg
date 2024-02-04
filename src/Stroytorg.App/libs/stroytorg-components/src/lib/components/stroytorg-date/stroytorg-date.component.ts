import {
  CUSTOM_ELEMENTS_SCHEMA,
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Optional,
  Output,
  Self,
  ViewEncapsulation,
  inject,
} from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DateSize } from './stroytorg-date.models';
import { NgControl, ReactiveFormsModule } from '@angular/forms';
import { ErrorPipe } from '@stroytorg/shared';
import {
  StroytorgBaseInputControls,
  StroytorgBaseFormInputComponent,
} from '../stroytorg-base-form';

@Component({
  selector: 'stroytorg-date',
  standalone: true,
  imports: [CommonModule, ErrorPipe, DatePipe, ReactiveFormsModule],
  templateUrl: './stroytorg-date.component.html',
  styleUrls: ['./stroytorg-date.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgDateComponent,
    },
    {
      provide: DatePipe,
    },
  ],
})
export class StroytorgDateComponent extends StroytorgBaseFormInputComponent {
  @Input()
  size = DateSize.large as string;

  @Output()
  valueChange = new EventEmitter<string>();

  datePipe = inject(DatePipe);

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  protected formatDate(dateString: string): string | null {
    if (!dateString) {
      return null;
    }

    const date = new Date(dateString);
    return date ? this.datePipe?.transform(date, 'yyyy-MM-dd') : dateString;
  }
}
