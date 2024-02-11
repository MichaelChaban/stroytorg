import {
  Component,
  Input,
  ViewEncapsulation,
  ChangeDetectionStrategy,
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
import { ErrorPipe } from '@stroytorg/shared';
import {
  StroytorgBaseInputControls,
  StroytorgBaseFormInputComponent,
} from '../stroytorg-base-form';

@Component({
  selector: 'stroytorg-checkbox',
  standalone: true,
  imports: [CommonModule, ErrorPipe, ReactiveFormsModule],
  templateUrl: './stroytorg-checkbox.component.html',
  styleUrls: ['./stroytorg-checkbox.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgCheckboxComponent,
    },
  ],
})
export class StroytorgCheckboxComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor
{
  @Input()
  checked = this.ngControl?.value;

  @Input()
  title?: string;


  get isChecked(){
    this.checked = this.ngControl?.value;
    return this.ngControl?.value;
  }

  @Output() valueChange = new EventEmitter<boolean>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  toggle(): void {
    if (this.ngControl?.disabled) {
      return;
    }
    this.checked = !this.checked;
    this.valueChange.emit(this.checked);
    if (!this.formControl) {
      return;
    }

    this.formControl.markAsTouched();
    this.formControl.setValue(this.checked);
    if (this.isRequired()) {
      if (this.checked) {
        return this.formControl.setErrors(null);
      }
      return this.formControl.setErrors({ required: true });
    }
  }
}
