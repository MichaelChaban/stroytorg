import { FormControl, FormGroup } from '@angular/forms';

export function createFormGroup(selectedDate?: DatePickerDate) {
  return new FormGroup({
    day: new FormControl(selectedDate?.monthDayNumber),
    month: new FormControl(selectedDate?.monthIndex),
    year: new FormControl(selectedDate?.year),
  });
}

export function patchValueToModel(form: FormGroup, date: DatePickerDate) {
  form.get('day')?.setValue(date.monthDayNumber);
  form.get('month')?.setValue(date.monthIndex);
  form.get('year')?.setValue(date.year);
}

export interface DatePickerDate {
  year: number;
  monthName: string;
  monthIndex: number;
  monthDayNumber: number;
  weekDayNumber: number;
  weekDayShortName: string;
  weekDayFullName: string;
  classNames: string;
  isToday: boolean;
  isCurrentMonth: boolean;
  isSelected?: boolean;
}

export const WEEK_DAYS_NANE = ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'];

export const MONTHS_NAMES = [
  'Jan',
  'Feb',
  'Mar',
  'Apr',
  'May',
  'Jun',
  'Jul',
  'Aug',
  'Sep',
  'Oct',
  'Nov',
  'Dec',
];

export const MONTH_OPTIONS = [
  { label: 'January', value: 0 },
  { label: 'February', value: 1 },
  { label: 'March', value: 2 },
  { label: 'April', value: 3 },
  { label: 'May', value: 4 },
  { label: 'June', value: 5 },
  { label: 'July', value: 6 },
  { label: 'August', value: 7 },
  { label: 'September', value: 8 },
  { label: 'October', value: 9 },
  { label: 'November', value: 10 },
  { label: 'December', value: 11 },
];

export const YEAR_OPTIONS = (() => {
  const currentYear = new Date().getFullYear();
  const startYear = currentYear - 10;
  const endYear = currentYear + 10;
  return Array.from(
    { length: endYear - startYear + 1 },
    (_, i) => startYear + i
  );
})();

export const TODAY = new Date();

export const DEFAULT_DATE : Date | null = null;

export enum Day {
  'Monday' = 1,
  'Tuesday' = 2,
  'Wednesday' = 3,
  'Thursday' = 4,
  'Friday' = 5,
  'Saturday' = 6,
  'Sunday' = 7,
}

export enum Month {
  'January' = 0,
  'February' = 1,
  'March' = 2,
  'April' = 3,
  'May' = 4,
  'June' = 5,
  'July' = 6,
  'August' = 7,
  'September' = 8,
  'October' = 9,
  'November' = 10,
  'December' = 11,
}

export const DAYS_IN_PICKER = 42;

export function dateToPickerDate(date?: Date | null) : DatePickerDate | null {
  if(!date){
    return null;
  }

  const convertedWeekDay = date.getDay() - 1 < 0 ? date.getDay() + 7 : date.getDay() - 1;

  const datePickerDate = {
    year: date.getFullYear(),
    monthName: MONTHS_NAMES[date.getMonth()],
    monthIndex: date.getMonth(),
    monthDayNumber: date.getDate() - 1,
    weekDayNumber: convertedWeekDay,
    weekDayShortName: Day[convertedWeekDay]?.slice(0, 2),
    weekDayFullName: Day[convertedWeekDay],
    classNames: '',
    isToday: false,
    isSelected: true
  } as DatePickerDate;
  
  datePickerDate.isToday = checkToday(datePickerDate.monthDayNumber, datePickerDate.monthIndex, datePickerDate.year);

  return datePickerDate;
}

export function checkToday(
  monthDayNumber: number,
  monthIndex: number,
  year: number
): boolean {
  const dateToCheck = new Date(year, monthIndex, monthDayNumber);
  return (
    dateToCheck.getDate() === TODAY.getDate() - 1 &&
    dateToCheck.getMonth() === TODAY.getMonth() &&
    dateToCheck.getFullYear() === TODAY.getFullYear()
  );
}

export function areDatesEqual(date1?: DatePickerDate | null, date2?: DatePickerDate | null): boolean {
  return date1?.year === date2?.year &&
         date1?.monthIndex === date2?.monthIndex &&
         date1?.monthDayNumber === date2?.monthDayNumber;
}