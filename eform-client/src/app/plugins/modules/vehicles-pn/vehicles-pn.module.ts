import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {MY_MOMENT_FORMATS} from 'src/app/common/helpers';
import {SharedPnModule} from '../shared/shared-pn.module';

import {VehiclesPnService} from './services';
import {
  VehiclesPnPageComponent,
  VehiclesPnAddComponent,
  VehiclesPnUpdateComponent
} from './components';
import {VehiclesPnRouting} from './vehicles-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';
import {VehicleInspectionsPnPageComponent} from './components/vehicle-inspections-pn-page/vehicle-inspections-pn-page.component';
import { VehicleInspectionPnAddComponent } from './components/vehicle-inspection-pn-add/vehicle-inspection-pn-add.component';


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehiclesPnRouting,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MDBRootModule,
    FormsModule,
    TranslateModule,
    SharedPnModule
  ],
  declarations: [
    VehiclesPnPageComponent,
    VehiclesPnAddComponent,
    VehiclesPnUpdateComponent,
    VehicleInspectionsPnPageComponent,
    VehicleInspectionPnAddComponent
  ],
  providers: [
    VehiclesPnService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class VehiclesPnModule {
}
