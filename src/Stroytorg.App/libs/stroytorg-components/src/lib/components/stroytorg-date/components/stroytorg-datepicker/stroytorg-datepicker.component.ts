/* eslint-disable @angular-eslint/component-selector */
import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { DEFAULT_DATE, DatePickerDate } from './stroytorg-datepicker.models';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { StroytorgDatePickerFacade } from './stroytorg-datepicker.facade';
import { StroytorgButtonComponent } from '../../../stroytorg-button';
import { StroytorgSelectComponent } from '../../../stroytorg-select';

@Component({
  selector: 'stroytorg-datepicker',
  templateUrl: './stroytorg-datepicker.component.html',
  standalone: true,
  imports: [CommonModule, MatIconModule, StroytorgButtonComponent, StroytorgSelectComponent, ReactiveFormsModule],
  providers: [StroytorgDatePickerFacade],
})
export class StroytorgDatePickerComponent implements OnInit {
  
  viewFacade = inject(StroytorgDatePickerFacade);

  @Input()
  initialDate?: Date | null;

  @Output()
  datePickerChanges = new EventEmitter<Date>();

  ngOnInit(): void {
    this.viewFacade.init(this.initialDate ? this.initialDate : null);
  }

  selectDate(date: DatePickerDate): void {
    this.viewFacade.selectDate(date);
    this.datePickerChanges.emit(new Date(date.year, date.monthIndex, date.monthDayNumber + 1));
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