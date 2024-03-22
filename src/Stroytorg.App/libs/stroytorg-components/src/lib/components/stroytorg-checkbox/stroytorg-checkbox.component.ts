import {
  Component,
  Input,
  ViewEncapsulation,
  ChangeDetectionStrategy,
  Output,
  EventEmitter,
  Optional,
  Self,
  OnChanges,
  SimpleChanges,
  forwardRef,
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
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: forwardRef(() => StroytorgCheckboxComponent),
    },
  ],
})
export class StroytorgCheckboxComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor, OnChanges
{
  @Input()
  checked = this.ngControl?.value;

  @Input()
  title?: string;

  @Output() valueChange = new EventEmitter<boolean>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  override ngOnChanges(changes: SimpleChanges): void {
    super.ngOnChanges(changes);
    if (changes['checked']) {
      this.checked = changes['checked'].currentValue;
      this.formControl?.setValue(this.checked);
    }
  }

  toggle(): void {
    if (this.formControl?.disabled) {
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
