import { Injectable, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Icon } from '@stroytorg/shared';
import { WEEK_DAYS_NANE, MONTH_OPTIONS, DatePickerDate, patchValueToModel, createFormGroup, YEAR_OPTIONS, dateToPickerDate, areDatesEqual } from './stroytorg-datepicker.models';
import { StroytorgDatePickerService } from './stroytorg-datepicker.service';
import { SelectSize } from '../../../stroytorg-select';

@Injectable()
export class StroytorgDatePickerFacade {

  readonly datePickerService = inject(StroytorgDatePickerService);

  formGroup!: FormGroup;

  previosMonthIcon = Icon.DOUBLE_LEFT;
  nextMonthIcon = Icon.DOUBLE_RIGHT;
  weekDaysName = WEEK_DAYS_NANE;
  monthOptions = MONTH_OPTIONS;
  yearOptions = YEAR_OPTIONS;

  selectSize = SelectSize;

  monthDates!: DatePickerDate[];
  monthNumber!: number;
  year!: number;

  selectedDate: DatePickerDate | null = null;
  todayDate!: DatePickerDate;

  init(initialDate?: Date | null) {
    this.selectedDate = dateToPickerDate(initialDate);
    this.setMonthDays(this.datePickerService.getCurrentMonth(this.selectedDate?.monthIndex, this.selectedDate?.year));
    this.todayDate = this.monthDates.find(date => date.isToday)!;
    this.initFormGroup();
  }

  getMonthName() {
    return this.datePickerService.getMonthName(this.monthNumber);
  }

  get monthHeader(){
    let currentDate = this.monthDates.find(date => date.isCurrentMonth);
    if(!currentDate){
      currentDate = this.monthDates.find(date => date);
    }

    return `${currentDate!.monthName}, ${currentDate!.year}`;
  }

  selectDate(date: DatePickerDate) {
    this.selectedDate = { ...date, isSelected: true };
    const previousSelectedDateIndex = this.monthDates.findIndex(monthDate => monthDate.isSelected);
    if (previousSelectedDateIndex !== -1) {
      this.monthDates[previousSelectedDateIndex].isSelected = false;
    }
    const newSelectedDateIndex = this.monthDates.findIndex(monthDate => monthDate === date);
    if (newSelectedDateIndex !== -1) {
      this.monthDates[newSelectedDateIndex].isSelected = true;
    }
    patchValueToModel(this.formGroup, this.selectedDate);
  }

  previousMonth() {
    this.monthNumber--;
    if (this.monthNumber < 0) {
      this.monthNumber = 11;
      this.year--;
    }

    return this.setMonthDays(
      this.datePickerService.getMonth(this.monthNumber, this.year)
    );
  }

  nextMonth() {
    this.monthNumber++;
    if (this.monthNumber > 11) {
      this.monthNumber = 0;
      this.year++;
    }
    return this.setMonthDays(
      this.datePickerService.getMonth(this.monthNumber, this.year)
    );
  }

  private setMonthDays(days: DatePickerDate[]) {
    this.monthDates = days;
    const currectMonthDate = this.monthDates.find(
      (x) => x.monthDayNumber === 0
    )!;
    this.monthNumber = currectMonthDate.monthIndex;
    this.year = currectMonthDate.year;
    const selectedDateIndex = this.monthDates.findIndex(date => areDatesEqual(date, this.selectedDate));
    if (selectedDateIndex !== -1) {
      this.monthDates[selectedDateIndex].isSelected = true;
    }
  }

  private initFormGroup() {
    this.formGroup = createFormGroup(this.selectedDate ? this.selectedDate : this.todayDate);
    this.formGroup.valueChanges.subscribe({
      next: (x) => {
        this.setMonthDays(
          this.datePickerService.getMonth(x.month, x.year)
        );
      }
    });
  }
}