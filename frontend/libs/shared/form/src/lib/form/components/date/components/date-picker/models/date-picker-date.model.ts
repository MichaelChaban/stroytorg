export interface DatePickerDate {
    year: number;
    month: string;
    monthIndex: number;
    monthDayNumber: number;
    weekDayNumber: number;
    weekDayShortName: string;
    weekDayFullName: string;
    className: string;
}

export const WEEK_DAYS_NANE = ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'];

export const MONTHS_NAMES = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];