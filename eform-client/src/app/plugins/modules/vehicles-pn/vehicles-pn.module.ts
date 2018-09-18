import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {NgDatepickerModule} from 'src/app/common/modules/eform-imported/ng-datepicker/module/ng-datepicker.module';
import {SharedPnModule} from 'src/app/plugins/modules/shared/shared-pn.module.js';

import {VehiclesPnService} from './services';
import {
  VehiclesPnPageComponent,
  VehiclesPnAddComponent,
  VehiclesPnUpdateComponent
} from './components';
import {VehiclesPnRouting} from './vehicles-pn.routing';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehiclesPnRouting,
    MDBRootModule,
    FormsModule,
    TranslateModule,
    NgDatepickerModule,
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
