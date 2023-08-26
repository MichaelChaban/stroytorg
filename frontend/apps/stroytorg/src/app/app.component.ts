import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MainPageComponent } from '@frontend/stroytorg/main-page';
import { AppRoutingModule } from './app.routes';

@Component({
  standalone: true,
  imports: [RouterModule, MainPageComponent, AppRoutingModule],
  selector: 'frontend-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'stroytorg';
}
