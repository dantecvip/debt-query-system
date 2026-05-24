import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClienteModel } from '../models/cliente.model';
import { environment } from '../../../../environments/environment';
import { ClienteResumoModel } from '../models/cliente.resumo.model';

@Injectable({
  providedIn: 'root'
})
export class DebitoService {
  private readonly http = inject(HttpClient);

  obterResumoDebitosPorCpf(cpf: string): Observable<ClienteResumoModel> {
    const cpfLimpo = cpf.replace(/\D/g, ''); 
    
    return this.http.get<ClienteResumoModel>(`${environment.apiUrl}/debitos/resumo/${cpfLimpo}`);
  }

  obterDebitosPorCpf(cpf: string): Observable<ClienteModel> {
    const cpfLimpo = cpf.replace(/\D/g, ''); 
    
    return this.http.get<ClienteModel>(`${environment.apiUrl}/debitos/${cpfLimpo}`);
  }
}