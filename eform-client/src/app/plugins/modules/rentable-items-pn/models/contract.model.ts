import {RentableItemPnModel} from './rentable-item-pn.model';
import {RentableItemCustomerModel} from 'src/app/plugins/modules/rentable-items-pn/models/rentableItemCustomerModel';

export class ContractModel {
  id: number;
  contractStart: Date;
  contractEnd: Date;
  rentableItemCustomer: RentableItemCustomerModel;
  customerId: number;
  contractNr: number;
  rentableItems: Array<RentableItemPnModel> = [];
  rentableItemIds: Array<number> = [];
  deleteIds: Array<number> = [];
}
