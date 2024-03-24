import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  HostListener,
  Input,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ButtonSize,
  ButtonPalette,
  TooltipDefinition,
  ButtonStyle,
} from './stroytorg-buttons.models';
import { RouterModule } from '@angular/router';
import { Icon, StroytorgRippleDirective } from '@stroytorg/shared';
import { UnsubscribeControlComponent } from 'libs/shared/src/utils/unsubcribe-control.component';
import {
  ButtonTooltipDirective,
  ButtonEnterPressedDirective
} from './directives';
import { MatIconModule } from '@angular/material/icon';
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
    ButtonEnterPressedDirective,
    StroytorgRippleDirective,
    MatIconModule,
    StroytorgLoaderComponent,
  ],
  templateUrl: './stroytorg-button.component.html',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class StroytorgButtonComponent
  extends UnsubscribeControlComponent
  implements AfterViewInit
{
  private clickSubject = new Subject<any>();

  ngAfterViewInit(): void {
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
  size: ButtonSize = 'fit-content-width';

  @Input()
  disabled = false;

  @Input()
  rounded? = false;

  @Input()
  palette?: ButtonPalette = 'default-button';

  @Input()
  style?: ButtonStyle = 'basic-button';

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
