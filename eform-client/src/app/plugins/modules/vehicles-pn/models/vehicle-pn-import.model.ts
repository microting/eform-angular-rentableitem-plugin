import {VehiclesPnHeadersModel} from './vehicles-pn-headers.model';

export class VehiclePnImportModel {
  importList: string;
  headerList: Array<VehiclesPnHeadersModel> = [];
  headers: string;
}
