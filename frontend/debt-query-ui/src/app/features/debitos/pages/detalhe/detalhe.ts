import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { DebitoService } from '../../services/debitos.service';
import { CommonModule } from '@angular/common';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { ClienteModel } from '../../models/cliente.model';

@Component({
  selector: 'app-detalhe',
  imports: [CommonModule, RouterModule, LoadingSpinner, FontAwesomeModule, NgxMaskPipe],
  templateUrl: './detalhe.html',
  styleUrl: './detalhe.scss',
  providers: [provideNgxMask()]
})
export class Detalhe implements OnInit {
  private readonly route = inject(ActivatedRoute);
  protected debitoService = inject(DebitoService);
  protected cliente: ClienteModel | null = null;

  ngOnInit(): void {
    const cpf = this.route.snapshot.paramMap.get('cpf');
    
    if (cpf) {
      this.debitoService.obterDebitosPorCpf(cpf).subscribe({
        next: (dados) => {
          console.log('Dados carregados com sucesso:', dados);
          this.cliente = dados;
        },
        error: (err) => console.error('Erro tratado pelo interceptor global:', err)
      });
    }
  }
}