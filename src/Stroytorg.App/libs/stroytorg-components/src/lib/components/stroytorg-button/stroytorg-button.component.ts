import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  HostListener,
  Input,
  OnInit,
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
import { Icon } from '@stroytorg/shared';
import { UnsubscribeControlComponent } from 'libs/shared/src/utils/unsubcribe-control.component';
import { ButtonTooltipDirective } from './directives/button-tooltip.directive';
import { MatIconModule } from '@angular/material/icon';
import { ButtonEnterPressedDirective } from './directives/button-enter-pressed.directive';
import { BehaviorSubject, Subject, debounceTime, takeUntil } from 'rxjs';
import { StroytorgLoaderComponent } from '../stroytorg-loader';
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
    StroytorgLoaderComponent,
  ],
  templateUrl: './stroytorg-button.component.html',
  changeDetection: ChangeDetectionStrategy.Default,
  encapsulation: ViewEncapsulation.None,
})
export class StroytorgButtonComponent
  extends UnsubscribeControlComponent
  implements OnInit
{
  private clickSubject = new Subject<any>();

  ngOnInit(): void {
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

  @Input()
  isLoadingVisible = false;

  @Output() clickFn = new EventEmitter<void>();

  loading$ = new BehaviorSubject<boolean>(false);

  @HostListener('click', ['$event'])
  @HostListener('keydown.enter', ['$event'])
  handleEvent(event: Event): void {
    if (this.disabled || (this.isLoadingVisible && this.loading$.value)) {
      return;
    }
    if (this.isLoadingVisible && !this.loading$.value) {
      this.disabled = true;
      this.loading$.next(true);
    }
    this.clickSubject.next(event);
  }
}
