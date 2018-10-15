import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {VehicleInspectionPnModel, VehiclePnModel} from '../../models';
import {VehiclesPnService} from '../../services';

@Component({
  selector: 'app-vehicle-inspection-pn-add',
  templateUrl: './vehicle-inspection-pn-add.component.html',
  styleUrls: ['./vehicle-inspection-pn-add.component.scss']
})
export class VehicleInspectionPnAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onInspectionCreated: EventEmitter<void> = new EventEmitter<void>();
  newInspectionModel: VehicleInspectionPnModel = new VehicleInspectionPnModel();
  spinnerStatus = false;
  frameShow = true;
  selectedVehicleModel: VehiclePnModel = new VehiclePnModel();
  constructor(private vehiclesService: VehiclesPnService) { }

  ngOnInit() {
  }

  show(vehicleModel: VehiclePnModel) {
    this.newInspectionModel = new VehicleInspectionPnModel();
    this.selectedVehicleModel = vehicleModel;

    this.frame.show();
  }

  createInspection() {
    this.spinnerStatus = true;
    this.vehiclesService.createVehicleInspection(this.newInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        // this.newInspectionModel = this.selectedVehicleModel;
        this.onInspectionCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
      }));
  }
}
