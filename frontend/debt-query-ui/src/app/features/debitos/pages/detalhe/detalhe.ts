import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DebitoService } from '../../services/debitos.service';

@Component({
  selector: 'app-detalhes-debito',
  standalone: true,
  templateUrl: './detalhe.html'
})
export class Detalhe implements OnInit {
  private readonly route = inject(ActivatedRoute);
  protected debitoService = inject(DebitoService);

  ngOnInit(): void {
    const cpf = this.route.snapshot.paramMap.get('cpf');
    
    if (cpf) {
      this.debitoService.obterDebitosPorCpf(cpf).subscribe({
        next: (dados) => console.log('Dados carregados com sucesso:', dados),
        error: (err) => console.error('Erro tratado pelo interceptor global:', err)
      });
    }
  }
}