import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  HostListener,
  Input,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ButtonSize,
  ButtonType,
  TooltipDefinition,
} from './stroytorg-buttons.models';
import { RouterModule } from '@angular/router';
import { Icon } from '@stroytorg/shared';
import { ButtonTooltipDirective } from './directives/button-tooltip.directive';
import { MatIconModule } from '@angular/material/icon';
// import { APP_CONFIG_ASSETS_BASE_URL } from "@stroytorg/shared";

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule, RouterModule, ButtonTooltipDirective, MatIconModule],
  templateUrl: './stroytorg-button.component.html',
  styleUrls: ['./stroytorg-button.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default,
  encapsulation: ViewEncapsulation.None,
})
export class StroytorgButtonComponent {
  @Input()
  routerLink?: string;

  @Input()
  queryParams?: { [key: string]: any };

  @Input()
  size = ButtonSize.normal as string;

  @Input()
  disabled!: boolean;

  @Input()
  type? = ButtonType.default as string;

  @Input()
  title?: string;

  @Input()
  icon?: Icon;

  @Input()
  buttonType: 'submit' | 'button' = 'button';

  @Input()
  tooltip?: TooltipDefinition;

  @Output() clicked = new EventEmitter<void>();

  @HostListener('click')
  clickedListen() {
    this.clicked.emit();
  }
}
