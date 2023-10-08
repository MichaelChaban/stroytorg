/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input, Optional, Self } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@frontend/shared/button';
import { ButtonPropertiesModel, Icons, InputType } from '@frontend/shared/domain';
import { MatIconModule } from '@angular/material/icon';
import { NgControl, ReactiveFormsModule } from '@angular/forms';
import { BaseInputControls, BaseFormControlInputComponent } from '../../forms';
import { ErrorPipe } from '../../pipes';
import { PlaceholderDirective } from './directives/placeholder.directive';

const ERROR_ICON = Icons.ERROR;

@Component({
  selector: 'stroytorg-input',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatIconModule, ReactiveFormsModule, ErrorPipe, PlaceholderDirective],
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: BaseInputControls,
      useExisting: InputComponent,
    },
  ],
})
export class InputComponent extends BaseFormControlInputComponent  {

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  @Input()
  inputType: InputType = InputType.TEXT;

  @Input()
  button?: ButtonPropertiesModel;

  isSet(element: any) {
    if (element) {
      return element;
    }
  }

  errorIcon = ERROR_ICON;
}
