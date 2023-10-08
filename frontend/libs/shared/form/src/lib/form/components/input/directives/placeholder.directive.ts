/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[placeholder]',
  standalone: true,
})
export class PlaceholderDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @HostListener('input') onInput() {
    this.toggleLabel();
  }

  private toggleLabel() {
    const input = this.el.nativeElement;
    const label = this.el.nativeElement.parentElement.querySelector('label');

    if (input.value) {
      this.renderer.addClass(label, 'active');
    } else {
      this.renderer.removeClass(label, 'active');
    }
  }
}
