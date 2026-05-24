import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './shared/components/layout/header/header';
import { Footer } from './shared/components/layout/footer/footer';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer],
  styles: [`
      .content-container {
        margin-top: 80px;
        margin-bottom: 50px;
      }
  `],
  template: `
    <app-header></app-header>
    <div class="content-container">
      <router-outlet></router-outlet>
    </div>
    <app-footer></app-footer>
  `
})
export class App {
}