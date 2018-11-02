import { Injectable } from '@angular/core';
import {BaseService} from '../../../../common/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {OperationDataResult, OperationResult} from '../../../../common/models';
import {RentableItemsFieldsPnUpdateModel} from '../models/field';

export let RentableItemsFieldsMethods = {
  rentableItemsPnFields: 'api/rentable-items/fields'
};

@Injectable({
  providedIn: 'root'
})
export class RentableItemsPnFieldsService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllFields(): Observable<OperationDataResult<RentableItemsFieldsPnUpdateModel>> {
    return this.get(RentableItemsFieldsMethods.rentableItemsPnFields);
  }

  updateFields(model: RentableItemsFieldsPnUpdateModel): Observable<OperationResult> {
    return this.put(RentableItemsFieldsMethods.rentableItemsPnFields, model);
  }
}
