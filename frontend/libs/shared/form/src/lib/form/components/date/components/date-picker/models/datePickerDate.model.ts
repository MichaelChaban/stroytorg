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