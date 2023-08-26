/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[defaultButton]',
  standalone: true,
})
export class DefaultButtonDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.renderer.setStyle(this.el.nativeElement, 'background-color', 'blue');
    this.renderer.setStyle(this.el.nativeElement, 'color', 'white');
  }
}
