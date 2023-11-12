export interface DatePickerDate {
    year: number;
    monthName: string;
    monthIndex: number;
    monthDayNumber: number;
    weekDayNumber: number;
    weekDayShortName: string;
    weekDayFullName: string;
    classNames: string;
}

export const WEEK_DAYS_NANE = ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'];

export const MONTHS_NAMES = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

export const MONTH_OPTIONS = [
    { label: 'Jan', value: 0 },
    { label: 'Feb', value: 1 },
    { label: 'Mar', value: 2 },
    { label: 'Apr', value: 3 },
    { label: 'May', value: 4 },
    { label: 'Jun', value: 5 },
    { label: 'Jul', value: 6 },
    { label: 'Aug', value: 7 },
    { label: 'Sep', value: 8 },
    { label: 'Oct', value: 9 },
    { label: 'Nov', value: 10 },
    { label: 'Dec', value: 11 }
  ];
  