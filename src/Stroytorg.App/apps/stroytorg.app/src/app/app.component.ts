import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarModel, StroytorgNavbarComponent, StroytorgSnackbarComponent } from '@stroytorg/stroytorg-components';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  standalone: true,
  imports: [RouterModule, StroytorgNavbarComponent, StroytorgSnackbarComponent],
  selector: 'stroytorg-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'stroytorg.app';
  pages: NavbarModel[] = [
    {
      name: 'Home',
      link: '/home',
      active: true
    },
    {
      name: 'Materials',
      link: '/materials',
      active: false
    },
    {
      name: 'About',
      link: '/about',
      active: false
    }
  ]
}
