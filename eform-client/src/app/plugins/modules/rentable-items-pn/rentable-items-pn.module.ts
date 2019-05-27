import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {OwlMomentDateTimeModule} from 'ng-pick-datetime-moment';
import {MDBRootModule} from 'port/angular-bootstrap-md';
import {MY_MOMENT_FORMATS} from 'src/app/common/helpers';
import {RentableItemsPnLayoutComponent} from './layouts';
import {SharedPnModule} from '../shared/shared-pn.module';
import {NgSelectModule} from '@ng-select/ng-select';
import {RouterModule} from '@angular/router';
import {ContractsService, ContractInspectionsService, RentableItemsPnService, RentableItemsPnSettingsService} from './services';
import {
  RentableItemsPnPageComponent,
  RentableItemsPnAddComponent,
  RentableItemsPnUpdateComponent,
  RentableItemsPnFieldsComponent,
  RentableItemsSettingsComponent
} from './components';
import {RentableItemsPnRouting} from './rentable-items-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';
import { ContractsAddComponent } from './components/contracts-add/contracts-add.component';
import { ContractInspectionsAddComponent } from './components/contract-inspections-add/contract-inspections-add.component';
import { ContractInspectionsUpdateComponent } from './components/contract-inspections-update/contract-inspections-update.component';
import { ContractsUpdateComponent } from './components/contracts-update/contracts-update.component';
import { ContractsPageComponent } from './components/contracts-page/contracts-page.component';
import { ContractInspectionsPageComponent } from './components/contract-inspections-page/contract-inspections-page.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {DragulaModule} from 'ng2-dragula';


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
    SharedPnModule,
    NgSelectModule,
    RouterModule,
    FontAwesomeModule,
    DragulaModule
  ],
  declarations: [
    RentableItemsPnPageComponent,
    RentableItemsPnAddComponent,
    RentableItemsPnUpdateComponent,
    RentableItemsPnLayoutComponent,
    ContractsAddComponent,
    ContractInspectionsAddComponent,
    ContractInspectionsUpdateComponent,
    ContractsUpdateComponent,
    ContractsPageComponent,
    ContractInspectionsPageComponent,
    RentableItemsPnFieldsComponent,
    RentableItemsSettingsComponent
  ],
  providers: [
    RentableItemsPnService,
    RentableItemsPnSettingsService,
    ContractsService,
    ContractInspectionsService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class RentableItemsPnModule {
}
