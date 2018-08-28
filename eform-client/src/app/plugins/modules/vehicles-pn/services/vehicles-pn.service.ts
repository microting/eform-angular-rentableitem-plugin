import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {BaseService} from 'src/app/common/services/base.service';
import {VehiclePnModel, VehiclesPnRequestModel} from 'src/app/plugins/modules/vehicles-pn/models';


const VehiclePnMethods = {
  VehiclePn: 'api/vehicles-pn',
  CreateVehiclePn: 'api/vehicles-pn/create-vehicle',
  UpdateVehiclePn: 'api/vehicles-pn/update-vehicle'
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
}
