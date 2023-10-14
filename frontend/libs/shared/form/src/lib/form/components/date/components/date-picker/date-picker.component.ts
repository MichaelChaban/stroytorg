/* eslint-disable @angular-eslint/component-selector */
import { Component, OnInit } from "@angular/core";
import { DatePickerService } from "./services";
import { DatePickerDate, WEEK_DAYS_NANE } from "./models";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";
import { Icon } from "@frontend/shared/domain";
import { ButtonComponent } from "@frontend/shared/button";

@Component({
    selector: 'stroytorg-datepicker',
    templateUrl: './date-picker.component.html',
    standalone: true,
    imports: [CommonModule, MatIconModule, ButtonComponent]
})
export class DatePickerComponent implements OnInit {

    previosMonthIcon = Icon.DOUBLE_LEFT;
    nextMonthIcon = Icon.DOUBLE_RIGHT;
    monthDays!: DatePickerDate[];
    monthNumber!: number;
    year!: number;
    selectedDate!: DatePickerDate;

    weekDaysName = WEEK_DAYS_NANE;

    constructor(readonly datePickerService: DatePickerService) { }

    ngOnInit(): void {
        this.setMonthDays(this.datePickerService.getCurrentMonth());
    }

    getMonthName() {
        return this.datePickerService.getMonthName(this.monthNumber)
    }

    selectMonth(date: DatePickerDate) {
        this.selectedDate = date;
    }

    nextMonth() {
        this.monthNumber++;
        if (this.monthNumber > 11) {
            this.monthNumber = 0;
            this.year++;
        }
        return this.setMonthDays(this.datePickerService.getMonth(this.monthNumber, this.year));
    }

    previousMonth() {
        this.monthNumber--;
        if (this.monthNumber < 0) {
            this.monthNumber = 11;
            this.year--;
        }

        return this.setMonthDays(this.datePickerService.getMonth(this.monthNumber, this.year));
    }

    private setMonthDays(days: DatePickerDate[]) {
        this.monthDays = days;
        const currectMonthDate = this.monthDays.find(x => x.monthDayNumber === 0)!;
        this.monthNumber = currectMonthDate.monthIndex;
        this.year = currectMonthDate.year;
        this.selectedDate = this.monthDays.find(x => x.className === 'today')!;
    }
}