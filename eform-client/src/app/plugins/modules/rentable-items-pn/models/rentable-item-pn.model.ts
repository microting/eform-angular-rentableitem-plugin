export class RentableItemPnModel {
  id: number;
  brand: string;
  modelName: string;
  registrationDate: Date;
  vinNumber: string;
  serialNumber: string;
  plateNumber: string;
  constructor() {
    this.registrationDate = null;
  }
}
