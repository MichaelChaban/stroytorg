/* eslint-disable @angular-eslint/directive-selector */
import {
  Directive,
  ElementRef,
  Input,
  Renderer2,
  HostListener,
  Inject,
  PLATFORM_ID,
  OnInit,
  OnDestroy,
  NgZone,
} from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { TooltipDefinition } from '../stroytorg-buttons.models';

@Directive({
  selector: 'button[tooltip]',
  standalone: true,
})
export class ButtonTooltipDirective implements OnInit, OnDestroy {
  @Input() tooltip?: TooltipDefinition;
  private tooltipElement!: HTMLElement;
  private tooltipTimeout = 0;
  private readonly TRANSITION_DURATION = 150;

  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
    private zone: NgZone,
    @Inject(PLATFORM_ID) private platformId: object,
  ) {}

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.zone.runOutsideAngular(() => {
        window.addEventListener('scroll', this.onScroll.bind(this));
      });
    }
  }

  ngOnDestroy() {
    if (isPlatformBrowser(this.platformId)) {
      window.removeEventListener('scroll', this.onScroll.bind(this));
    }
  }

  onScroll() {
    if (this.tooltipElement && this.tooltip) {
      this.setTooltipPosition();
    }
  }

  @HostListener('mouseenter')
  onMouseEnter() {
    if (!this.tooltip) {
      return null;
    }
    this.removeTooltipWithDelay();
    return this.createTooltip();
  }

  @HostListener('mouseleave')
  onMouseLeave() {
    if (!this.tooltip) {
      return null;
    }
    return this.removeTooltipWithDelay();
  }

  private createTooltip() {
    if (!this.tooltipElement) {
      this.tooltipElement = this.renderer.createElement('div');
      this.tooltipElement.className = 'stroytorg-tooltip';
      this.tooltipElement.textContent = this.tooltip!.title;
      this.renderer.appendChild(document.body, this.tooltipElement);
      this.setTooltipPosition();
      this.renderer.setStyle(this.tooltipElement, 'transform', 'scale(75%)');
      setTimeout(() => {
        if (this.tooltipElement) {
          this.tooltipElement.style.transition = `opacity ${this.TRANSITION_DURATION}ms, transform ${this.TRANSITION_DURATION}ms`;
          this.tooltipElement.style.opacity = '1';
          this.tooltipElement.style.transform = 'scale(1)';
        }
      });
    }
  }

  private removeTooltipWithDelay() {
    this.removeTooltip();
  }

  private removeTooltip() {
    if (this.tooltipElement) {
      this.tooltipElement.style.opacity = '0';
      this.tooltipElement.style.transform = 'scale(75%)';

      this.tooltipTimeout = setTimeout(() => {
        if (this.tooltipElement) {
          this.renderer.removeChild(document.body, this.tooltipElement);
          this.resetTimeout();
        }
      }, this.TRANSITION_DURATION);
    }
  }

  private resetTimeout(): void {
    (this.tooltipElement as unknown) = null;
    clearTimeout(this.tooltipTimeout);
  }

  private setTooltipPosition() {
    const hostPosition = this.el.nativeElement.getBoundingClientRect();
    const tooltipDimensions = this.tooltipElement.getBoundingClientRect();
    let tooltipPositionTop;
    let tooltipPositionLeft;

    switch (this.tooltip!.position) {
      case 'above':
        tooltipPositionTop = hostPosition.top - tooltipDimensions.height - 8;
        tooltipPositionLeft =
          hostPosition.left +
          (hostPosition.width - tooltipDimensions.width) / 2;
        break;
      case 'left':
        tooltipPositionTop =
          hostPosition.top +
          (hostPosition.height - tooltipDimensions.height) / 2;
        tooltipPositionLeft = hostPosition.left - tooltipDimensions.width - 8;
        if (tooltipPositionLeft < 0) {
          this.tooltip!.position = 'right';
          tooltipPositionLeft = hostPosition.left + hostPosition.width + 8;
        }
        break;
      case 'right':
        tooltipPositionTop =
          hostPosition.top +
          (hostPosition.height - tooltipDimensions.height) / 2;
        tooltipPositionLeft = hostPosition.left + hostPosition.width + 8;
        if (tooltipPositionLeft + tooltipDimensions.width > window.innerWidth) {
          this.tooltip!.position = 'left';
          tooltipPositionLeft = hostPosition.left - tooltipDimensions.width - 8;
        }
        break;
      case 'below':
        tooltipPositionTop = hostPosition.top + hostPosition.height + 8;
        tooltipPositionLeft =
          hostPosition.left +
          (hostPosition.width - tooltipDimensions.width) / 2;
        break;
    }

    tooltipPositionTop += window.scrollY;

    this.renderer.setStyle(
      this.tooltipElement,
      'top',
      `${tooltipPositionTop}px`,
    );
    this.renderer.setStyle(
      this.tooltipElement,
      'left',
      `${tooltipPositionLeft}px`,
    );
  }
}
