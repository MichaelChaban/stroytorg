import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  HostListener,
  Optional,
  Output,
  Self,
  ViewEncapsulation
} from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ControlValueAccessor, NgControl, ReactiveFormsModule } from '@angular/forms';
import {
  StroytorgBaseFormInputComponent,
  StroytorgBaseInputControls,
} from '../stroytorg-base-form';
import { ErrorPipe, FloatingHintDirective } from '@stroytorg/shared';
import { AVAILABLE_HOURS, StroytorgTimePickerComponent } from './components';

@Component({
  selector: 'stroytorg-time',
  standalone: true,
  imports: [
    CommonModule,
    ErrorPipe,
    DatePipe,
    ReactiveFormsModule,
    FloatingHintDirective,
    StroytorgTimePickerComponent
  ],
  templateUrl: './stroytorg-time.component.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgTimeComponent,
    },
  ],
})
export class StroytorgTimeComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  @Output()
  valueChange = new EventEmitter<string>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }
  
  get formattedValue() {
    return this.formatTime(this.formControl?.value);
  }

  protected formatTime(id: number | string): string | null {
    if (!id || typeof id === 'string') {
      return typeof id === 'string' ? id : null;
    }

    const selectedTime = AVAILABLE_HOURS.find(x => x.id === id);
    return selectedTime ? `${selectedTime.title}:00` : null;
  }

  @HostListener('keydown', ['$event'])
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onKeyDown(event: any) {
    event.preventDefault();
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  selectTime(timeId: number): void {
    if (this.formControl && !this.readonly) {
      this.formControl.setValue(timeId);
    }
  }
}
