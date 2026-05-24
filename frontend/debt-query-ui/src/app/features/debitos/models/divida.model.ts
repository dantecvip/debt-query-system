import { ParcelaModel } from "./parcela.model";

export interface DividaModel {
  id: string;
  descricao: string;
  valorTotalDividaAtualizada: number;
  parcelas: ParcelaModel[];
}