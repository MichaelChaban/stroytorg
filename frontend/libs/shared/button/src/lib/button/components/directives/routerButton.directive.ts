/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[routerButton]',
  standalone: true,
})
export class RouterButtonDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.renderer.setStyle(this.el.nativeElement, 'background-color', 'red');
    this.renderer.setStyle(this.el.nativeElement, 'color', 'white');
  }
}
