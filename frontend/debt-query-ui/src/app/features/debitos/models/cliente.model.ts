import { DividaModel } from "./divida.model";

export interface ClienteModel {
  cpf: string;
  nome: string;
  email: string;
  telefone: string;
  valorOriginalTotal: number;
  valorBaseAtualizadoTotal: number;
  valorTotalConsolidado: number;
  dividas: DividaModel[];
}