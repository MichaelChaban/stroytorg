import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
} from '@angular/router';
import { appRoutes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withJsonpSupport } from '@angular/common/http';
import { StoreModule, provideStore } from '@ngrx/store';
import { HttpErrorMessageInterceptor } from '@stroytorg/shared';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes,
      withEnabledBlockingInitialNavigation(),
      ),
    provideHttpClient(withJsonpSupport()),
    provideStore(),
    provideHttpClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorMessageInterceptor,
      multi: true
    },
    importProvidersFrom(
      BrowserAnimationsModule,
      StoreModule.forRoot(),
      EffectsModule.forRoot([]),
    )
  ],
};
