/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultButtonDirective } from './components/directives/defaultButton.directive';
import { RouterButtonDirective } from './components/directives/routerButton.directive';

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  hostDirectives: [
    DefaultButtonDirective, RouterButtonDirective
  ]
})
export class ButtonComponent {

  @Input()
  queryParams: { [key: string]: any} = {};

  @Input()
  params: { [key: string]: any} = {};
}
