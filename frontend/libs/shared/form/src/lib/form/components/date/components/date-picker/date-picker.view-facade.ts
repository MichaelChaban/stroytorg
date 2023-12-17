import { Injectable, inject } from '@angular/core';
import { DatePickerService } from './services';
import { Icon } from '@frontend/shared/domain';
import { DatePickerDate, MONTHS_NAMES, MONTH_OPTIONS, WEEK_DAYS_NANE } from './models';
import { FormGroup } from '@angular/forms';
import { createFormGroup, patchValueToModel } from './date-picker.model';

@Injectable()
export class DatePickerViewFacade {

  readonly datePickerService = inject(DatePickerService);

  formGroup!: FormGroup;

  previosMonthIcon = Icon.DOUBLE_LEFT;
  nextMonthIcon = Icon.DOUBLE_RIGHT;
  weekDaysName = WEEK_DAYS_NANE;
  monthOptions = MONTH_OPTIONS;
  yearOptions = MONTHS_NAMES;

  monthDays!: DatePickerDate[];
  monthNumber!: number;
  year!: number;

  selectedDate!: DatePickerDate;

  init() {
    this.setMonthDays(this.datePickerService.getCurrentMonth());
    this.initFormGroup();
  }

  getMonthName() {
    return this.datePickerService.getMonthName(this.monthNumber);
  }

  selectDate(date: DatePickerDate) {
    this.selectedDate = date;
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
    this.monthDays = days;
    const currectMonthDate = this.monthDays.find(
      (x) => x.monthDayNumber === 0
    )!;
    this.monthNumber = currectMonthDate.monthIndex;
    this.year = currectMonthDate.year;
    this.selectedDate = this.selectedDate
      ? this.selectedDate
      : this.monthDays.find((x) => x.classNames.includes('today'))!;
  }

  private initFormGroup() {
    this.formGroup = createFormGroup(this.selectedDate);
    this.formGroup.valueChanges.subscribe({
      next: (x) => this.setMonthDays(
          this.datePickerService.getMonth(x.month, x.year)
        )
    });
  }
}
