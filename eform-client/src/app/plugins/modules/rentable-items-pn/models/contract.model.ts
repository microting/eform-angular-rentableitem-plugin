import {RentableItemPnModel} from './rentable-item-pn.model';
import {CustomerModel} from 'src/app/plugins/modules/rentable-items-pn/models/customer.model';

export class ContractModel {
  id: number;
  contractStart: Date;
  contractEnd: Date;
  customer: CustomerModel;
  customerId: number;
  contractNr: number;
  rentableItems: Array<RentableItemPnModel> = [];
  rentableItemIds: Array<number> = [];
  deleteIds: Array<number> = [];
}
