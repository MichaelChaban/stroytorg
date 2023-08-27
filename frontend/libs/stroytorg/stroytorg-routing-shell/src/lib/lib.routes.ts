import { Route } from '@angular/router';

export const stroytorgRoutingShellRoutes: Route[] = [
  {
    path: 'welcome',
    loadChildren: () =>
      import('@frontend/stroytorg/main-page').then((c) => c.mainPageRoutes),
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
