import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
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
import { StroytorgTimePickerComponent } from './components';

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

  selectTime(time: any): void {
    if (this.formControl && !this.readonly) {
      this.formControl.setValue(time);
    }
  }

  getInitialTime() {
    return this.formControl?.value ? new Date(this.formControl?.value) : null;
  }
}
