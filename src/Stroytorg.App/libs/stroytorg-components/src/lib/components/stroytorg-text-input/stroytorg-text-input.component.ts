import {
  ChangeDetectionStrategy,
  Component,
  Input,
  Optional,
  Self,
  ViewEncapsulation,
  forwardRef,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ControlValueAccessor,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  StroytorgBaseFormInputComponent,
  StroytorgBaseInputControls,
} from '../stroytorg-base-form';
import { InputType } from './stroytorg-text-input.models';
import { ErrorPipe, FloatingHintDirective } from '@stroytorg/shared';

@Component({
  selector: 'stroytorg-text-input',
  standalone: true,
  imports: [
    CommonModule,
    ErrorPipe,
    ReactiveFormsModule,
    FloatingHintDirective,
  ],
  templateUrl: './stroytorg-text-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: forwardRef(() => StroytorgTextInputComponent),
      multi: true
    },
  ],
})
export class StroytorgTextInputComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  @Input() type: InputType = 'singleline';

  get isInputNumber(): boolean {
    const value = this.formControl.value;
    return value !== null && value !== undefined && !isNaN(Number(value));
  }  
}
