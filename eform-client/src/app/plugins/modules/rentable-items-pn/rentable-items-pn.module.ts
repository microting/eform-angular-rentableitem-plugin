import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {OwlMomentDateTimeModule} from 'ng-pick-datetime-ex';
import {MDBRootModule} from 'angular-bootstrap-md';
import {MY_MOMENT_FORMATS} from 'src/app/common/helpers';
import {RentableItemsPnLayoutComponent} from './layouts';
import {SharedPnModule} from '../shared/shared-pn.module';
import {NgSelectModule} from '@ng-select/ng-select';
import {RouterModule} from '@angular/router';
import {
  ContractsService,
  ContractInspectionsService,
  RentableItemsPnService,
  RentableItemsPnSettingsService,
  ContractRentableItemService
} from './services';
import {
  RentableItemsPnPageComponent,
  RentableItemsPnAddComponent,
  RentableItemsPnUpdateComponent,
  RentableItemsSettingsComponent,
  RentableItemsPnDeleteComponent,
  ContractInspectionsAddComponent,
  ContractInspectionsUpdateComponent,
  ContractInspectionsPageComponent,
  ContractInspectionsDeleteComponent,
  ContractsAddComponent,
  ContractsUpdateComponent,
  ContractsPageComponent,
  ContractsDeleteComponent,
  ContractRentableItemComponent,
  ImporterComponent
} from './components';
import {RentableItemsPnRouting} from './rentable-items-pn.routing';
import {OWL_DATE_TIME_FORMATS, OwlDateTimeModule, OwlNativeDateTimeModule} from 'ng-pick-datetime-ex';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {DragulaModule} from 'ng2-dragula';
import {EformSharedModule} from '../../../common/modules/eform-shared/eform-shared.module';


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
        DragulaModule,
        EformSharedModule
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
    RentableItemsSettingsComponent,
    RentableItemsPnDeleteComponent,
    ContractsDeleteComponent,
    ContractInspectionsDeleteComponent,
    ContractRentableItemComponent,
    ImporterComponent
  ],
  providers: [
    RentableItemsPnService,
    RentableItemsPnSettingsService,
    ContractsService,
    ContractInspectionsService,
    ContractRentableItemService,
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS}
  ]
})
export class RentableItemsPnModule {
}
