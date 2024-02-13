import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { AVAILABLE_HOURS } from './stroytorg-timepicker.models';
import { ButtonSize, StroytorgButtonComponent } from '../../../stroytorg-button';

@Component({
  selector: 'stroytorg-timepicker',
  templateUrl: './stroytorg-timepicker.component.html',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent],
})
export class StroytorgTimePickerComponent {

  readonly availableHours = AVAILABLE_HOURS;
  readonly buttonSize = ButtonSize;

  @Output() timeSelected = new EventEmitter<string>();

  @Output() hourSelected = new EventEmitter<number>();
  @Output() minuteSelected = new EventEmitter<number>();

  onHourChange(event: any) {
    this.hourSelected.emit(event.target.value);
  }
}
