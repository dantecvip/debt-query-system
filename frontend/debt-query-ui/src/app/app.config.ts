import { ApplicationConfig, inject, LOCALE_ID, provideAppInitializer, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideEnvironmentNgxMask } from 'ngx-mask';

import { routes } from './app.routes';

import 'zone.js';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { AppConfigService } from './core/config/config.service';

registerLocaleData(localePt);

export const appConfig: ApplicationConfig = {
  providers: [
    provideAppInitializer(() => {
      const config = inject(AppConfigService);
      return config.loadConfig();
    }),
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideEnvironmentNgxMask()
  ]
};

