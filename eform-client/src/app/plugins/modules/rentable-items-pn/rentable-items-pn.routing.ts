import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard} from 'src/app/common/guards';
import {RentableItemsPnLayoutComponent} from './layouts';
import {RentableItemsPnPageComponent} from './components';
import {ContractsPageComponent} from './components/contracts-page/contracts-page.component';
import {InspectionsPageComponent} from './components/inspections-page/inspections-page.component';

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
        component: InspectionsPageComponent
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
