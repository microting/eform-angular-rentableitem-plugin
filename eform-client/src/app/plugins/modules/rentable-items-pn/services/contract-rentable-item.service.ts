import { Injectable } from '@angular/core';
import {BaseService} from "../../../../common/services/base.service";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {Observable} from "rxjs";
import {OperationDataResult} from "../../../../common/models";
import {RentableItemsPnModel} from "../models";

const ContractRentableItemMethods = {
  getAll: '/api/rentable-items-pn/contract-rentable-items'
};


@Injectable({
  providedIn: 'root'
})
export class ContractRentableItemService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllRentableItemsFromContract(contractId: number): Observable<OperationDataResult<RentableItemsPnModel>> {
    return this.get(ContractRentableItemMethods.getAll + '/' + contractId);
  }
}
