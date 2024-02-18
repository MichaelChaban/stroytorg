import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ButtonStyle,
  StroytorgButtonComponent,
  TooltipDefinition,
  StroytorgTextInputComponent,
  StroytorgSelectComponent,
  StroytorgCheckboxComponent,
  StroytorgDateComponent,
  StroytorgTimeComponent,
  InputSize,
  StroytorgTableComponent,
  StroytorgLoaderComponent,
} from '@stroytorg/stroytorg-components';
import { Icon } from '@stroytorg/shared';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { getColumnDefinitions, mockData } from './stroytorg-home.column-definitions';

@Component({
  selector: 'stroytorg-stroytorg-home',
  standalone: true,
  imports: [
    CommonModule,
    StroytorgButtonComponent,
    StroytorgTextInputComponent,
    ReactiveFormsModule,
    StroytorgSelectComponent,
    StroytorgCheckboxComponent,
    StroytorgDateComponent,
    StroytorgTimeComponent,
    StroytorgTableComponent,
    StroytorgLoaderComponent
  ],
  templateUrl: './stroytorg-home.component.html',
  styleUrl: './stroytorg-home.component.scss'
})
export class StroytorgHomeComponent {
  buttonType = ButtonStyle;
  icon = Icon.HOME;
  inputSize = InputSize.XLARGE;

  mockDATA = mockData;
  
  columnDefinitions = getColumnDefinitions();

  tooltip: TooltipDefinition = {
    tooltipPosition: 'above',
    tooltipText: 'Stroytorg',
  };

  items = [
    {
      value: 1,
      label: 'item 1',
    },
    {
      value: 2,
      label: 'item 2',
    },
    {
      value: 3,
      label: 'item 3',
    },
    {
      value: 4,
      label: 'item 4',
    },
  ];

  someFunction() {
    console.log('some function');
  }

  formGroup = new FormGroup({
    select: new FormControl(null, [Validators.required]),
    textInput: new FormControl(null, [Validators.required]),
    checkbox: new FormControl(null, [Validators.required]),
    date: new FormControl(null, [Validators.required]),
    time: new FormControl(null, [Validators.required]),
  });
}
