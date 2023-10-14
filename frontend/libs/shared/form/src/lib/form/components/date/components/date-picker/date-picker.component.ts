/* eslint-disable @angular-eslint/component-selector */
import { Component, OnInit } from "@angular/core";
import { DatePickerDate } from "./models";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";
import { ButtonComponent } from "@frontend/shared/button";
import { DatePickerViewFacade } from "./date-picker.view-facade";

@Component({
    selector: 'stroytorg-datepicker',
    templateUrl: './date-picker.component.html',
    standalone: true,
    imports: [CommonModule, MatIconModule, ButtonComponent],
    providers: [DatePickerViewFacade]
})
export class DatePickerComponent implements OnInit {

    constructor(readonly viewFacade: DatePickerViewFacade) { }

    ngOnInit(): void {
        this.viewFacade.init();
    }

    selectMonth(date: DatePickerDate): void {
        this.viewFacade.selectDate(date);
    }

    getMonthName() {
        this.viewFacade.getMonthName()
    }

    nextMonth() {
        this.viewFacade.nextMonth();
    }

    previousMonth() {
        this.viewFacade.previousMonth();
    }
}