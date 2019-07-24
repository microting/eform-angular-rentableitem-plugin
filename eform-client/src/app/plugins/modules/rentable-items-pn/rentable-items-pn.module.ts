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
  RentableItemsSettingsComponent,
  RentableItemsPnDeleteComponent,
  ContractInspectionsAddComponent,
  ContractInspectionsUpdateComponent,
  ContractInspectionsPageComponent,
  ContractInspectionsDeleteComponent,
  ContractsAddComponent,
  ContractsUpdateComponent,
  ContractsPageComponent,
  ContractsDeleteComponent
} from './components';
import {RentableItemsPnRouting} from './rentable-items-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {DragulaModule} from 'ng2-dragula';
import {CustomersPnService} from '../customers-pn/services';


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
    RentableItemsSettingsComponent,
    RentableItemsPnDeleteComponent,
    ContractsDeleteComponent,
    ContractInspectionsDeleteComponent
  ],
  providers: [
    RentableItemsPnService,
    RentableItemsPnSettingsService,
    ContractsService,
    ContractInspectionsService,
    CustomersPnService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class RentableItemsPnModule {
}
