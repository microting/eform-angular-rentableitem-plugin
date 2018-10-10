import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard} from 'src/app/common/guards';
import {VehiclesPnLayoutComponent} from './layouts';
import {VehiclesPnPageComponent} from './components';

export const routes: Routes = [
  {
    path: '',
    component: VehiclesPnLayoutComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: VehiclesPnPageComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehiclesPnRouting {
}
