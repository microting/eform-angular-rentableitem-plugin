export class VehiclePnModel {
  id: number;
  contactNumber: string;
  customerName: string;
  brand: string;
  modelName: string;
  registrationDate: Date;
  vinNumber: string;
  contractStartDate: Date;
  contractEndDate: Date;

  constructor() {
    this.contractStartDate = null;
    this.contractEndDate = null;
    this.registrationDate = null;
  }
}
