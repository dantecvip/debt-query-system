import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { DebitoService } from '../../services/debitos.service';
import { CommonModule } from '@angular/common';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { ClienteModel } from '../../models/cliente.model';
import Swal from 'sweetalert2';

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
  isLoading = false;
  errorMessage: string = "";

  ngOnInit(): void {
    const cpf = this.route.snapshot.paramMap.get('cpf');
    
    if (cpf) {
      this.isLoading = true;
      this.debitoService.obterDebitosPorCpf(cpf).subscribe({
        next: (dados) => {
          console.log('Dados carregados com sucesso:', dados);
          this.cliente = dados;
          this.isLoading = false;
        }, error: (error) => {
          this.isLoading = false;
          Swal.fire({
            icon: 'error',
            title: 'Erro',
            text: error.error.title,
          });
        }
      });
    }
  }
}