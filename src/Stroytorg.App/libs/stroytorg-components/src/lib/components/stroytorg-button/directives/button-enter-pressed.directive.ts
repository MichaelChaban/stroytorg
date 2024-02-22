/* eslint-disable @angular-eslint/directive-selector */
import { Directive, HostListener, Renderer2, inject } from '@angular/core';

@Directive({
  selector: '[enterPress]',
  standalone: true,
})
export class ButtonEnterPressedDirective {
  private enterPressedClass = 'enter-pressed';

  private renderer = inject(Renderer2);

  @HostListener('keydown.enter')
  onEnterKeyDown() {
    this.addEnterPressedClass();
  }

  @HostListener('keyup.enter')
  onEnterKeyUp() {
    this.removeEnterPressedClass();
  }

  private addEnterPressedClass() {
    this.renderer.addClass(document.activeElement, this.enterPressedClass);
  }

  private removeEnterPressedClass() {
    this.renderer.removeClass(document.activeElement, this.enterPressedClass);
  }
}
