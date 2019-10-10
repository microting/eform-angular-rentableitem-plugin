import {RentableItemPnModel} from './rentable-item-pn.model';

export class ContractModel {
  id: number;
  contractStart: Date;
  contractEnd: Date;
  customerId: number;
  contractNr: number;
  rentableItems: Array<RentableItemPnModel> = [];
  rentableItemIds: Array<number> = [];
}
