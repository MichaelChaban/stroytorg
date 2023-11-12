/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';
import { SelectStyle } from '@frontend/shared/domain';

@Directive({
  selector: '[selectStyle]',
  standalone: true,
})
export class SelectStyleDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @Input()
  set selectStyle(value: SelectStyle | undefined){
    switch(value){
      case 'DEFAULT_SELECT': this.setDefaultSelect(); break;
      case 'SIMPLE_SELECT': this.setSimpleSelect(); break;
      default: this.setSimpleSelect(); break;
    }
  }

  private setDefaultSelect(){
    return this.renderer.addClass(this.el.nativeElement, 'default-select')
  }

  private setSimpleSelect(){
    return this.renderer.addClass(this.el.nativeElement, 'simple-select')
  }
}