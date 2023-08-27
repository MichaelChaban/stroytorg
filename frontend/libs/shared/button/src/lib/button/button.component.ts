/* eslint-disable @angular-eslint/component-selector */
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonStyleDirective } from './components/directives/buttonStyle.directive';
import { ButtonStyle, ButtonType } from '@frontend/shared/domain';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule, ButtonStyleDirective, MatIconModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
  @Input()
  buttonStyle = ButtonStyle.DEFAULT_BUTTON;

  @Input()
  buttonType: ButtonType = ButtonType.DEFAULT_BUTTON;

  @Input()
  queryParams: { [key: string]: any } = {};

  @Input()
  params: { [key: string]: any } = {};

  @Input()
  label!: string;

  @Input()
  buttonFn = new EventEmitter<any>();

  callButtonFn(){
    this.buttonFn.emit();
  }
}
