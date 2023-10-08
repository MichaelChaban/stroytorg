/* eslint-disable @typescript-eslint/no-empty-function */
import { ChangeDetectionStrategy, Component, Input, OnInit } from "@angular/core";
import { BaseInputControls } from "./base-form-input-controls";
import { AbstractControl, ControlValueAccessor, FormControl, FormControlDirective, FormControlName, FormGroupDirective, NgControl, NgModel } from "@angular/forms";

const noFn = () => { };

@Component({
    template: '',
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BaseFormControlInputComponent implements BaseInputControls<any>, ControlValueAccessor, OnInit {

    readonly ngControl!: NgControl;

    formControl!: FormControl;

    constructor(private readonly control: NgControl) {
        this.ngControl = control;
        if (this.control) {
            this.ngControl.valueAccessor = this;
        }
    }

    @Input()
    value?: any;

    @Input()
    label!: string;

    @Input()
    required!: boolean;

    @Input()
    readonly!: boolean;

    @Input()
    disabled!: boolean;

    onChange = noFn;

    onTouch = noFn;

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouch = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    writeValue(value: any): void { }

    ngOnInit(): void {
        if (this.ngControl instanceof FormControlName) {
            if (this.ngControl.control) {
                this.formControl = this.ngControl.control;
            } else {
                const formGroupDirective = this.ngControl
                    .formDirective as FormGroupDirective;
                if (formGroupDirective) {
                    this.formControl = formGroupDirective.form.controls[
                        this.ngControl.name as string
                    ] as FormControl;
                }
            }
        } else if (this.ngControl instanceof FormControlDirective) {
            this.formControl = this.ngControl.control;
        } else if (this.ngControl instanceof NgModel) {
            this.formControl = this.ngControl.control;
            this.formControl.valueChanges.subscribe(() =>
                this.ngControl.viewToModelUpdate(this.control.value)
            );
        } else if (!this.ngControl) {
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