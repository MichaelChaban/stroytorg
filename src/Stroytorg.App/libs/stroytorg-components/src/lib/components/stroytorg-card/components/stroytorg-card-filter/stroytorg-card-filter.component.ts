import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  inject,
} from '@angular/core';
import { FilterDefinition } from './stroytorg-card-filter.models';
import { CommonModule } from '@angular/common';
import { StroytorgCheckboxComponent } from '../../../stroytorg-checkbox';
import { StroytorgDateComponent } from '../../../stroytorg-date';
import { StroytorgTextInputComponent } from '../../../stroytorg-text-input';
import { StroytorgSelectComponent } from '../../../stroytorg-select';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { StroytorgRangeComponent } from '../../../stroytorg-range';
import { StroytorgButtonComponent } from '../../../stroytorg-button';
import { MobileService } from '@stroytorg/shared';
import { BehaviorSubject } from 'rxjs';

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
    StroytorgButtonComponent,
  ],
})
export class StroytorgCardFilterComponent implements OnInit {
  private readonly formBuilder = inject(FormBuilder);
  private readonly mobileService = inject(MobileService);

  @Input()
  filter: FilterDefinition[] = [];

  @Output()
  valueChange = new EventEmitter<any>();

  @Input()
  isFilterVisible$ = new BehaviorSubject<boolean>(false);

  formGroup!: FormGroup;

  get isMobile() {
    return this.mobileService.getIsMobile();
  }

  ngOnInit(): void {
    this.createFormGroup();
  }

  resetFilter() {
    this.formGroup.reset();
  }

  showFilter() {
    this.isFilterVisible$.next(!this.isFilterVisible$.value);
  }

  private createFormGroup() {
    const formControls: { [key: string]: any } = {};
    this.filter.forEach((filter) => {
      formControls[filter.label] = filter.value ? filter.value : null;
    });
    this.formGroup = this.formBuilder.group(formControls);
  }
}
