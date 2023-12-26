import { Route } from '@angular/router';
import { StroytorgRoutes } from '@stroytorg/stroytorg';

export const appRoutes: Route[] = [
    {
        path: '',
        children: [...StroytorgRoutes]
    },
];