import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
  Optional,
  Self,
  ViewEncapsulation,
  forwardRef,
} from '@angular/core';
import {
  StroytorgBaseFormInputComponent,
  StroytorgBaseInputControls,
} from '../stroytorg-base-form';
import {
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { StroytorgTextInputComponent } from '../stroytorg-text-input';

@Component({
  selector: 'stroytorg-range',
  templateUrl: './stroytorg-range.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, StroytorgTextInputComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: forwardRef(() => StroytorgRangeComponent)
    },
  ],
})
export class StroytorgRangeComponent
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor, OnInit
{
  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  @Input()
  rangeStep = 1;

  @Input()
  title = '';

  @Input()
  minValue!: number;

  @Input()
  maxValue!: number;

  @Input()
  twoSidesRange = true;

  @Input()
  showInputs = true;

  formGroup!: FormGroup;

  get formGroupMaxRangeValue(): number {
    if (!this.formGroup) {
      return this.maxValue;
    }
    const formGroupMaxRangeValue = this.formGroup.get('maxRange')?.value;
    return !formGroupMaxRangeValue && formGroupMaxRangeValue !== 0
      ? this.maxValue
      : formGroupMaxRangeValue;
  }

  get formGroupMinRangeValue(): number {
    if (!this.formGroup) {
      return this.minValue;
    }
    const formGroupMinRangeValue = this.formGroup.get('minRange')?.value;
    return !formGroupMinRangeValue && formGroupMinRangeValue !== 0
      ? this.minValue
      : formGroupMinRangeValue;
  }

  override ngOnInit() {
    super.ngOnInit();
    this.createFormGroup();
  }

  private createFormGroup() {
    this.formGroup = new FormGroup({
      minRange: new FormControl(this.minValue),
      maxRange: new FormControl(this.maxValue),
    });

    this.formGroup?.get('minRange')?.valueChanges.subscribe((value) => {
      this.minRangeValueChange(
        isNaN(Number(value ? +value : 'undefined')) ? undefined : value,
      );
    });
    this.formGroup?.get('maxRange')?.valueChanges.subscribe((value) => {
      this.maxRangeValueChange(
        isNaN(Number(value ? +value : 'undefined')) ? undefined : value,
      );
    });
  }

  private minRangeValueChange(minRangeValue?: number) {
    if (!minRangeValue && minRangeValue !== 0) {
      return;
    }
    if (minRangeValue >= this.formGroupMaxRangeValue) {
      const newMinRangeValue =
        minRangeValue >= this.formGroupMaxRangeValue
          ? this.formGroupMaxRangeValue
          : minRangeValue;
      this.formGroup.setValue(
        {
          minRange:
            newMinRangeValue < this.minValue ? this.minValue : newMinRangeValue,
          maxRange:
            newMinRangeValue < this.minValue ? this.minValue : newMinRangeValue,
        },
        { emitEvent: false },
      );
      return;
    }
    this.formGroup
      .get('minRange')
      ?.setValue(
        minRangeValue < this.minValue ? this.minValue : minRangeValue,
        { emitEvent: false },
      );
  }

  private maxRangeValueChange(maxRangeValue?: number) {
    if (!maxRangeValue && maxRangeValue !== 0) {
      return;
    }
    if (maxRangeValue <= this.formGroupMinRangeValue) {
      const newMaxRangeValue =
        maxRangeValue <= this.formGroupMinRangeValue
          ? this.formGroupMinRangeValue
          : maxRangeValue;
      this.formGroup.setValue(
        {
          minRange:
            newMaxRangeValue > this.maxValue ? this.maxValue : newMaxRangeValue,
          maxRange:
            newMaxRangeValue > this.maxValue ? this.maxValue : newMaxRangeValue,
        },
        { emitEvent: false },
      );
      return;
    }
    this.formGroup
      .get('maxRange')
      ?.setValue(
        maxRangeValue > this.maxValue ? this.maxValue : maxRangeValue,
        { emitEvent: false },
      );
  }
}
