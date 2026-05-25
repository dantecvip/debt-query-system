import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClienteModel } from '../models/cliente.model';
import { ClienteResumoModel } from '../models/cliente.resumo.model';
import { AppConfigService } from '../../../core/config/config.service';
import { APP_CONFIG } from '../../../core/config/config.token';

@Injectable({
  providedIn: 'root'
})
export class DebitoService {
  private readonly http = inject(HttpClient);
  private readonly config = inject(APP_CONFIG);

  obterResumoDebitosPorCpf(cpf: string): Observable<ClienteResumoModel> {
    const cpfLimpo = cpf.replace(/\D/g, ''); 
    
    return this.http.get<ClienteResumoModel>(`${this.config.apiUrl}/debitos/resumo/${cpfLimpo}`);
  }

  obterDebitosPorCpf(cpf: string): Observable<ClienteModel> {
    const cpfLimpo = cpf.replace(/\D/g, ''); 
    
    return this.http.get<ClienteModel>(`${this.config.apiUrl}/debitos/${cpfLimpo}`);
  }
}