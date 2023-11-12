import { Day, Month } from "@frontend/shared/domain";
import { DatePickerDate } from "../models";
import { Injectable } from "@angular/core";

const DAYS_IN_PICKER = 42;

@Injectable({
    providedIn: 'root'
})
export class DatePickerService {

    private currentYear!: number;
    private currentMonthIndex!: number;
    private readonly TODAY = new Date();

    constructor() {
        this.initializeDate();
    }

    public getCurrentMonth(): DatePickerDate[] {
        return this.getMonth(this.currentMonthIndex, this.currentYear);
    }

    public getMonth(monthIndex: number, year: number): DatePickerDate[] {
        const days = [];
        const firstday = this.createDatePicker(0, monthIndex, year, 'current-month');
        const previousMonthIndex = monthIndex - 1 < 0 ? 11 : monthIndex - 1;
        const previousYear = previousMonthIndex === 11 ? year - 1 : year;
        const previosMonthDaysCount = new Date(previousYear, previousMonthIndex, 0).getDate();
        const nextMonthIndex = monthIndex + 1 > 11 ? 0 : monthIndex + 1;
        const nextYear = nextMonthIndex === 0 ? year + 1 : year;
        
        for (let i = firstday.weekDayNumber; i > 0; i--) {
            days.push(this.createDatePicker(previosMonthDaysCount - 1 - i, previousMonthIndex, previousYear, 'not-current-month'));
        }
        
        days.push(firstday);

        const countDaysInMonth = new Date(year, monthIndex, 0).getDate() - 1;
        for (let i = 1; i < countDaysInMonth + 1; i++) {
            days.push(this.createDatePicker(i, monthIndex, year, 'current-month'));
        }

        for (let i = 0; days.length < DAYS_IN_PICKER; i++) {
            days.push(this.createDatePicker(i, nextMonthIndex, nextYear, 'not-current-month'));
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

    private createDatePicker(monthDayNumber: number, monthIndex: number, year: number, className: string) {
        const weekDayNumber = new Date(year, monthIndex, monthDayNumber).getDay();
        const isToday = this.isToday(monthDayNumber, monthIndex, year);
        return {
            year: year,
            monthName: this.getMonthName(monthIndex),
            monthIndex: monthIndex,
            monthDayNumber: monthDayNumber,
            weekDayNumber: weekDayNumber,
            weekDayShortName: this.getWeekDayShortName(weekDayNumber + 1),
            weekDayFullName: this.getWeekDayFullName(weekDayNumber + 1),
            classNames: isToday ? 'today' : className
        } as DatePickerDate;
    }

    private isToday(monthDayNumber: number, monthIndex: number, year: number): boolean {
        const dateToCheck = new Date(year, monthIndex, monthDayNumber);
        return dateToCheck.getDate() === this.TODAY.getDate() - 1 &&
            dateToCheck.getMonth() === this.TODAY.getMonth() &&
            dateToCheck.getFullYear() === this.TODAY.getFullYear();
    }
}