/* eslint-disable @angular-eslint/component-selector */
import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { Icons } from "@frontend/shared/domain";
import { ValidationErrors } from "@angular/forms";
import { ErrorPipe } from "../../pipes";

@Component({
    selector: 'error-tooltip',
    templateUrl: './error-tooltip.component.html',
    standalone: true,
    imports: [CommonModule, MatIconModule, ErrorPipe]
})
export class ErrorTooltipComponent{

    @Input()
    errors?: ValidationErrors;

    errorIcon = Icons.ERROR;
}