import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  StroytorgButtonComponent,
  TooltipDefinition,
  StroytorgTextInputComponent,
  StroytorgSelectComponent,
  StroytorgCheckboxComponent,
  StroytorgDateComponent,
  StroytorgTimeComponent,
  StroytorgTableComponent,
  StroytorgLoaderComponent,
  StroytorgSnackbarService,
  StroytorgCardComponent,
  StroytorgRangeComponent,
  StroytorgRadioComponent,
  StroytorgRadioOption,
  StroytorgChipComponent,
} from '@stroytorg/stroytorg-components';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  cardMockData,
  getCardFilterDefinitions,
  getCardRowDefinition,
  getColumnDefinitions,
  mockData,
} from './stroytorg-home.column-definitions';
import { environment } from 'apps/stroytorg.app/src/environments/environment';

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
    StroytorgLoaderComponent,
    StroytorgCardComponent,
    StroytorgRangeComponent,
    StroytorgRadioComponent,
    StroytorgChipComponent,
  ],
  templateUrl: './stroytorg-home.component.html',
  styleUrl: './stroytorg-home.component.scss',
})
export class StroytorgHomeComponent implements OnInit {
  environmentImageResource = environment.baseImagePath;

  mockDATA = mockData;
  snackbar = inject(StroytorgSnackbarService);

  cardMockData = cardMockData;

  columnDefinitions = getColumnDefinitions();

  cardRowDefinition = getCardRowDefinition();

  cardFilter = getCardFilterDefinitions();

  tooltip: TooltipDefinition = {
    position: 'above',
    title: 'Stroytorg',
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

  ngOnInit(): void {
    this.formGroup.valueChanges.subscribe((value) => {
      console.log(value);
    });
  }

  loading = false;

  someFunction() {
    // this.snackbar.showError('Majk je pan');
  }

  anotherFunction() {
    // this.snackbar.showSuccess('Majk ma kokot jak slon');
  }

  scrollDown(number: number) {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
      this.cardMockData = [...this.cardMockData, ...cardMockData];
    }, 2000);
  }

  radioOptions: StroytorgRadioOption[] = [
    {
      label: '1st option',
      value: 1,
    },
    {
      label: '2nd option',
      value: 2,
    },
    {
      label: '3rd option',
      value: 3,
      disabled: true,
    },
  ];

  filters = [
    { type: 'boolean', label: 'In Stock', value: false },
    {
      type: 'select',
      label: 'Category',
      value: '',
      options: [
        { label: 'Electronics', value: 'electronics' },
        { label: 'Clothing', value: 'clothing' },
        // Add more options as needed
      ],
    },
    { type: 'range', label: 'Price', value: 0, min: 0, max: 1000 },
    // Add more filters as needed
  ];

  formGroup = new FormGroup({
    select: new FormControl(null, [Validators.required]),
    textInput: new FormControl(null, [Validators.required]),
    checkbox: new FormControl(false, [Validators.required]),
    radio: new FormControl(2, [Validators.required]),
    date: new FormControl(new Date(2024, 2, 11), [Validators.required]),
    time: new FormControl(null, [Validators.required]),
    range: new FormControl(null, [Validators.required]),
  });
}
