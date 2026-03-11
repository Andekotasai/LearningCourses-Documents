import { ApplicationConfig, InjectionToken, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { httpInterceptorInterceptor } from './http-interceptor-interceptor';
import { CookieService } from 'ngx-cookie-service';
import { app_Config, config } from './token';

export const BaseURL = new InjectionToken<string>('BaseURL');

export const value=new InjectionToken<string>('value');

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([httpInterceptorInterceptor])),
    CookieService,
    {
      provide:BaseURL,
      useFactory:(app_Config:config)=>{
        return app_Config.api_Url;
      },
      deps:[app_Config]
    },
    {
      provide:value,
      useValue:{
        name:'AngularApp',
        version:'1.0.0'
      }
    }
  ]
};
