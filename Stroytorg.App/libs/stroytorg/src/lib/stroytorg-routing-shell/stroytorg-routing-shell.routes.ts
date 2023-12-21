import { Route } from '@angular/router';

export const StroytorgRoutingShell: Route[] = [
  {
    path: 'home',
    loadChildren: () =>
      import('@stroytorg/stroytorg').then((c) => c.StroytorgHomePageComponent),
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
