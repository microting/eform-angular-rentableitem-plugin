import { Injectable } from '@angular/core';
import {BaseService} from '../../../../common/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable} from 'rxjs';
import {OperationDataResult, OperationResult} from '../../../../common/models';
import {RentableItemsPnSettingsModel} from '../models';

export let RentableItemsSettingsMethods = {
  RentableItemsPnSettings: 'api/rentable-items/settings'
};

@Injectable({
  providedIn: 'root'
})
export class RentableItemsPnSettingsService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllSettings(): Observable<OperationDataResult<RentableItemsPnSettingsModel>> {
    return this.get(RentableItemsSettingsMethods.RentableItemsPnSettings);
  }

  updateSettings(model: RentableItemsPnSettingsModel): Observable<OperationResult> {
    return this.post(RentableItemsSettingsMethods.RentableItemsPnSettings, model);
  }
}
