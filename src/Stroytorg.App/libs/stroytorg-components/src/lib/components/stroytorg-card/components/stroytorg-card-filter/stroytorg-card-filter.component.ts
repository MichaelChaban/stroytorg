import { Component, Input, OnInit, inject } from '@angular/core';
import { FilterDefinition } from './stroytorg-card-filter.models';
import { CommonModule } from '@angular/common';
import { StroytorgCheckboxComponent } from '../../../stroytorg-checkbox';
import { StroytorgDateComponent } from '../../../stroytorg-date';
import { StroytorgTextInputComponent } from '../../../stroytorg-text-input';
import { StroytorgSelectComponent } from '../../../stroytorg-select';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { StroytorgRangeComponent } from '../../../stroytorg-range';

@Component({
  selector: 'stroytorg-card-filter',
  templateUrl: './stroytorg-card-filter.component.html',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    StroytorgCheckboxComponent,
    StroytorgDateComponent,
    StroytorgTextInputComponent,
    StroytorgSelectComponent,
    StroytorgRangeComponent,
  ],
})
export class StroytorgCardFilterComponent implements OnInit {
  private readonly formBuilder = inject(FormBuilder);

  @Input()
  filters: FilterDefinition[] = [];

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.createFormGroup();
  }

  private createFormGroup() {
    const formControls: { [key: string]: any } = {};
    this.filters.forEach((filter) => {
      formControls[filter.label] = [filter.value];
    });
    this.formGroup = this.formBuilder.group(formControls);
  }
}
