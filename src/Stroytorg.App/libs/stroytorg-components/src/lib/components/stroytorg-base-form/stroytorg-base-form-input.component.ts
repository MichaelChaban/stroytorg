/* eslint-disable @typescript-eslint/member-ordering */
import { Component, Input, OnInit } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormControlDirective,
  FormControlName,
  FormGroupDirective,
  NgControl,
  NgModel,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StroytorgBaseInputControls } from './stroytorg-base-form-input-control.component';
// eslint-disable-next-line @typescript-eslint/no-empty-function
const noop = () => {};

@Component({
  template: '',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class StroytorgBaseFormInputComponent
  implements StroytorgBaseInputControls<any>, ControlValueAccessor, OnInit
{
  readonly ngControl: NgControl;

  formControl!: FormControl;

  constructor(private readonly control: NgControl) {
    this.ngControl = control;
    if (this.control) {
      this.ngControl.valueAccessor = this;
    }
  }

  @Input()
  label!: string;

  @Input()
  required!: boolean;

  @Input()
  readonly!: boolean;

  @Input()
  step!: number;

  @Input()
  min!: number;

  @Input()
  max!: number;

  @Input()
  hint!: string;

  @Input()
  disabled!: boolean;

  @Input()
  placeholder!: string;

  onChange: any = noop;

  onTouch: any = noop;

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  writeValue(value: any): void {}

  ngOnInit(): void {
    if (this.ngControl) {
      if (this.ngControl instanceof FormControlName) {
        this.formControl =
          this.ngControl.control ||
          ((this.ngControl.formDirective as FormGroupDirective)?.form.controls[
            this.ngControl.name as string
          ] as FormControl);
      } else if (
        this.ngControl instanceof FormControlDirective ||
        this.ngControl instanceof NgModel
      ) {
        this.formControl = this.ngControl.control;
        if (this.ngControl instanceof NgModel) {
          this.formControl.valueChanges.subscribe(() =>
            this.ngControl.viewToModelUpdate(this.control.value)
          );
        }
      } else {
        this.formControl = new FormControl();
      }
    }
    else {
      this.formControl = new FormControl();
    }
  }

  isRequired() {
    if (this.ngControl) {
      const validator = this.ngControl?.control?.validator?.(
        {} as AbstractControl
      );
      return this.required || (validator && validator['required']);
    }
    return this.required;
  }
}
