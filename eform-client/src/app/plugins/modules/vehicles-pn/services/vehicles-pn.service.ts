import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {BaseService} from 'src/app/common/services/base.service';
import {VehicleInspectionPnModel, VehiclePnModel, VehiclesPnRequestModel} from 'src/app/plugins/modules/vehicles-pn/models';
import {VehiclesPnImportComponent} from '../components/vehicles-pn-import/vehicles-pn-import.component';
import {VehiclePnImportModel} from '../models/vehicle-pn-import.model';


const VehiclePnMethods = {
  VehiclePn: 'api/vehicles-pn',
  CreateVehiclePn: 'api/vehicles-pn/create-vehicle',
  UpdateVehiclePn: 'api/vehicles-pn/update-vehicle',
  ImportVehicle: 'api/vehicles-pn/import-vehicles',
  VehicleInspectionPn: 'api/vehicleInspections-pn',
  CreateVehicleInspectionPn: 'api/vehicleInspections-pn/create-vehicleInspection',
  UpdateVehicleInspectionPn: 'api/vehicleInspections-pn/update-vehicleInspection'
};

@Injectable()
export class VehiclesPnService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllVehicles(model: VehiclesPnRequestModel): Observable<any> {
    return this.post(VehiclePnMethods.VehiclePn, model);
  }

  updateVehicle(model: VehiclePnModel): Observable<any> {
    return this.post(VehiclePnMethods.UpdateVehiclePn, model);
  }

  createVehicle(model: VehiclePnModel): Observable<any> {
    return this.post(VehiclePnMethods.CreateVehiclePn, model);
  }

  getAllVehicleInspections(model: VehiclesPnRequestModel): Observable<any> {
    return this.post(VehiclePnMethods.VehicleInspectionPn, model);
  }

  updateVehicleInspection(model: VehicleInspectionPnModel): Observable<any> {
    return this.post(VehiclePnMethods.UpdateVehicleInspectionPn, model);
  }

  createVehicleInspection(model: VehicleInspectionPnModel): Observable<any> {
    return this.post(VehiclePnMethods.CreateVehicleInspectionPn, model);
  }
  importVehicle(model: VehiclePnImportModel): Observable<any> {
    return this.post(VehiclePnMethods.ImportVehicle, model);
  }
}
