import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard} from 'src/app/common/guards';
import {RentableItemsPnLayoutComponent} from './layouts';
import {RentableItemsPnFieldsComponent, RentableItemsPnPageComponent, RentableItemsSettingsComponent} from './components';
import {ContractsPageComponent} from './components/contracts-page/contracts-page.component';
import {ContractInspectionsPageComponent} from './components/contract-inspections-page/contract-inspections-page.component';

export const routes: Routes = [
  {
    path: '',
    component: RentableItemsPnLayoutComponent,
    children: [
      {
        path: 'rentable-items',
        canActivate: [AuthGuard],
        component: RentableItemsPnPageComponent
      },
      {
        path: 'contracts',
        canActivate: [AuthGuard],
        component: ContractsPageComponent
      },
      {
        path: 'inspections',
        canActivate: [AuthGuard],
        component: ContractInspectionsPageComponent
      },
      {
        path: 'settings',
        canActivate: [AdminGuard],
        component: RentableItemsSettingsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RentableItemsPnRouting {
}
