import { Routes } from '@angular/router';
import { NotFound } from './features/pages/not-found/not-found';
import { Forbidden } from './features/pages/forbidden/forbidden';
import { canActivateAuthRole } from './core/guards/auth.role.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadComponent: () => import('./features/home/home').then((m) => m.Home),
  },
  {
    path: 'debitos',
    loadChildren: () => import('./features/debitos/debitos.routes').then((m) => m.DEBITOS_ROUTES),
    canActivate: [canActivateAuthRole],
    data: { role: 'view-debts' },
  },
  {
    path: 'perfil',
    loadComponent: () => import('./features/user-profile/user-profile').then((m) => m.UserProfile),
    canActivate: [canActivateAuthRole],
    data: { role: 'view-profile' },
  },
  { path: 'forbidden', component: Forbidden },
  { path: 'not-found', component: NotFound },
  { path: '**', redirectTo: 'not-found' },
];
