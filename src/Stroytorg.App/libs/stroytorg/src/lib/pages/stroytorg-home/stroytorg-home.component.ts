import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonType, StroytorgButtonComponent, TooltipDefinition } from '@stroytorg/stroytorg-components';
import { Icon } from '@stroytorg/shared';

@Component({
  selector: 'stroytorg-stroytorg-home',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent],
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
}
