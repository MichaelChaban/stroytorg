import { CommonModule } from '@angular/common';
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
  forwardRef,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import {
  StroytorgBaseFormInputComponent,
  StroytorgBaseInputControls,
} from '../stroytorg-base-form';
import { ErrorPipe, uuidv4 } from '@stroytorg/shared';
import { StroytorgRadioOption } from './stroytorg-radio.models';

@Component({
  selector: 'stroytorg-radio',
  templateUrl: 'stroytorg-radio.component.html',
  standalone: true,
  imports: [CommonModule, ErrorPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: forwardRef(() => StroytorgRadioComponent),
    },
  ],
})
export class StroytorgRadioComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  @Input()
  checked = this.ngControl?.value;

  @Input()
  options?: StroytorgRadioOption[] = [];

  // Required for radio groups
  @Input()
  radioName = uuidv4();

  @Output() valueChange = new EventEmitter<string>();

  toggle(value: any): void {
    if (this.ngControl?.disabled) {
      return;
    }
    this.checked = true;
    this.valueChange.emit(value);
    if (!this.formControl) {
      return;
    }
    this.formControl.markAsTouched();
    this.formControl.setValue(value);
    if (this.isRequired()) {
      if (this.checked) {
        return this.formControl.setErrors(null);
      }
      return this.formControl.setErrors({ required: true });
    }
  }
}
