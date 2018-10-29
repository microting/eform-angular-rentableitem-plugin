export class ContractModel {
  id: number;
  contractStart: Date;
  contractEnd: Date;
  customerId: number;
  contractNr: number;
  constructor() {
    this.contractStart = null;
    this.contractEnd = null;
  }
}
