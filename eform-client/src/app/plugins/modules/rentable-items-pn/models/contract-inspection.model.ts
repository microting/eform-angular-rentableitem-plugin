export class ContractInspectionModel {
  id: number;
  contractId: number;
  sdkCaseId: number;
  status: number;
  doneAt: Date;
  siteId: number;
  constructor() {
    this.doneAt = null;
  }
}
