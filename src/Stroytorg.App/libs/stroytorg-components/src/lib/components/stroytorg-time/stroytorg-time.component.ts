import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
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
import { TimeSize } from './stroytorg-time.models';
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
  @Input()
  size = TimeSize.DEFAULT as string;

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
