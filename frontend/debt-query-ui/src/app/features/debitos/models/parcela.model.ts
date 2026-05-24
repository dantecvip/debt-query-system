export interface ParcelaModel {
  id: string;
  parcelaNumero: number;
  dataVencimento: Date | string;
  valorOriginal: number;
  diasAtraso: number;
  valorMulta: number;
  valorJuros: number;
  taxaAdministrativa: number;
  valorTotalAtualizado: number;
}