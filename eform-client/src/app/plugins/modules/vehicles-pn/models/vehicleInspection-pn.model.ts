export class VehicleInspectionPnModel {
  id: number;
  workflowState: string;
  version: number;
  status: number;
  createdAt: Date;
  UpdatedAt: Date;
  DoneAt: Date;
  EformId: number;
  VehicleId: number;


  constructor() {
    this.createdAt = null;
    this.UpdatedAt = null;
    this.DoneAt = null;
  }
}
