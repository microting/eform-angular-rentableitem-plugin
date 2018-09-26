import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {SharedPnModule} from 'src/app/plugins/modules/shared/shared-pn.module.js';

import {VehiclesPnService} from './services';
import {
  VehiclesPnPageComponent,
  VehiclesPnAddComponent,
  VehiclesPnUpdateComponent
} from './components';
import {VehiclesPnRouting} from './vehicles-pn.routing';
import {OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';

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
  providers: [VehiclesPnService]
})
export class VehiclesPnModule {
}
