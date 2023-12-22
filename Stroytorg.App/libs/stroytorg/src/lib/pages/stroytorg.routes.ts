import { Route } from '@angular/router';
import { StroytorgHomeComponent } from './stroytorg-home/stroytorg-home.component';

export const StroytorgRoutes: Route[] = [
  {
    path: 'home',
    component: StroytorgHomeComponent,
  },
  {
    path: '*',
    redirectTo: ''
  },
  {
    path: '**',
    redirectTo: ''
  },
];
