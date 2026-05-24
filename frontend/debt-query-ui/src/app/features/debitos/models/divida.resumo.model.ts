import { ParcelaModel } from "./parcela.model";

export interface DividaResumoModel {
  id: string;
  descricao: string;
  valorOriginalTotal: number;
  valorBaseAtualizadoTotal: number;
  valorTotalDividaAtualizada: number;
  parcelas: ParcelaModel[];
}