import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faHome, faList, faSignInAlt, faSignOutAlt, faUser } from '@fortawesome/free-solid-svg-icons';
import { RouterModule } from '@angular/router';
import { UserModel } from '../../../../core/models/user.model';

@Component({
  selector: 'app-header',
  imports: [
    RouterModule,
    FontAwesomeModule
  ],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  collapsed = true;
  faHome = faHome;
  faList = faList;
  faUser = faUser;
  faSignInAlt = faSignInAlt;
  faSignOutAlt = faSignOutAlt;
  authenticated = true;
  user: UserModel | undefined;

  constructor() {
    this.user = this.mockUser();
  }

  login() {
    this.authenticated = true;
    this.user = this.mockUser();
  }

  logout() {
    this.authenticated = false;
    this.user = undefined;
  }

  private mockUser() {
    let userMock = {
      name: 'John Doe',
      email: 'johndoe@example.com',
      username: 'johndoe'
    };

    return userMock;
  }
}
