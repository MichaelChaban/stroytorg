/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';
import { ButtonStyle } from '@frontend/shared/domain';

@Directive({
  selector: '[buttonStyle]',
  standalone: true,
})
export class ButtonStyleDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @Input()
  set buttonStyle(value: ButtonStyle | undefined){
    switch(value){
      case 'DEFAULT_BUTTON': this.setDefaultButton(); break;
      case 'PRIMARY_BUTTON': this.setPrimaryButton(); break;
      case 'WARNING_BUTTON': this.setWarningButton(); break;
      case 'SIMPLE_BUTTON': this.setSimpleButton(); break;
      default: this.setSimpleButton(); break;
    }
  }

  private setDefaultButton(){
    return this.renderer.addClass(this.el.nativeElement, 'default-button')
  }
  
  private setPrimaryButton(){
    return this.renderer.addClass(this.el.nativeElement, 'primary-button')
  }
  
  private setWarningButton(){
    return this.renderer.addClass(this.el.nativeElement, 'warning-button')
  }

  private setSimpleButton(){
    return this.renderer.addClass(this.el.nativeElement, 'simple-button')
  }
}