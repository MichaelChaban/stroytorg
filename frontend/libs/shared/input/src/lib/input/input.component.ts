/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ButtonComponent } from '@frontend/shared/button';
import { ButtonStyle, Icons, InputType, TooltipPropertiesModel } from '@frontend/shared/domain';

@Component({
  selector: 'stroytorg-input',
  standalone: true,
  imports: [CommonModule, MatInputModule, MatFormFieldModule, ButtonComponent],
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InputComponent {

  @Input()
  inputType: InputType = InputType.TEXT;

  @Input()
  placeholder?: string;

  @Input()
  readonly?: boolean = false;

  @Input()
  text = '';

  @Input()
  icon?: Icons;

  @Input()
  buttonTooltip?: TooltipPropertiesModel = { tooltipPosition: 'above', tooltipText: 'Пошук' };

  buttonStyle: ButtonStyle = ButtonStyle.PRIMARY_BUTTON;
}
