/* eslint-disable @angular-eslint/component-selector */
import { Component, OnInit } from "@angular/core";
import { DatePickerService } from "./services";
import { DatePicker, WEEK_DAYS_NANE } from "./models";
import { CommonModule } from "@angular/common";

@Component({
    selector: 'stroytorg-datepicker',
    templateUrl: './datePicker.component.html',
    standalone: true,
    imports: [CommonModule]
})
export class DatePickerComponent implements OnInit {
    public monthDays!: DatePicker[];

    public monthNumber!: number;
    public year!: number;

    public weekDaysName = WEEK_DAYS_NANE;

    constructor(public datePickerService: DatePickerService) { }

    ngOnInit(): void {
        this.setMonthDays(this.datePickerService.getCurrentMonth());
    }

    getMonthName(){
        return this.datePickerService.getMonthName(this.monthNumber)
    }

    nextMonth() {
        this.monthNumber++;
        if (this.monthNumber == 13) {
            this.monthNumber = 1;
            this.year++;
        }
        return this.setMonthDays(this.datePickerService.getMonth(this.monthNumber, this.year));
    }

    previousMonth() {
        this.monthNumber--;

        if (this.monthNumber < 1) {
            this.monthNumber = 12;
            this.year--;
        }

        return this.setMonthDays(this.datePickerService.getMonth(this.monthNumber, this.year));
    }

    private setMonthDays(days: DatePicker[]) {
        this.monthDays = days;
        this.monthNumber = this.monthDays[0].monthIndex;
        this.year = this.monthDays[0].year;
    }
}