import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonType, StroytorgButtonComponent, TooltipDefinition, StroytorgTextInputComponent } from '@stroytorg/stroytorg-components';
import { Icon } from '@stroytorg/shared';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'stroytorg-stroytorg-home',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent, StroytorgTextInputComponent, ReactiveFormsModule],
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

  someFunction() {
    console.log('some function');
  }

  formGroup = new FormGroup({
    formControl: new FormControl('null', [Validators.required])
  });

}
