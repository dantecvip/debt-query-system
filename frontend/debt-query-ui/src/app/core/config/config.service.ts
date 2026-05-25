import { Injectable } from '@angular/core';

export interface AppConfig {
  apiUrl: string;
  apiPattern: string;
  keycloakConfig: {
    realm: string;
    url: string;
    clientId: string;
  };
}

@Injectable({ providedIn: 'root' })
export class AppConfigService {
  private config!: AppConfig;

  loadConfig(): Promise<void> {
    return fetch('/assets/config.json')
      .then(res => res.json())
      .then(cfg => {
        this.config = cfg;
      });
  }

  get apiUrl(): string {
    return this.config.apiUrl;
  }

  get apiPattern(): string {
    return this.config.apiPattern;
  }

  get keycloakConfig(): any {
    return this.config.keycloakConfig;
  }
}