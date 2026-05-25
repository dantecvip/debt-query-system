import { ParcelaModel } from "./parcela.model";

export interface DividaModel {
  id: string;
  descricao: string;
  valorOriginalTotal: number;
  valorBaseAtualizadoTotal: number;
  valorTotalDividaAtualizada: number;
  parcelas: ParcelaModel[];
}