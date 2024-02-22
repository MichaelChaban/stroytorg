import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideHttpClient, withJsonpSupport } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(appRoutes), provideHttpClient(withJsonpSupport())],
};
