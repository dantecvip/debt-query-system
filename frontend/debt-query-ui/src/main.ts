import { bootstrapApplication } from '@angular/platform-browser';

import { App } from './app/app';
import { appConfig } from './app/app.config';

import { AppConfigService } from './app/core/config/config.service';

async function bootstrap() {
  const configService = new AppConfigService();
  await configService.loadConfig();

  const config = appConfig(configService);

  await bootstrapApplication(App, config);
}

bootstrap().catch((err) => console.error(err));