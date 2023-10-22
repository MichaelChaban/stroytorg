/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { ButtonComponent } from "@frontend/shared/button";
import { MONTHS_NAMES } from "../../models";

@Component({
    selector: 'stroytorg-month-picker',
    templateUrl: './month-picker.component.html',
    standalone: true,
    imports: [CommonModule, ButtonComponent],
})
export class MonthPickerComponent{

    monthNames = MONTHS_NAMES;

    @Input() isVisible = false;

    @Output() monthChange = new EventEmitter<string>();

    selectMonth(month: string) : void{
        this.monthChange.emit(month);
    }
}