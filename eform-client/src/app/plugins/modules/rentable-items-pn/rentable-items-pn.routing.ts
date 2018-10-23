import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AdminGuard, AuthGuard} from 'src/app/common/guards';
import {RentableItemsPnLayoutComponent} from './layouts';
import {RentableItemsPnPageComponent} from './components';

export const routes: Routes = [
  {
    path: '',
    component: RentableItemsPnLayoutComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: RentableItemsPnPageComponent
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
