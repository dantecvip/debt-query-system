import {
  ApplicationConfig,
  LOCALE_ID,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection
} from '@angular/core';

import { provideRouter } from '@angular/router';

import {
  provideHttpClient,
  withInterceptors
} from '@angular/common/http';

import { provideEnvironmentNgxMask } from 'ngx-mask';

import {
  includeBearerTokenInterceptor
} from 'keycloak-angular';

import { routes } from './app.routes';

import 'zone.js';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { AppConfigService } from './core/config/config.service';

registerLocaleData(localePt);

import { provideKeycloakAngular } from './keycloak.config';
import { APP_CONFIG } from './core/config/config.token';

const response = await fetch('/assets/config.json');
const config = await response.json();

export function appConfig(
  configService: AppConfigService
): ApplicationConfig {
  return {
    providers: [
      provideZoneChangeDetection({
        eventCoalescing: true
      }),
      { provide: APP_CONFIG, useValue: config },

      provideKeycloakAngular(configService),

      {
        provide: LOCALE_ID,
        useValue: 'pt-BR'
      },

      provideHttpClient(
        withInterceptors([
          includeBearerTokenInterceptor
        ])
      ),

      provideBrowserGlobalErrorListeners(),

      provideRouter(routes),

      provideEnvironmentNgxMask()
    ]
  };
}