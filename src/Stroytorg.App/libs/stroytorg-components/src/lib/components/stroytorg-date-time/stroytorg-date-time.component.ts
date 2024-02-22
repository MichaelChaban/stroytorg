import {
  Component,
  Input,
  ViewEncapsulation,
  ChangeDetectionStrategy,
  Output,
  EventEmitter,
  Optional,
  Self,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeSize } from './stroytorg-date-time.models';
import { NgControl, ReactiveFormsModule } from '@angular/forms';
import { ErrorPipe } from '@stroytorg/shared';
import {
  StroytorgBaseInputControls,
  StroytorgBaseFormInputComponent,
} from '../stroytorg-base-form';

@Component({
  selector: 'stroytorg-date-time',
  standalone: true,
  imports: [CommonModule, ErrorPipe, ReactiveFormsModule],
  templateUrl: './stroytorg-date-time.component.html',
  styleUrls: ['./stroytorg-date-time.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgDateTimeComponent,
    },
  ],
})
export class StroytorgDateTimeComponent extends StroytorgBaseFormInputComponent {
  @Input()
  size = TimeSize.large;

  @Output()
  valueChange = new EventEmitter<string>();

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  changeValue(eventArg: any): void {
    let value = eventArg.target.value;
    if (value instanceof Date) {
      const hours = value.getHours();
      const minutes = value.getMinutes();
      value = `${hours}:${minutes}`;
    }

    this.formControl.setValue(value);
  }
}
