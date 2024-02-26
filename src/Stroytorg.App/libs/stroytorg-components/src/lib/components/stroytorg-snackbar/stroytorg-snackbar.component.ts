import { Component, Input, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { UnsubscribeControlComponent } from '@stroytorg/shared';
import { BehaviorSubject, skip, takeUntil } from 'rxjs';
import { SnackBarConfig } from './stroytorg-snackbar.models';
import { StroytorgSnackbarService } from './services/stroytorg-snackbar.service';


const TIMEOUT_OFFSET = 1000;

@Component({
  selector: 'stroytorg-snackbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-snackbar.component.html',
  styleUrl: './stroytorg-snackbar.component.scss',
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('snackbarAnimation', [
      state(
        'closed',
        style({
          transform: 'translateY(100%)',
          opacity: 0,
          height: '0',
        })
      ),
      state(
        'open',
        style({
          transform: 'translateY(0)',
          opacity: 1,
          height: '*',
        })
      ),
      transition('closed => open', [
        style({
          transform: 'translateY(100%)',
          opacity: 0,
          height: '*',
        }),
        animate(
          '300ms ease-out',
          style({
            transform: 'translateY(0)',
            opacity: 1,
            height: '*',
          })
        ),
      ]),
      transition('open => closed', [
        style({
          transform: 'translateY(0)',
          opacity: 1,
          height: '*',
        }),
        animate(
          '300ms ease-in',
          style({
            transform: 'translateY(100%)',
            opacity: 0,
            height: '*',
          })
        ),
      ]),
    ]),
  ],
})
export class StroytorgSnackbarComponent extends UnsubscribeControlComponent {
  message!: string;
  title!: string;
  success = true;
  duration!: number;

  protected snackAnimationState = 'closed';
  private locked = new BehaviorSubject<boolean>(false);

  constructor(private snackBarService: StroytorgSnackbarService) {
    super();
    const pendingConfigs: SnackBarConfig[] = [];
    this.snackBarService.snackbarState$
      ?.pipe(skip(1), takeUntil(this.destroyed$))
      .subscribe((config) => {
        if (!this.locked.value) {
          this.triggerSnackBar(config);
        } else {
          pendingConfigs.push({ ...config });
        }
      });

    this.locked.pipe(takeUntil(this.destroyed$)).subscribe((isLocked) => {
      if (!isLocked && pendingConfigs.length > 0) {
        const configToTrigger = pendingConfigs.shift();
        if (configToTrigger) {
          setTimeout(() => {
            this.triggerSnackBar(configToTrigger);
          }, TIMEOUT_OFFSET);
        }
      }
    });
  }

  protected getTitle() {
      return this.success ? 'Success' : 'Error';
  }

  private triggerSnackBar(config: SnackBarConfig) {
    this.message = config.message;
    this.success = config.success;
    this.duration = config.duration ?? 3000;
    this.snackAnimationState = 'open';
    this.locked.next(true);
    setTimeout(() => {
      this.snackAnimationState = 'closed';
      this.locked.next(false);
    }, this.duration);
  }
}

