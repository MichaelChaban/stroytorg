import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [ CommonModule ],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent { }
