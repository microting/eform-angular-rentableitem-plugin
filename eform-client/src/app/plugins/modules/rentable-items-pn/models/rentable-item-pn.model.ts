export class RentableItemPnModel {
  id: number;
  brand: string;
  modelName: string;
  registrationDate: Date;
  vinNumber: string;
  serialNumber: string;
  plateNumber: string;
  eFormId: number;
  constructor(brand?: string, modelName?: string) {
    this.registrationDate = null;
    this.brand = brand;
    this.modelName = modelName;
  }
}
