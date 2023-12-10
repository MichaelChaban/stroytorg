/* eslint-disable @angular-eslint/component-selector */
import { CUSTOM_ELEMENTS_SCHEMA, ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { ButtonStyleDirective } from './directives/index';
import { ButtonStyle, Icon, TooltipDirective, TooltipProperties } from '@frontend/shared/domain';

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [
    CommonModule,
    ButtonStyleDirective,
    TooltipDirective,
    RouterModule,
    MatIconModule,
  ],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ButtonComponent {
  @Input()
  buttonStyle?: ButtonStyle;

  @Input()
  routerLink?: string;

  @Input()
  queryParams?: { [key: string]: any };

  @Input()
  label?: string;

  @Input()
  icon?: Icon;

  @Input()
  tooltip?: TooltipProperties;

  @Input()
  width?: number;

  @Input()
  disabled = false;

  getButtonStyles(){
    if(!this.width){
      return null;
    }
    return { '--button-width': `${this.width}%` }
  }
}
