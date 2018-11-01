import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {BaseService} from 'src/app/common/services/base.service';
import {RentableItemPnModel, RentableItemsPnRequestModel} from 'src/app/plugins/modules/rentable-items-pn/models';


const RentableItemsMethods = {
  RentableItemPn: 'api/rentableItems-pn',
  CreateRentableItemPn: 'api/rentbaleItems-pn/create-rentableItem',
  UpdateRentableItemPn: 'api/rentbaleItems-pn/update-rentableItem'
};

@Injectable()
export class RentableItemsPnService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllRentableItems(model: RentableItemsPnRequestModel): Observable<any> {
    return this.post(RentableItemsMethods.RentableItemPn, model);
  }

  createRentableItem(model: RentableItemPnModel): Observable<any> {
    return this.post(RentableItemsMethods.CreateRentableItemPn, model);
  }

  updateRentableItem(model: RentableItemPnModel): Observable<any> {
    return this.post(RentableItemsMethods.UpdateRentableItemPn, model);
  }

}
