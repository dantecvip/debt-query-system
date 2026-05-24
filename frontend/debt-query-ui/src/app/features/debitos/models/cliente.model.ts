import { DividaModel } from "./divida.model";

export interface ClienteModel {
  cpf: string;
  nomeCliente: string;
  totalConsolidado: number;
  dividas: DividaModel[];
}