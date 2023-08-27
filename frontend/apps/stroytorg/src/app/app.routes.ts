import { Route } from '@angular/router';
export const appRoutes: Route[] = [
    {
        path: '',
        loadChildren: () => import('@frontend/stroytorg/stroytorg-routing-shell').then(c => c.stroytorgRoutingShellRoutes),
    },
];