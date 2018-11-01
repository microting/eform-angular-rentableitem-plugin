import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {OwlMomentDateTimeModule} from 'ng-pick-datetime-moment';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {MY_MOMENT_FORMATS} from 'src/app/common/helpers';
import {RentableItemsPnLayoutComponent} from './layouts';
import {SharedPnModule} from '../shared/shared-pn.module';

import {ContractsService, InspectionsService, RentableItemsPnService} from './services';
import {
  RentableItemsPnPageComponent,
  RentableItemsPnAddComponent,
  RentableItemsPnUpdateComponent
} from './components';
import {RentableItemsPnRouting} from './rentable-items-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';
import { ContractsAddComponent } from './components/contracts-add/contracts-add.component';
import { InspectionsAddComponent } from './components/inspections-add/inspections-add.component';
import { InspectionsUpdateComponent } from './components/inspections-update/inspections-update.component';
import { ContractsUpdateComponent } from './components/contracts-update/contracts-update.component';
import { ContractsPageComponent } from './components/contracts-page/contracts-page.component';
import { InspectionsPageComponent } from './components/inspections-page/inspections-page.component';

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
    RentableItemsPnLayoutComponent,
    ContractsAddComponent,
    InspectionsAddComponent,
    InspectionsUpdateComponent,
    ContractsUpdateComponent,
    ContractsPageComponent,
    InspectionsPageComponent
  ],
  providers: [
    RentableItemsPnService,
    ContractsService,
    InspectionsService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class RentableItemsPnModule {
}
