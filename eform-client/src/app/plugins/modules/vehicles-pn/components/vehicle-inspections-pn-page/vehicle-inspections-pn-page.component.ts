import {Component, OnInit, ViewChild} from '@angular/core';
import {VehicleInspectionPnModel, VehicleInspectionsPnModel, VehiclePnModel, VehiclesPnModel, VehiclesPnRequestModel} from '../../models';
import {VehiclesPnService} from '../../services';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from '../../../../../common/services/auth';
declare var require: any;

@Component({
  selector: 'app-vehicle-inspections-pn-page',
  templateUrl: './vehicle-inspections-pn-page.component.html',
  styleUrls: ['./vehicle-inspections-pn-page.component.scss']
})
export class VehicleInspectionsPnPageComponent implements OnInit {

  @ViewChild('createVehicleInspectionModal') createVehicleInspectionModal;
  @ViewChild('editVehicleInspectionModal') editVehicleInspectionModal;

  vehiclesRequestModel: VehiclesPnRequestModel = new VehiclesPnRequestModel();
  vehicleInspectionsModel: VehicleInspectionsPnModel = new VehicleInspectionsPnModel();
  spinnerStatus = false;

  constructor(private vehiclesService: VehiclesPnService,
              private translateService: TranslateService,
              private localeService: LocaleService) {

  }

  ngOnInit() {
    this.setTranslation();
    this.getAllVehicleInspections();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateVehicleInspectionModal() {
    this.createVehicleInspectionModal.show();
  }

  showEditVehicleInspectionModal(model: VehicleInspectionPnModel) {
    this.editVehicleInspectionModal.show(model);
  }

  getAllVehicleInspections() {
    this.spinnerStatus = true;
    this.vehiclesService.getAllVehicleInspections(this.vehiclesRequestModel).subscribe((data => {
      this.vehicleInspectionsModel = data.model;
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
      this.getAllVehicleInspections();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.vehiclesRequestModel.sortColumnName = columnName;
    this.vehiclesRequestModel.isSortDsc = sortedByDsc;
    this.getAllVehicleInspections();
  }

}
