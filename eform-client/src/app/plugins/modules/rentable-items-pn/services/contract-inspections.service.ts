import { Injectable } from '@angular/core';
import {BaseService} from '../../../../common/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {ContractInspectionModel, ContractInspectionsRequestModel} from '../models';
import {Observable} from 'rxjs';
import {OperationResult} from '../../../../common/models';

const InspectionsMethods = {
  Inspections: 'api/inspections',
  CreateInspections: 'api/inspections/create-inspection',
  UpdateInspection: 'api/inspections/update-inspection',
  DeleteInspection: 'api/inspections/delete-inspection'
};

@Injectable()

export class ContractInspectionsService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllInspections(model: ContractInspectionsRequestModel): Observable<any> {
    return this.post(InspectionsMethods.Inspections, model);
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
