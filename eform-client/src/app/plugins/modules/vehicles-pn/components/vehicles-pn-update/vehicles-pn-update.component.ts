import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {VehiclePnModel} from 'src/app/plugins/modules/vehicles-pn/models';
import {VehiclesPnService} from 'src/app/plugins/modules/vehicles-pn/services';

@Component({
  selector: 'app-vehicles-pn-update',
  templateUrl: './vehicles-pn-update.component.html',
  styleUrls: ['./vehicles-pn-update.component.scss']
})
export class VehiclesPnUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onVehicleUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedVehicleModel: VehiclePnModel = new VehiclePnModel();
  spinnerStatus = false;

  constructor(private vehiclesService: VehiclesPnService) { }

  ngOnInit() {
  }

  show(vehicleModel: VehiclePnModel) {
    this.selectedVehicleModel = vehicleModel;
    this.frame.show();
  }

  updateVehicle() {
    this.spinnerStatus = true;
    this.vehiclesService.updateVehicle(this.selectedVehicleModel).subscribe(((data) => {
      if (data && data.success) {
        this.onVehicleUpdated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }

}
