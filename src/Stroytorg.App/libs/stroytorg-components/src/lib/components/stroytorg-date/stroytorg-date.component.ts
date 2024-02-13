import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Optional,
  Output,
  Self,
  SimpleChanges,
  ViewEncapsulation,
  inject,
} from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DateSize } from './stroytorg-date.models';
import {
  ControlValueAccessor,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { ErrorPipe, FloatingHintDirective } from '@stroytorg/shared';
import {
  StroytorgBaseInputControls,
  StroytorgBaseFormInputComponent,
} from '../stroytorg-base-form';
import { StroytorgDatePickerComponent } from './components';

@Component({
  selector: 'stroytorg-date',
  standalone: true,
  imports: [
    CommonModule,
    ErrorPipe,
    DatePipe,
    ReactiveFormsModule,
    FloatingHintDirective,
    StroytorgDatePickerComponent,
  ],
  templateUrl: './stroytorg-date.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
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
export class StroytorgDateComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  @Input()
  size = DateSize.DEFAULT as string;

  @Output()
  valueChange = new EventEmitter<string>();

  datePipe = inject(DatePipe);

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  get formattedValue() {
    return this.formatDate(this.formControl?.value);
  }

  protected formatDate(dateString: string): string | null {
    if (!dateString) {
      return null;
    }

    const date = new Date(dateString);
    return date ? this.datePipe?.transform(date, 'yyyy-MM-dd') : dateString;
  }

  selectDate(date: Date): void {
    if (this.formControl && !this.readonly) {
      this.formControl.setValue(this.formatDate(date?.toISOString()));
    }
  }

  getInitialDate() {
    return this.formControl?.value ? new Date(this.formControl?.value) : null;
  }
}
