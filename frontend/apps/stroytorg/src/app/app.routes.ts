import { Route, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
export const appRoutes: Route[] = [
    {
        path: '',
        loadChildren: () => import('@frontend/stroytorg/stroytorg-routing-shell').then(c => c.stroytorgRoutingShellRoutes),
    },
    {
        path: '*',
        redirectTo: ''
    },
    {
        path: '**',
        redirectTo: ''
    }
];

@NgModule({
    imports: [RouterModule.forChild(appRoutes)],
    exports: [RouterModule],
})
export class AppRoutingModule{}