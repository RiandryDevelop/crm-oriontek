import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { InjectionToken } from '@angular/core';
import { ApplicationConfig } from "@angular/core";
import { provideRouter } from '@angular/router';
import { routes }from './app-routing.module';
import {AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { provideAnimationsAsync, provideApiUrl, provideAppInitializer, provideCharts, withDefaultRegisterables } from './core/providers/index.provider';


export const APP_CONFIG = new InjectionToken<ApplicationConfig>('app.config');



export const appConfiguration: ApplicationConfig = {
  providers: [
    provideHttpClient(withFetch(), withInterceptors([AuthInterceptor, ErrorInterceptor])),
    provideAppInitializer(),
    provideApiUrl(),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideCharts(withDefaultRegisterables()),
  ],
};


