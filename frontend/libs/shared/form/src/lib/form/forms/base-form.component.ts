import { AfterViewInit, ChangeDetectorRef, Directive, EventEmitter, Input, OnChanges, OnDestroy, Output, SimpleChanges } from '@angular/core';
import { FormState, isFormStateEqual } from './base-form.model';
import { Subject, takeUntil } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { cloneDeep } from 'lodash';

@Directive({
  standalone: true,
})
export abstract class BaseFormComponent<T extends object> implements OnChanges, OnDestroy, AfterViewInit {

  @Input() initialData!: T | any;
  @Output() dataChange = new EventEmitter<T>();
  @Output() statusChange = new EventEmitter<FormState>();

  formDestroyed$ = new Subject<void>();
  formGroup!: FormGroup;

  constructor(protected cd: ChangeDetectorRef) {}

  valueInitialization = false;

  lastState!: FormState;

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.formGroup) {
      this.initialFormGroup();
    } else if (changes['initialData']) {
      this.valueInitialization = true;
      const dirty = this.formGroup.dirty;
      this.formGroup.patchValue(this.fromModel(this.initialData), {
        emitEvent: false,
        onlySelf: true,
      });

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

  private initValueChanges() {
    this.formGroup.valueChanges
      .pipe(takeUntil(this.formDestroyed$))
      .subscribe(() => {
        if (!this.valueInitialization) {
          const change = this.toModel(this.formGroup.value);
          this.recursiveObjectAttributesTransformation(change);
          const updatedData = this.applyChanges(
            cloneDeep(this.initialData),
            cloneDeep(change)
          );

          this.dataChange.emit(updatedData);
        }
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

    Object.keys(obj).forEach((key) => {
      transformationFn(obj, key);

      // if object (includes array, exclude function object) - recursive traversal
      if (typeof obj[key] === 'object') {
        this.recursiveObjectAttributesTraversal(obj[key], transformationFn);
      }
    });
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
