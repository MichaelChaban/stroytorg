import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '@frontend/shared/form';
import { ButtonProperties, ButtonStyle, Icons, InputType } from '@frontend/shared/domain';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [ CommonModule, InputComponent ],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  button: ButtonProperties = {
    buttonStyle: ButtonStyle.PRIMARY_BUTTON,
    icon: Icons.HOME,
    tooltip: { tooltipPosition: 'above', tooltipText: 'Пошук' }
  };
  

  inputType = InputType.DATE;
}
