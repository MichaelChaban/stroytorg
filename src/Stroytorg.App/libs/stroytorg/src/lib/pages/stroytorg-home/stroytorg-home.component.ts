import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonType, StroytorgButtonComponent, TooltipDefinition, StroytorgTextInputComponent, StroytorgSelectComponent } from '@stroytorg/stroytorg-components';
import { Icon } from '@stroytorg/shared';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'stroytorg-stroytorg-home',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent, StroytorgTextInputComponent, ReactiveFormsModule, StroytorgSelectComponent],
  templateUrl: './stroytorg-home.component.html',
  styleUrl: './stroytorg-home.component.scss',
})
export class StroytorgHomeComponent {

  buttonType = ButtonType;
  icon = Icon.HOME;

  tooltip: TooltipDefinition = {
    tooltipPosition: 'above',
    tooltipText: 'Stroytorg'
  };

  items = [
    {
      id: 1,
      value: 'item 1'
    },
    {
      id: 2,
      value: 'item 2'
    },
    {
      id: 3,
      value: 'item 3'
    },
    {
      id: 4,
      value: 'item 4'
    },
  ]

  someFunction() {
    console.log('some function');
  }

  formGroup = new FormGroup({
    textInput: new FormControl(null, [Validators.required, Validators.minLength(3)]),
    select: new FormControl(null, [Validators.required, Validators.minLength(3)]),
  });

}
