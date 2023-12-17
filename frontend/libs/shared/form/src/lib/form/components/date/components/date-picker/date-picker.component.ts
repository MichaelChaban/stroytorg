/* eslint-disable @angular-eslint/component-selector */
import { Component, OnInit, inject } from '@angular/core';
import { DatePickerDate } from './models';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ButtonComponent } from '@frontend/shared/button';
import { DatePickerViewFacade } from './date-picker.view-facade';
import { SelectComponent } from '../../../select/src';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'stroytorg-datepicker',
  templateUrl: './date-picker.component.html',
  standalone: true,
  imports: [CommonModule, MatIconModule, ButtonComponent, SelectComponent, ReactiveFormsModule],
  providers: [DatePickerViewFacade],
})
export class DatePickerComponent implements OnInit {
  
  viewFacade = inject(DatePickerViewFacade);

  ngOnInit(): void {
    this.viewFacade.init();
  }

  selectDate(date: DatePickerDate): void {
    this.viewFacade.selectDate(date);
  }

  getMonthName(): void {
    this.viewFacade.getMonthName();
  }

  nextMonth(): void {
    this.viewFacade.nextMonth();
  }

  previousMonth(): void {
    this.viewFacade.previousMonth();
  }
}
