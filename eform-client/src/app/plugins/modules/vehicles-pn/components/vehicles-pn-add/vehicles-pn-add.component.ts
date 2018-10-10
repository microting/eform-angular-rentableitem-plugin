import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {VehiclePnModel} from 'src/app/plugins/modules/vehicles-pn/models';
import {VehiclesPnService} from 'src/app/plugins/modules/vehicles-pn/services';
import {formatTimezone} from 'src/app/common/helpers';

@Component({
  selector: 'app-vehicles-pn-add',
  templateUrl: './vehicles-pn-add.component.html',
  styleUrls: ['./vehicles-pn-add.component.scss']
})
export class VehiclesPnAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onVehicleCreated: EventEmitter<void> = new EventEmitter<void>();
  newVehicleModel: VehiclePnModel = new VehiclePnModel();
  spinnerStatus = false;
  frameShow = true;
  constructor(private vehiclesService: VehiclesPnService) { }

  ngOnInit() {
  }

  show() {
    this.newVehicleModel = new VehiclePnModel();
    this.frame.show();
  }

  createVehicle() {
    this.spinnerStatus = true;
    this.vehiclesService.createVehicle(this.newVehicleModel).subscribe(((data) => {
      if (data && data.success) {
        this.newVehicleModel = new VehiclePnModel();
        this.onVehicleCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }

  onRegistrationDateSelected(e: any) {
    debugger;
    this.newVehicleModel.registrationDate = formatTimezone(e.value._d);
  }
  onContractStartDateSelected(e: any) {
    this.newVehicleModel.contractStartDate = formatTimezone(e.value._d);
  }
  onContractEndDateSelected(e: any) {
    this.newVehicleModel.contractEndDate = formatTimezone(e.value._d);
  }
}
