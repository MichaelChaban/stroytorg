import { Injectable } from '@angular/core';
import { DatePickerService } from './services';
import { Icon } from '@frontend/shared/domain';
import { DatePickerDate, WEEK_DAYS_NANE } from './models';

@Injectable()
export class DatePickerViewFacade {

  previosMonthIcon = Icon.DOUBLE_LEFT;
  nextMonthIcon = Icon.DOUBLE_RIGHT;
  weekDaysName = WEEK_DAYS_NANE;

  monthDays!: DatePickerDate[];
  monthNumber!: number;
  year!: number;

  selectedDate!: DatePickerDate;

  constructor(readonly datePickerService: DatePickerService) {}

  init() {
    this.setMonthDays(this.datePickerService.getCurrentMonth());
  }

  getMonthName() {
    return this.datePickerService.getMonthName(this.monthNumber);
  }

  selectDate(date: DatePickerDate) {
    this.selectedDate = date;
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
    this.monthDays = days;
    const currectMonthDate = this.monthDays.find(
      (x) => x.monthDayNumber === 0
    )!;
    this.monthNumber = currectMonthDate.monthIndex;
    this.year = currectMonthDate.year;
    this.selectedDate = this.selectedDate
      ? this.selectedDate
      : this.monthDays.find((x) => x.classNames === 'today')!;
  }
}
