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

  @Output() timeChanges = new EventEmitter<number>();

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onHourChange(timeId: number) {
    this.timeChanges.emit(timeId);
  }
}
