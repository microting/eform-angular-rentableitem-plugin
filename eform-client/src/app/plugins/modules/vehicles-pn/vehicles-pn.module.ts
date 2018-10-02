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
    VehiclesPnUpdateComponent
  ],
  providers: [
    VehiclesPnService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class VehiclesPnModule {
}
