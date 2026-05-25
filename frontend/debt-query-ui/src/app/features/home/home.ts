import { Component, effect, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BehaviorSubject, timer } from 'rxjs';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  imports: [CommonModule, RouterModule],
})
export class Home implements OnDestroy {
  authenticated = false;
  private readonly copyMessageSubject = new BehaviorSubject<string | null>(null);
  copyMessage$ = this.copyMessageSubject.asObservable(); // Expose as Observable

  private readonly keycloak = inject(Keycloak);
  private readonly keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);

  constructor() {
    effect(() => {
      const keycloakEvent = this.keycloakSignal();

      if (keycloakEvent.type === KeycloakEventType.Ready) {
        this.authenticated = typeEventArgs<ReadyArgs>(keycloakEvent.args);
      }

      if (keycloakEvent.type === KeycloakEventType.AuthLogout) {
        this.authenticated = false;
      }
    });
  }

  copyToClipboard(text: string): void {
    navigator.clipboard
      .writeText(text)
      .then(() => {
        this.copyMessageSubject.next(`"${text}" copiado!`);
        timer(3000).subscribe(() => this.copyMessageSubject.next(null));
      })
      .catch((err) => {
        this.copyMessageSubject.next('Falha ao copiar texto. Por favor, tente novamente.');
      });
  }

  ngOnDestroy(): void {
    this.copyMessageSubject.complete();
  }
}
