import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateComponent, InputComponent } from '@frontend/shared/form';
import { ButtonProperties, ButtonStyle, Icon, InputType } from '@frontend/shared/domain';
import { SelectComponent } from '@frontend/shared/form/src/lib/form/components/select';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [ CommonModule, InputComponent, DateComponent, SelectComponent ],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  button: ButtonProperties = {
    icon: Icon.HOME,
    label: 'Home',
    tooltip: { tooltipPosition: 'above', tooltipText: 'Пошук' }
  };

  inputType = InputType.DATE;
}
