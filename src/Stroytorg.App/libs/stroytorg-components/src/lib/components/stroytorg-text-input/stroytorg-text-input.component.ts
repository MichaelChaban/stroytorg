import { ChangeDetectionStrategy, Component, EventEmitter, Input, Optional, Output, Self, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, NgControl, ReactiveFormsModule } from '@angular/forms';
import { StroytorgBaseFormInputComponent, StroytorgBaseInputControls } from '../stroytorg-base-form';
import { InputSize, InputType } from './stroytorg-text-input.models';
import { ErrorPipe, FloatingHintDirective } from '@stroytorg/shared';

@Component({
  selector: 'stroytorg-text-input',
  standalone: true,
  imports: [CommonModule, ErrorPipe, ReactiveFormsModule, FloatingHintDirective],
  templateUrl: './stroytorg-text-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgTextInputComponent,
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

  @Input()
  size = InputSize.DEFAULT as string;

  @Output() valueChange = new EventEmitter<string>();

  id = (Math.random() + 1).toString(36).substring(7);

  @Input() type = InputType.SINGLELINE as string;

  InputSize = this.size;
}
