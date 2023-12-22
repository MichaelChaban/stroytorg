import { AfterViewInit, ChangeDetectorRef, Directive, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject, takeUntil, filter, map } from 'rxjs';
import { FormState, isFormStateEqual } from './stroytorg-base-form.models';

@Directive()
export abstract class StroytorgBaseFormComponent<T extends object>
  implements OnInit, OnChanges, OnDestroy, AfterViewInit
{
  @Input() initialData!: T | any;
  @Output() dataChange = new EventEmitter<T>();
  @Output() statusChange = new EventEmitter<FormState>();
  @Output() formSubmit = new EventEmitter<T>();
  @Output() formClear = new EventEmitter<void>();

  formDestroyed$ = new Subject<void>();
  formGroup!: FormGroup;

  constructor(protected cd: ChangeDetectorRef) {}

  valueInitialization = false;

  lastState!: FormState;

  ngOnInit(): void {
    if (!this.formGroup) {
      this.initialFormGroup();
      return;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.formGroup) {
      this.initialFormGroup();
      return;
    }

    if (changes['initialData']) {
      this.valueInitialization = true;
      const dirty = this.formGroup.dirty;

      this.formGroup.patchValue(this.fromModel(this.initialData), {
        emitEvent: false,
        onlySelf: true,
      });

      this.formGroup.updateValueAndValidity();

      if (!dirty) {
        this.formGroup.markAsPristine();
      }

      this.valueInitialization = false;

      this.createAndEmitIfFormStateChanged();
    }
  }

  abstract createFormGroup(): FormGroup;

  ngAfterViewInit() {
    this.cd.detectChanges();
  }

  ngOnDestroy(): void {
    this.formDestroyed$.next();
    this.formDestroyed$.complete();
  }

  createCompleteData() {
    const change = this.toModel(this.formGroup?.value);
    this.recursiveObjectAttributesTransformation(change);
    // const updatedData = this.applyChanges(
    //   cloneDeep(this.initialData),
    //   cloneDeep(change)
    // );

    return { ahoj: 'pojeb'} as unknown as T;
  }

  submit() {
    this.formGroup.markAllAsTouched();
    if (!this.formGroup.invalid) {
      this.formSubmit.emit(this.createCompleteData());
    }
    this.formGroup.markAsUntouched();
  }

  clear() {
    this.formClear.emit();
  }

  private initValueChanges() {
    this.formGroup.valueChanges
      .pipe(
        takeUntil(this.formDestroyed$),
        filter(() => !this.valueInitialization),
        map((value) => {
          const change = this.toModel(value);
          this.recursiveObjectAttributesTransformation(change);
          // const updatedData = this.applyChanges(
          //   cloneDeep(this.initialData),
          //   cloneDeep(change)

          // );
          return { ahoj: 'test'} as unknown as T;
        })
      )
      .subscribe((updatedData) => {
        this.dataChange.emit(updatedData);
      });
  }

  private initStatusChanges() {
    this.formGroup.statusChanges
      .pipe(takeUntil(this.formDestroyed$))
      .subscribe(() => {
        if (!this.valueInitialization) {
          this.createAndEmitIfFormStateChanged();
        }
      });

    this.createAndEmitIfFormStateChanged();
  }

  private createAndEmitIfFormStateChanged() {
    const formState: FormState = {
      valid: !this.formGroup.invalid,
      dirty: this.formGroup.dirty,
    };

    if (!isFormStateEqual(formState, this.lastState)) {
      this.lastState = formState;
      this.statusChange.emit(formState);
    }
  }

  protected applyChanges(data: T, changes: Partial<T>): T {
    return Object.assign(data || ({} as T), changes);
  }

  protected toModel(data: T): Partial<T> {
    return data || {};
  }

  protected fromModel(data: T): T {
    return { ...(data || {}) } as T;
  }

  private recursiveObjectAttributesTransformation(obj: any): void {
    this.recursiveObjectAttributesTraversal(
      obj,
      this.transformEmptyStringToNullStringFn
    );
  }

  private recursiveObjectAttributesTraversal(
    obj: any,
    transformationFn: (obj: any, key: string) => void
  ) {
    if (
      obj === null ||
      transformationFn === null ||
      typeof transformationFn !== 'function'
    ) {
      return;
    }

    const traverse = (obj: any) => {
      for (const key in obj) {
        // eslint-disable-next-line no-prototype-builtins
        if (obj.hasOwnProperty(key)) {
          transformationFn(obj, key);

          if (typeof obj[key] === 'object') {
            traverse(obj[key]);
          }
        }
      }
    };

    traverse(obj);
  }

  private transformEmptyStringToNullStringFn(obj: any, key: string) {
    // if empty string - transformation to null string
    if (typeof obj[key] === 'string' && obj[key] === '') {
      obj[key] = null;
    }
  }

  private initialFormGroup() {
    this.formGroup = this.createFormGroup();
    this.valueInitialization = true;
    this.formGroup.patchValue(this.fromModel(this.initialData), {
      emitEvent: false,
      onlySelf: true,
    });

    this.formGroup.markAsPristine();
    this.initValueChanges();
    this.initStatusChanges();
    this.valueInitialization = false;
  }
}
