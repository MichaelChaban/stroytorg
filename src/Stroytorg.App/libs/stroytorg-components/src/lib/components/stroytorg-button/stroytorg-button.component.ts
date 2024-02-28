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
  ButtonStyle,
  TooltipDefinition,
} from './stroytorg-buttons.models';
import { RouterModule } from '@angular/router';
import { Icon, UnsubscribeControlComponent } from '@stroytorg/shared';
import { ButtonTooltipDirective } from './directives/button-tooltip.directive';
import { MatIconModule } from '@angular/material/icon';
import { ButtonEnterPressedDirective } from './directives/button-enter-pressed.directive';
import { Subject, debounceTime, takeUntil } from 'rxjs';
// import { APP_CONFIG_ASSETS_BASE_URL } from "@stroytorg/shared";

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ButtonTooltipDirective,
    MatIconModule,
    ButtonEnterPressedDirective,
  ],
  templateUrl: './stroytorg-button.component.html',
  changeDetection: ChangeDetectionStrategy.Default,
  encapsulation: ViewEncapsulation.None,
})
export class StroytorgButtonComponent extends UnsubscribeControlComponent {
  private readonly clickSubject = new Subject();

  constructor() {
    super();
    this.clickSubject
      .pipe(takeUntil(this.destroyed$), debounceTime(100))
      .subscribe(() => {
        this.clickFn.emit();
      });
  }

  @Input()
  routerLink?: string;

  @Input()
  queryParams?: { [key: string]: unknown };

  @Input()
  size = ButtonSize.FIT_CONTENT as string;

  @Input()
  disabled!: boolean;

  @Input()
  rounded = false as boolean;

  @Input()
  style? = ButtonStyle.DEFAULT as string;

  @Input()
  title?: string;

  @Input()
  icon?: Icon;

  @Input()
  buttonType: 'submit' | 'button' = 'button';

  @Input()
  tooltip?: TooltipDefinition;

  @Input()
  buttonClass = '';

  @Output() clickFn = new EventEmitter<void>();

  @HostListener('click', ['$event'])
  @HostListener('keydown.enter', ['$event'])
  handleEvent(event: Event): void {
    this.clickSubject.next(event);
  }
}
