import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StroytorgButtonComponent } from '@stroytorg/stroytorg-components';

@Component({
  selector: 'stroytorg-stroytorg-home',
  standalone: true,
  imports: [CommonModule, StroytorgButtonComponent],
  templateUrl: './stroytorg-home.component.html',
  styleUrl: './stroytorg-home.component.scss',
})
export class StroytorgHomeComponent {

  showAlert() {
    alert('Tvoje mama xd');
  }
}
