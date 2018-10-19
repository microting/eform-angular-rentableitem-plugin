import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from 'src/app/common/services/auth';


import {VehiclePnModel, VehiclesPnModel, VehiclesPnRequestModel} from '../../models';
import {VehiclesPnService} from '../../services';
declare var require: any;

@Component({
  selector: 'app-vehicles-pn-page',
  templateUrl: './vehicles-pn-page.component.html',
  styleUrls: ['./vehicles-pn-page.component.scss']
})
export class VehiclesPnPageComponent implements OnInit {
  @ViewChild('createVehicleModal') createVehicleModal;
  @ViewChild('editVehicleModal') editVehicleModal;

  vehiclesRequestModel: VehiclesPnRequestModel = new VehiclesPnRequestModel();
  vehiclesModel: VehiclesPnModel = new VehiclesPnModel();
  spinnerStatus = false;

  constructor(private vehiclesService: VehiclesPnService,
              private translateService: TranslateService,
              private localeService: LocaleService) {

  }

  ngOnInit() {
    this.setTranslation();
    this.getAllVehicles();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateVehicleModal() {
    this.createVehicleModal.show();
  }

  showEditVehicleModal(model: VehiclePnModel) {
    this.editVehicleModal.show(model);
  }

  getAllVehicles() {
    this.spinnerStatus = true;
    this.vehiclesService.getAllVehicles(this.vehiclesRequestModel).subscribe((data => {
      this.vehiclesModel = data.model;
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.vehiclesRequestModel.offset = e;
      if (e === 0) {
        this.vehiclesRequestModel.pageIndex = 0;
      } else {
        this.vehiclesRequestModel.pageIndex = Math.floor(e / this.vehiclesRequestModel.pageSize);
      }
      this.getAllVehicles();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.vehiclesRequestModel.sortColumnName = columnName;
    this.vehiclesRequestModel.isSortDsc = sortedByDsc;
    this.getAllVehicles();
  }

  importVehicle() {
    return true;
  }
}
