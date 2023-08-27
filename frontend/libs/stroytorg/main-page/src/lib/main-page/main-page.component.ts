import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '@frontend/shared/button';
import { ButtonStyle, ButtonType } from '@frontend/shared/domain';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  buttonStyle = ButtonStyle.WARNING_BUTTON;
  buttonType = ButtonType.ICON_BUTTON;
}
