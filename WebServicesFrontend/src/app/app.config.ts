//Defines the application config logic that tells Angular how to assemble the application. 
//As you add more providers to the app, they must be declared here.

import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)]
};
