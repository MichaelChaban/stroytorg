/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component, Optional, Self } from "@angular/core";
import { InputComponent } from "../input";
import { NgControl } from "@angular/forms";
import { BaseInputControls } from "../../forms";
import { DatePickerComponent } from "./components";

@Component({
    selector: 'stroytorg-date',
    templateUrl: './date.component.html',
    standalone: true,
    imports: [CommonModule, InputComponent, DatePickerComponent],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: BaseInputControls,
            useExisting: DateComponent,
        },
    ],
})
export class DateComponent extends InputComponent {
    constructor(@Optional() @Self() ngControl: NgControl) {
        super(ngControl);
    }
}