import { Day, Month } from "@frontend/shared/domain";
import { DatePicker } from "../models";
import { Injectable } from "@angular/core";

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
        const firstday = this.createDatePicker(1, monthIndex, year);

        for (let i = 1; i < firstday.weekDayNumber; i++) {
            days.push({
                weekDayNumber: i,
                monthIndex: monthIndex,
                year: year,
            } as DatePicker);
        }
        days.push(firstday);

        const countDaysInMonth = new Date(year, monthIndex + 1, 0).getDate();
        for (let i = 2; i < countDaysInMonth + 1; i++) {
            days.push(this.createDatePicker(i, monthIndex, year));
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
            dayNumber: dayNumber,
            year: year,
            weekDayNumber: weekDayNumber,
            weekDayName: this.getWeekDayShortName(weekDayNumber)
        } as DatePicker;
    }
}