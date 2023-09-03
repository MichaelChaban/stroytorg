/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { ButtonStyleDirective, TooltipDirective } from './components/directives/index';
import { ButtonStyle, Icons, TooltipModel } from '@frontend/shared/domain';

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
})
export class ButtonComponent {
  @Input()
  buttonStyle = ButtonStyle.DEFAULT_BUTTON;

  @Input()
  routerLink?: string;

  @Input()
  queryParams?: { [key: string]: any };

  @Input()
  label?: string;

  @Input()
  icon?: Icons;

  @Input()
  tooltip!: TooltipModel;
}
