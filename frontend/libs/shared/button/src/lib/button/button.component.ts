/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonStyleDirective } from './components/directives/buttonStyle.directive';
import { ButtonStyle, Icons } from '@frontend/shared/domain';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule, ButtonStyleDirective, MatIconModule, RouterModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
  @Input()
  buttonStyle = ButtonStyle.DEFAULT_BUTTON;

  @Input()
  routerLink!: string;

  @Input()
  queryParams!: { [key: string]: any };

  @Input()
  label!: string;

  @Input()
  icon!: Icons;
}
