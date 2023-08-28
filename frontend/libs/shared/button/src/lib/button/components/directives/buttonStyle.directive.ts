/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';
import { ButtonStyle } from '@frontend/shared/domain';
import { setDefaultButton, setPrimaryButton, setWarningButton } from './utils/buttonStyle.utils';

@Directive({
  selector: '[buttonStyle]',
  standalone: true,
})
export class ButtonStyleDirective {

  @Input()
  set buttonStyle(value: ButtonStyle){
    switch(value){
      case 'DEFAULT_BUTTON': setDefaultButton(this.el, this.renderer); break;
      case 'PRIMARY_BUTTON': setPrimaryButton(this.el, this.renderer); break;
      case 'WARNING_BUTTON': setWarningButton(this.el, this.renderer); break;
      default: break;
    }
  }

  constructor(private el: ElementRef, private renderer: Renderer2) {}
}
