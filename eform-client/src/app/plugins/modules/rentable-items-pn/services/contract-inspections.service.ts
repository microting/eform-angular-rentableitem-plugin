import { Injectable } from '@angular/core';
import {BaseService} from '../../../../common/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ContractInspectionModel, ContractInspectionsRequestModel} from '../models';
import {Observable} from 'rxjs';
import {OperationDataResult, OperationResult} from '../../../../common/models';

const InspectionsMethods = {
  Inspections: 'api/rentable-items-pn/inspections',
  ReadInspection: 'api/rentable-items-pn/inspections',
  CreateInspections: 'api/rentable-items-pn/inspections',
  UpdateInspection: 'api/rentable-items-pn/inspections',
  DeleteInspection: 'api/rentable-items-pn/inspections'
};

@Injectable()

export class ContractInspectionsService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllInspections(model: ContractInspectionsRequestModel): Observable<any> {
    return this.get(InspectionsMethods.Inspections, model);
  }

  getInspection(inspectionId: number): Observable<OperationDataResult<any>> {
    return this.get(InspectionsMethods.ReadInspection + '/' + inspectionId);
  }

  createInspection(model: ContractInspectionModel): Observable<any> {
    return this.post(InspectionsMethods.CreateInspections, model);
  }

  updateInspection(model: ContractInspectionModel): Observable<any> {
    // debugger;
    return this.post(InspectionsMethods.UpdateInspection, model);
  }
  deleteInspection(inspectionId: number): Observable<OperationResult> {
    return this.delete(InspectionsMethods.DeleteInspection + '/' + inspectionId);
  }
}
