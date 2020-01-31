import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {BaseService} from 'src/app/common/services/base.service';
import {RentableItemPnModel, RentableItemsImportModel, RentableItemsPnRequestModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {OperationResult} from '../../../../common/models';

const RentableItemsMethods = {
  RentableItemPn: 'api/rentableItems-pn',
  CreateRentableItemPn: 'api/rentableItems-pn/create-rentableItem',
  UpdateRentableItemPn: 'api/rentableItems-pn/update-rentableItem',
  DeleteRentableItem: 'api/rentableItems-pn/delete-rentableItem'
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

  deleteRentableItem(itemId: number): Observable<OperationResult> {
    return this.delete(RentableItemsMethods.DeleteRentableItem + '/' + itemId);
  }

  import(model: RentableItemsImportModel): Observable<OperationResult> {
    return this.post(RentableItemsMethods.RentableItemPn + '/import', model);
  }

}
