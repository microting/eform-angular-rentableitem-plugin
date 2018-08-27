import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AuthGuard} from 'src/app/common/guards';
import {VehiclesPnPageComponent} from './components';

export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: VehiclesPnPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehiclesPnRouting {
}
