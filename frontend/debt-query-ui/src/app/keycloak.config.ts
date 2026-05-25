import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';

import {
  provideKeycloak,
  withAutoRefreshToken,
  AutoRefreshTokenService,
  UserActivityService,
  INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
  createInterceptorCondition,
  IncludeBearerTokenCondition,
} from 'keycloak-angular';

import { AppConfigService } from './core/config/config.service';



export function provideKeycloakAngular(configService: AppConfigService): EnvironmentProviders {
  const localhostCondition = createInterceptorCondition<IncludeBearerTokenCondition>({
    urlPattern: new RegExp(configService.apiPattern,  'i'),
  });

  return makeEnvironmentProviders([
    provideKeycloak({
      config: configService.keycloakConfig,

      initOptions: {
        onLoad: 'check-sso',

        silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html',

        redirectUri: window.location.origin + '/',
      },

      features: [
        withAutoRefreshToken({
          onInactivityTimeout: 'logout',
          sessionTimeout: 60000,
        }),
      ],

      providers: [
        AutoRefreshTokenService,
        UserActivityService,

        {
          provide: INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,

          useValue: [localhostCondition]
        },
      ],
    }),
  ]);
}
