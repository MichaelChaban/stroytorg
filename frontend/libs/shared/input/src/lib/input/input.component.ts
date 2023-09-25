/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

import { Icons, InputType } from '@frontend/shared/domain';

@Component({
  selector: 'stroytorg-editor',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatInputModule, MatFormFieldModule],
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
}
