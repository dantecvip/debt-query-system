import { Component, inject, OnInit } from '@angular/core';
import Keycloak from 'keycloak-js';
import { CommonModule } from '@angular/common';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoadingSpinner } from '../../shared/components/loading-spinner/loading-spinner';
import { UserModel } from '../../core/models/user.model';

@Component({
  selector: 'app-user-profile',
  imports: [CommonModule, FontAwesomeModule, LoadingSpinner],
  templateUrl: './user-profile.html',
  styleUrl: './user-profile.scss',
})
export class UserProfile implements OnInit {
  private readonly keycloak = inject(Keycloak);

  user: UserModel | undefined;
  faUser = faUser

  async ngOnInit() {
    if (this.keycloak?.authenticated) {
      const profile = await this.keycloak.loadUserProfile();

      this.user = {
        name: `${profile?.firstName} ${profile.lastName}`,
        email: profile?.email,
        username: profile?.username
      };
    }
  }
}
