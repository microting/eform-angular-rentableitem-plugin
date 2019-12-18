export class ContractInspectionModel {
  id: number;
  contractId: number;
  sdkCaseId: number;
  eFormId: number;
  status: number;
  doneAt: Date;
  siteId: number;
  constructor() {
    this.doneAt = null;
  }
}
