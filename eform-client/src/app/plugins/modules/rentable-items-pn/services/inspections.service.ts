import { Injectable } from '@angular/core';
import {BaseService} from '../../../../common/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {InspectionModel, InspectionsRequestModel} from '../models';
import {Observable} from 'rxjs';

const InspectionsMethods = {
  Inspections: 'api/inspections',
  CreateInspections: 'api/create-inspection',
  UpdateInspection: 'api/update-inspection'
};

@Injectable()

export class InspectionsService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getAllInspections(model: InspectionsRequestModel): Observable<any> {
    return this.post(InspectionsMethods.Inspections, model);
  }

  createInspection(model: InspectionModel): Observable<any> {
    return this.post(InspectionsMethods.CreateInspections, model);
  }

  updateInspection(model: InspectionModel): Observable<any> {
    return this.put(InspectionsMethods.UpdateInspection, model);
  }
}
