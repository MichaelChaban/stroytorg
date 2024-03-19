import {
  Component,
  Input,
  Output,
  EventEmitter,
  Optional,
  Self,
  forwardRef,
} from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [
    CommonModule,
    ErrorPipe,
    ReactiveFormsModule,
    NgSelectModule,
    FloatingHintDirective,
  ],
  templateUrl: './stroytorg-select.component.html',
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: forwardRef(() => StroytorgSelectComponent),
      multi: true
    },
  ],
})
export class StroytorgSelectComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  @Input()
  bindLabel = 'label';

  @Input()
  bindValue = 'value';

  @Input()
  items!: any[];

  @Input()
  multiple = false;

  @Input()
  clearable = true;

  @Output() valueChange = new EventEmitter<string>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }
}
