import { Injectable } from '@angular/core';
import { DatePickerDate, Day, Month, DAYS_IN_PICKER, checkToday } from './stroytorg-datepicker.models';

@Injectable({
  providedIn: 'root',
})
export class StroytorgDatePickerService {
  private currentYear!: number;
  private currentMonthIndex!: number;

  constructor() {
    this.initializeDate();
  }

  public getCurrentMonth(initialMonthIndex?: number, initialYear?: number): DatePickerDate[] {
    return this.getMonth(initialMonthIndex ? initialMonthIndex : this.currentMonthIndex, initialYear ? initialYear : this.currentYear);
  }

  public getMonth(monthIndex: number, year: number): DatePickerDate[] {
    const days = [];
    const firstday = this.createDatePicker(
      0,
      monthIndex,
      year,
      true
    );
    const previousMonthIndex = monthIndex - 1 < 0 ? 11 : monthIndex - 1;
    const previousYear = previousMonthIndex === 11 ? year - 1 : year;
    const previosMonthDaysCount = new Date(
      previousYear,
      previousMonthIndex,
      0
    ).getDate();
    const nextMonthIndex = monthIndex + 1 > 11 ? 0 : monthIndex + 1;
    const nextYear = nextMonthIndex === 0 ? year + 1 : year;

    for (let i = firstday.weekDayNumber; i > 0; i--) {
      days.push(
        this.createDatePicker(
          previosMonthDaysCount - 1 - i,
          previousMonthIndex,
          previousYear,
          false
        )
      );
    }

    days.push(firstday);

    const countDaysInMonth = new Date(year, monthIndex, 0).getDate() - 1;
    for (let i = 1; i < countDaysInMonth + 1; i++) {
      days.push(this.createDatePicker(i, monthIndex, year, true));
    }

    for (let i = 0; days.length < DAYS_IN_PICKER; i++) {
      days.push(
        this.createDatePicker(i, nextMonthIndex, nextYear, false)
      );
    }

    return days;
  }

  public getMonthName(monthIndex: number) {
    return Month[monthIndex];
  }

  public getWeekDayShortName(weekDayIndex: number) {
    return Day[weekDayIndex]?.slice(0, 2);
  }

  public getWeekDayFullName(weekDayIndex: number) {
    return Day[weekDayIndex];
  }

  private initializeDate() {
    const date = new Date();
    this.currentYear = date.getFullYear();
    this.currentMonthIndex = date.getMonth();
  }

  private createDatePicker(
    monthDayNumber: number,
    monthIndex: number,
    year: number,
    isCurrentMonth: boolean
  ) {
    const weekDayNumber = new Date(year, monthIndex, monthDayNumber).getDay();
    const isToday = checkToday(monthDayNumber, monthIndex, year);
    return {
      year: year,
      monthName: this.getMonthName(monthIndex),
      monthIndex: monthIndex,
      monthDayNumber: monthDayNumber,
      weekDayNumber: weekDayNumber,
      weekDayShortName: this.getWeekDayShortName(weekDayNumber),
      weekDayFullName: this.getWeekDayFullName(weekDayNumber),
      classNames: isCurrentMonth ? 'current-month' : 'not-current-month',
      isToday: isToday,
      isCurrentMonth: isCurrentMonth
    } as DatePickerDate;
  }
}
