import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard, PermissionGuard} from 'src/app/common/guards';
import {RentableItemsPnLayoutComponent} from './layouts';
import {ImporterComponent, RentableItemsPnPageComponent, RentableItemsSettingsComponent} from './components';
import {ContractsPageComponent} from './components/contracts';
import {ContractInspectionsPageComponent} from './components/contract-inspections';
import {RentableItemsPnClaims} from './enums';

export const routes: Routes = [
  {
    path: '',
    component: RentableItemsPnLayoutComponent,
    canActivate: [PermissionGuard],
    data: {requiredPermission: RentableItemsPnClaims.accessRentableItemsPlugin},
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
      },
      {
        path: 'import',
        canActivate: [AuthGuard],
        component: ImporterComponent
      },
      {
        path: 'case',
        loadChildren: () => import('./components/contract-inspection-case/contract-inspection-case.module')
          .then(m => m.ContractInspectionCaseModule)
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RentableItemsPnRouting {
}
