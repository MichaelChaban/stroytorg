import { Route } from '@angular/router';

export const appRoutes: Route[] = [
    {
        path: '',
        loadChildren: () => import('@stroytorg/stroytorg').then(c => c.StroytorgRoutingShell),
    },
];