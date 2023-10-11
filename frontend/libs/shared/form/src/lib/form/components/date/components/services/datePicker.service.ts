import { Day, Month } from "@frontend/shared/domain";
import { DatePicker } from "../models";
import { Injectable } from "@angular/core";

const DAYS_IN_PICKER = 42;

@Injectable({
    providedIn: 'root'
})
export class DatePickerService {

    private currentYear!: number;
    private currentMonthIndex!: number;

    constructor() {
        this.initializeDate();
    }

    public getCurrentMonth(): DatePicker[] {
        return this.getMonth(this.currentMonthIndex, this.currentYear);
    }

    public getMonth(monthIndex: number, year: number): DatePicker[] {
        const days = [];
        const firstday = this.createDatePicker(0, monthIndex, year);
        for (let i = firstday.weekDayNumber; i > 0; i--) {
            const previousMonthIndex = monthIndex - 1 < 0 ? 11 : monthIndex - 1;
            const previousYear = previousMonthIndex === 11 ? year - 1 : year;
            const previosMonthDaysCount = new Date(previousYear, previousMonthIndex, 0).getDate();
            days.push(this.createDatePicker(previosMonthDaysCount - 1 - i, previousMonthIndex, previousYear));
        }
        days.push(firstday);

        const countDaysInMonth = new Date(year, monthIndex, 0).getDate() - 1;
        for (let i = 1; i < countDaysInMonth + 1; i++) {
            days.push(this.createDatePicker(i, monthIndex, year));
        }

        for (let i = 0; days.length < DAYS_IN_PICKER; i++) {
            const nextMonthIndex = monthIndex + 1 > 11 ? 0 : monthIndex + 1;
            const nextYear = nextMonthIndex === 0 ? year + 1 : year;
            days.push(this.createDatePicker(i, nextMonthIndex, nextYear));
        }

        return days;
    }

    public getMonthName(monthIndex: number) {
        return Month[monthIndex];
    }

    public getWeekDayShortName(weekDayIndex: number) {
        return Day[weekDayIndex].slice(0, 2);
    }

    public getWeekDayFullName(weekDayIndex: number) {
        return Day[weekDayIndex];
    }

    private initializeDate(){
        const date = new Date();
        this.currentYear = date.getFullYear();
        this.currentMonthIndex = date.getMonth();
    }

    private createDatePicker(dayNumber: number, monthIndex: number, year: number) {
        const weekDayNumber = new Date(year, monthIndex, dayNumber).getDay();
        return {
            monthIndex: monthIndex,
            month: this.getMonthName(monthIndex),
            dayNumber: dayNumber + 1,
            year: year,
            weekDayNumber: weekDayNumber,
            weekDayName: this.getWeekDayShortName(weekDayNumber)
        } as DatePicker;
    }
}