import {RentableItemCustomerModel} from 'src/app/plugins/modules/rentable-items-pn/models/rentableItemCustomerModel';
import {RentableItemPnModel} from 'src/app/plugins/modules/rentable-items-pn/models/rentable-item-pn.model';

export class ContractInspectionModel {
  id: number;
  contractStart: Date;
  contractEnd: Date;
  contractId: number;
  sdkCaseId: number;
  eFormId: number;
  status: number;
  doneAt: Date;
  siteId: number;
  rentableItemCustomer: RentableItemCustomerModel;
  rentableItems: Array<RentableItemPnModel> = [];
  constructor() {
    this.doneAt = null;
  }
}
