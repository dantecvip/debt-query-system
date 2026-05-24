export interface ParcelaModel {
  numeroParcela: number;
  dataVencimento: Date | string;
  valorOriginal: number;
  diasAtraso: number;
  valorMulta: number;
  valorJuros: number;
  valorTotalAtualizado: number;
}