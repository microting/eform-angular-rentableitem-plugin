import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {BaseService} from 'src/app/common/services/base.service';
import {RentableItemPnModel, RentableItemsImportModel, RentableItemsPnRequestModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {OperationDataResult, OperationResult} from '../../../../common/models';

const RentableItemsMethods = {
  RentableItemPn: '/api/rentable-items-pn/rentable-items',
  CreateRentableItemPn: '/api/rentable-items-pn/rentable-items',
  ReadRenatebleItemPn: '/api/rentable-items-pn/rentable-items',
  UpdateRentableItemPn: '/api/rentable-items-pn/rentable-items',
  DeleteRentableItem: '/api/rentable-items-pn/rentable-items',
  GetEmail: '/api/rentable-items-pn/mail'
};

@Injectable()
export class RentableItemsPnService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllRentableItems(model: RentableItemsPnRequestModel): Observable<any> {
    return this.get(RentableItemsMethods.RentableItemPn, model);
  }

  createRentableItem(model: RentableItemPnModel): Observable<any> {
    return this.post(RentableItemsMethods.CreateRentableItemPn, model);
  }

  readReantableItem(itemId: number): Observable<OperationDataResult<any>> {
    return this.get(RentableItemsMethods.ReadRenatebleItemPn + '/' + itemId);
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

  getEmail(): Observable<OperationResult> {
    return this.get(RentableItemsMethods.GetEmail, '');
  }
}
