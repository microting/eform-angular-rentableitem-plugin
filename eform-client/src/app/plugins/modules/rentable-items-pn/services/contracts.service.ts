import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {BaseService} from '../../../../common/services/base.service';
import {ContractModel, ContractsRequestModel} from '../models';
import {Observable} from 'rxjs';
import {OperationResult} from '../../../../common/models';
// import {CustomersPnRequestModel} from '../../customers-pn/models/customer';

const ContractMethods = {
  Contracts: 'api/contracts',
  CreateContract: 'api/contracts/create-contract',
  UpdateContract: 'api/contracts/update-contract',
  DeleteContract: 'api/contracts/delete-contract',
  // Customers: 'api/contracts/customers'
};

@Injectable()
export class ContractsService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllContracts(model: ContractsRequestModel): Observable<any> {
    return this.post(ContractMethods.Contracts, model);
  }

  createContract(model: ContractModel): Observable<any> {
    return this.post(ContractMethods.CreateContract, model);
  }

  updateContract(model: ContractModel): Observable<any> {
    return this.post(ContractMethods.UpdateContract, model);
  }

  deleteContract(contractId: number): Observable<OperationResult> {
    return this.delete(ContractMethods.DeleteContract + '/' + contractId);
  }
  // getAllCustomers(model: CustomersPnRequestModel): Observable<any> {
  //   return this.post(ContractMethods.Customers, model);
  // }
}
