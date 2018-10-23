import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {OwlMomentDateTimeModule} from 'ng-pick-datetime-moment';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {MY_MOMENT_FORMATS} from 'src/app/common/helpers';
import {RentableItemsPnLayoutComponent} from './layouts';
import {SharedPnModule} from '../shared/shared-pn.module';

import {RentableItemsPnService} from './services';
import {
  RentableItemsPnPageComponent,
  RentableItemsPnAddComponent,
  RentableItemsPnUpdateComponent
} from './components';
import {RentableItemsPnRouting} from './rentable-items-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RentableItemsPnRouting,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    OwlMomentDateTimeModule,
    MDBRootModule,
    FormsModule,
    TranslateModule,
    SharedPnModule
  ],
  declarations: [
    RentableItemsPnPageComponent,
    RentableItemsPnAddComponent,
    RentableItemsPnUpdateComponent,
    RentableItemsPnLayoutComponent
  ],
  providers: [
    RentableItemsPnService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class RentableItemsPnModule {
}
