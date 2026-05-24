import { Routes } from '@angular/router';
import { Consulta } from './pages/consulta/consulta';
import { Detalhe } from './pages/detalhe/detalhe';

export const DEBITOS_ROUTES: Routes = [
  { path: 'consulta', component: Consulta },
  { path: 'detalhe/:cpf', component: Detalhe },
  { path: '', redirectTo: 'consulta', pathMatch: 'full' }
];