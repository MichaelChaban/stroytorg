/* eslint-disable @angular-eslint/component-selector */
import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostListener,
  Input,
  Optional,
  Self,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '../../../../input';
import { NgControl } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import {
  Icon,
  SelectStyle,
  TooltipDirective,
  TooltipProperties,
} from '@frontend/shared/domain';
import { SelectStyleDirective } from './directives';
import { BaseInputControls, BaseFormControlInputComponent } from '../../../../../forms';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    InputComponent,
    SelectStyleDirective,
    TooltipDirective,
  ],
  templateUrl: './select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: BaseInputControls,
      useExisting: SelectComponent,
    },
  ],
})
export class SelectComponent extends BaseFormControlInputComponent {
  
  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    if (!this.elRef.nativeElement.contains(event.target as Node)) {
      this.showOptions = false;
    }
  }

  @Input()
  selectStyle?: SelectStyle;

  @Input()
  isTooltipSet?: boolean;

  selectedOption: any;
  showOptions = false;

  constructor(
    @Optional() @Self() ngControl: NgControl,
    private elRef: ElementRef
  ) {
    super(ngControl);
  }

  get icon() {
    return this.value ? Icon.CLOSE : Icon.EXPAND_MORE;
  }

  getTooltip(label: string): TooltipProperties | undefined {
    if(!this.isTooltipSet){
      return undefined;
    }

    return {
      tooltipPosition: 'right',
      tooltipText: label,
    };
  }

  options = [
    { value: 2023, label: '2023' },
    { value: '2023', label: '2023' },
    { value: '2023', label: '2023' },
  ];

  toggleOptionsList(isRemove?: boolean): void {
    if (isRemove && this.value) {
      this.value = null;
    }

    this.showOptions = !this.showOptions;
  }

  selectOption(option: any) {
    this.value = option.value;
    this.showOptions = false;
  }
}
