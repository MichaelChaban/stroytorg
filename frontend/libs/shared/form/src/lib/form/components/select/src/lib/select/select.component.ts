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
import { FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import {
  Icon,
  SelectStyle,
  TooltipDirective,
  TooltipProperties,
  ValueToKeyPipe,
} from '@frontend/shared/domain';
import { SelectStyleDirective } from './directives';
import { BaseFormControlInputComponent, BaseInputControls } from '../../../../../forms';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    InputComponent,
    SelectStyleDirective,
    TooltipDirective,
    ReactiveFormsModule,
    FormsModule,
    ValueToKeyPipe,
  ],
  templateUrl: './select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
providers: [
    {
      provide: BaseInputControls,
      useExisting: SelectComponent,
      multi: true,
    },
  ],
})
export class SelectComponent extends BaseFormControlInputComponent {

  constructor(
    @Optional() @Self() ngControl: NgControl,
    private elRef: ElementRef
  ) {
    super(ngControl);
  }
  
  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    if (!this.elRef.nativeElement.contains(event.target as Node)) {
      this.showOptions = false;
    }
  }

  selectOptions = [
    { value: 'option1', label: 'Option 1' },
    { value: 'option2', label: 'Option 2' },
    { value: 'option3', label: 'Option 3' }
  ];

  selectedValue: any;

  @Input()
  selectStyle?: SelectStyle;

  @Input()
  isTooltipSet?: boolean;

  @Input()
  options: { label: any, value: any }[] = [];

  showOptions = false;
  
  get icon() {
    return this.formControl.value ? Icon.CLOSE : Icon.EXPAND_MORE;
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

  toggleOptionsList(isRemove?: boolean): void {
    if (isRemove && this.formControl.value) {
      this.formControl.setValue(null);
    }

    this.showOptions = !this.showOptions;
  }

  override writeValue(option: any) {
    this.formControl.setValue(option.value);
    this.showOptions = false;
  }
}
