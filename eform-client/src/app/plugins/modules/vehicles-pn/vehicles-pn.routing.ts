import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AuthGuard} from 'src/app/common/guards';
import {VehiclesPnPageComponent} from './components';
import {VehicleInspectionsPnPageComponent} from './components/vehicle-inspections-pn-page/vehicle-inspections-pn-page.component';
import {VehiclesPnImportComponent} from './components/vehicles-pn-import/vehicles-pn-import.component';

export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: VehiclesPnPageComponent
  },
  {
    path: 'inspections',
    canActivate: [AuthGuard],
    component: VehicleInspectionsPnPageComponent
  },
  {
    path: 'import',
    canActivate: [AuthGuard],
    component: VehiclesPnImportComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehiclesPnRouting {
}
