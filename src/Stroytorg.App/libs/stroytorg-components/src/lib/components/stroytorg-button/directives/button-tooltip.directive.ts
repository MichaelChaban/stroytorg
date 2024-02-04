/* eslint-disable @angular-eslint/directive-selector */
import {
    Directive,
    ElementRef,
    Input,
    Renderer2,
    HostListener,
  } from '@angular/core';
import { TooltipDefinition } from '../stroytorg-buttons.models';
  
  @Directive({
    selector: 'button[tooltip]',
    standalone: true,
  })
  export class ButtonTooltipDirective {
    @Input() tooltip?: TooltipDefinition;
  
    private tooltipElement!: HTMLElement;
    private tooltipTimeout: any;
  
    private readonly TRANSITION_DURATION = 150;
  
    constructor(private el: ElementRef, private renderer: Renderer2) {}
  
    @HostListener('mouseenter')
    onMouseEnter() {
      if (!this.tooltip) {
        return null;
      }
  
      this.resetTimeout();
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
        this.tooltipElement.className = 'tooltip';
        this.tooltipElement.textContent = this.tooltip!.tooltipText;
  
        this.renderer.appendChild(document.body, this.tooltipElement);
  
        this.setTooltipPosition();
  
        this.renderer.setStyle(this.tooltipElement, 'transform', 'scale(75%)');
  
        setTimeout(() => {
          this.tooltipElement.style.transition = `opacity ${this.TRANSITION_DURATION}ms, transform ${this.TRANSITION_DURATION}ms`;
          this.tooltipElement.style.opacity = '1';
          this.tooltipElement.style.transform = 'scale(1)';
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
          this.renderer.removeChild(document.body, this.tooltipElement);
          (this.tooltipElement as unknown) = null;
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
  
      switch (this.tooltip!.tooltipPosition) {
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
            this.tooltip!.tooltipPosition = 'right';
            tooltipPositionLeft = hostPosition.left + hostPosition.width + 8;
          }
          break;
        case 'right':
          tooltipPositionTop =
            hostPosition.top +
            (hostPosition.height - tooltipDimensions.height) / 2;
          tooltipPositionLeft = hostPosition.left + hostPosition.width + 8;
  
          if (tooltipPositionLeft + tooltipDimensions.width > window.innerWidth) {
            this.tooltip!.tooltipPosition = 'left';
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
  
      if (tooltipPositionTop < 0) {
        tooltipPositionTop = 8;
      }
      if (tooltipPositionLeft < 0) {
        tooltipPositionLeft = 8;
      }
      if (tooltipPositionLeft + tooltipDimensions.width > window.innerWidth) {
        tooltipPositionLeft = window.innerWidth - tooltipDimensions.width - 8;
      }
  
      this.renderer.setStyle(
        this.tooltipElement,
        'top',
        `${tooltipPositionTop}px`
      );
      this.renderer.setStyle(
        this.tooltipElement,
        'left',
        `${tooltipPositionLeft}px`
      );
    }
  }