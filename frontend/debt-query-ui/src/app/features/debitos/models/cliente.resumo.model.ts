import { DividaResumoModel } from "./divida.resumo.model";

export interface ClienteResumoModel {
  cpf: string;
  nome: string;
  email: string;
  telefone: string;
  valorOriginalTotal: number;
  valorBaseAtualizadoTotal: number;
  valorTotalConsolidado: number;
  dividas: DividaResumoModel[];
}