import {RentableItemsPnHeadersModel} from './rentableItems-pn-headers.model';

export class RentableItemsImportModel {
  importList: string;
  eFormID: number;
  headers: string;
  headerList: Array<RentableItemsPnHeadersModel> = [];
}


