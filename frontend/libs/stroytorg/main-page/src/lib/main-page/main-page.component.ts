import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '@frontend/shared/input';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [ CommonModule, InputComponent ],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent { }
