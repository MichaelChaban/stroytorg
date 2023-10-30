/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, ElementRef, HostListener, Optional, Self } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '../../../../input';
import { NgControl } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { Icon } from '@frontend/shared/domain';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [CommonModule, MatIconModule, InputComponent],
  templateUrl: './select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SelectComponent extends InputComponent {

  selectedOption: any;
  showOptions = false;

  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    if (!this.elRef.nativeElement.contains(event.target as Node)) {
      this.showOptions = false;
    }
  }

  constructor(@Optional() @Self() ngControl: NgControl,
    private elRef: ElementRef) {
    super(ngControl);
  }

  get icon() {
    return this.value ? Icon.CLOSE : Icon.EXPAND_MORE;
  } 

  options = [
    { value: 'option1', label: 'Option 1' },
    { value: 'option2', label: 'Option 2' },
    { value: 'option3', label: 'Option 3' }
  ];

  toggleOptionsList(isRemove?: boolean): void {
    if (isRemove && this.value){
      this.value = null;
    }
    
    this.showOptions = !this.showOptions;
  }

  selectOption(option: any) {
    this.value = option.value;
    this.showOptions = false;
  }
}
