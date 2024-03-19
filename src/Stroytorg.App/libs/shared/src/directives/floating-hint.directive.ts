/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[floatingHint]',
  standalone: true,
})
export class FloatingHintDirective {

  private inputValue = null;

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @HostListener('input')
  onInput() {
    if(!this.el){
      return;
    }
    this.toggleLabel();
  }

  private toggleLabel() {
    this.inputValue = this.el.nativeElement.value;
    const label = this.el.nativeElement.parentElement.querySelector('label');
    if (!label) {
      return;
    }
    if (this.inputValue) {
      return this.renderer.addClass(label, 'active');
    }
    
    return this.renderer.removeClass(label, 'active');
  }
}