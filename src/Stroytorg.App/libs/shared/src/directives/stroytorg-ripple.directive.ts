/* eslint-disable @angular-eslint/directive-selector */
import {
  Directive,
  ElementRef,
  HostListener,
  Renderer2,
  inject,
} from '@angular/core';

@Directive({
  selector: '[ripple]',
  standalone: true,
})
export class StroytorgRippleDirective {
  private el = inject(ElementRef);
  private renderer = inject(Renderer2);
  private ripple?: HTMLElement;

  @HostListener('click', ['$event'])
  onMousedown(event: MouseEvent) {
    if (this.ripple) {
      this.renderer.removeChild(this.el.nativeElement, this.ripple);
      this.ripple = undefined;
    }
    this.addRipple(event);
  }

  private addRipple(event: MouseEvent) {
    const nativeEl = this.el.nativeElement;
    const circle = this.renderer.createElement('div');
    this.ripple = circle;
    this.renderer.appendChild(nativeEl, circle);

    const diameter = Math.min(nativeEl.clientHeight, nativeEl.clientWidth);
    this.renderer.setStyle(circle, 'width', diameter + 'px');
    this.renderer.setStyle(circle, 'height', diameter + 'px');
    const rect = nativeEl.getBoundingClientRect();
    const x = event.clientX - rect.left - diameter;
    const y = event.clientY - rect.top - diameter / 2;
    this.renderer.setStyle(circle, 'left', x + 'px');
    this.renderer.setStyle(circle, 'top', y + 'px');

    circle.classList.add('stroytorg-ripple');

    setTimeout(() => {
      if (this.ripple) {
        this.renderer.removeChild(nativeEl, this.ripple);
        this.ripple = undefined;
      }
    }, 600);
  }
}
