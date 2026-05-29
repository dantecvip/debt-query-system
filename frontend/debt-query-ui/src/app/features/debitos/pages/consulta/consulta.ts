import { Component, inject } from '@angular/core';
import { DebitoService } from '../../services/debitos.service';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { faEye } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { RouterModule } from '@angular/router';
import { DividaResumoModel } from '../../models/divida.resumo.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-consulta',
  imports: [CommonModule, RouterModule, FormsModule, LoadingSpinner, FontAwesomeModule, NgxMaskDirective],
  templateUrl: './consulta.html',
  styleUrl: './consulta.scss',
  providers: [provideNgxMask()]
})
export class Consulta {
  produtos: DividaResumoModel[] = [];
  cpf: string = "";
  private readonly debitoService = inject(DebitoService);
  isLoading = false;
  errorMessage: string = "";
  faEye = faEye;

  private refreshTable(callback: () => void) {
    //console.log('Iniciando busca por débitos para CPF:', this.cpf);
    if(this.cpf.trim() === "") {
	  Swal.fire({
        icon: 'error',
        title: 'Erro',
        text: "Por favor, insira um CPF válido.",
      });
      //this.errorMessage = "Por favor, insira um CPF válido.";
      this.produtos = [];
      return;
    }

    this.isLoading = true;
    this.debitoService.obterResumoDebitosPorCpf(this.cpf).subscribe((data) => {
      //console.log('Resposta recebida do serviço:', data);
      this.produtos = data.dividas;
      //console.log('Débitos carregados:', this.produtos);
      callback();
    }, error => {
      this.isLoading = false;
      //this.errorMessage = error.error.title;
      Swal.fire({
        icon: 'error',
        title: 'Erro',
        text: error.error.title,
      });
      this.produtos = [];
    });
  }

  buscarDebitos() {
    this.refreshTable(() => {
      this.isLoading = false;
    });
  }
}
