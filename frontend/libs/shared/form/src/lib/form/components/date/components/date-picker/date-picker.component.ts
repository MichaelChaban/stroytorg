/* eslint-disable @angular-eslint/component-selector */
import { Component, OnInit } from '@angular/core';
import { DatePickerDate } from './models';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ButtonComponent } from '@frontend/shared/button';
import { DatePickerViewFacade } from './date-picker.view-facade';
import { MonthPickerComponent } from './components';

@Component({
  selector: 'stroytorg-datepicker',
  templateUrl: './date-picker.component.html',
  standalone: true,
  imports: [CommonModule, MatIconModule, ButtonComponent, MonthPickerComponent],
  providers: [DatePickerViewFacade],
})
export class DatePickerComponent implements OnInit {
  constructor(readonly viewFacade: DatePickerViewFacade) {}

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

  selectMonth(): void {
    this.viewFacade.selectMonth();
  }

  selectYear(): void {
    this.viewFacade.selectYear();
  }
}
