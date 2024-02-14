import {
  Component,
  Input,
  Output,
  EventEmitter,
  Optional,
  Self,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ControlValueAccessor,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  ErrorPipe,
  FloatingHintDirective
} from '@stroytorg/shared';
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
      useExisting: StroytorgSelectComponent,
    },
  ],
})
export class StroytorgSelectComponent<T>
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  @Input()
  bindLabel: string = 'label';

  @Input()
  bindValue: string = 'value';

  @Input()
  items!: T[];

  @Input()
  multiple: boolean = false;

  @Input()
  clearable: boolean = true;

  @Output() valueChange = new EventEmitter<string>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }
}
