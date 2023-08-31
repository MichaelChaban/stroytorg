import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MainPageComponent } from '@frontend/stroytorg/main-page';

@Component({
  standalone: true,
  imports: [RouterModule, MainPageComponent, MatIconModule],
  selector: 'frontend-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'stroytorg';
}
