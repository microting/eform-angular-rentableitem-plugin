import {Component, OnInit, ViewChild} from '@angular/core';
import {ContractInspectionsService, ContractsService} from '../../../services';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from '../../../../../../common/services/auth';
import {
  ContractInspectionModel,
  ContractInspectionsModel,
  ContractInspectionsRequestModel,
  ContractsModel,
  ContractsRequestModel
} from '../../../models';


declare  var require: any;

@Component({
  selector: 'app-inspections-page',
  templateUrl: './contract-inspections-page.component.html',
  styleUrls: ['./contract-inspections-page.component.scss']
})
export class ContractInspectionsPageComponent implements OnInit {
  @ViewChild('createInspectionModal', {static: false}) createInspectionModal;
  @ViewChild('editInspectionModal', {static: false}) editInspectionModal;
  @ViewChild('deleteInspectionModal', {static: false}) deleteInspectionModal;

  contractInspectionsRequestModel: ContractInspectionsRequestModel = new ContractInspectionsRequestModel();
  contractInspectionsModel: ContractInspectionsModel = new ContractInspectionsModel();
  contractsRequestModel: ContractsRequestModel = new ContractsRequestModel();
  contractsModel: ContractsModel = new ContractsModel();
  spinnerStatus = false;

  constructor(private contractService: ContractsService,
              private contractInspectionsService: ContractInspectionsService,
              private translateService: TranslateService,
              private localeService: LocaleService) { }

  ngOnInit() {
    this.setTranslation();
    this.getAllInspections();
    // this.getAllContracts();
  }
  getAllContracts() {
    this.spinnerStatus = true;
    this.contractService.getAllContracts(this.contractsRequestModel).subscribe((data => {
      this.contractsModel = data.model;
      this.spinnerStatus = false;
    }));
  }
  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showEditInspectionModal(model: ContractInspectionModel) {
    this.editInspectionModal.show(model);
  }
  showDeleteInspectionModal(model: ContractInspectionModel) {
    this.deleteInspectionModal.show(model);
  }
  getAllInspections() {
    // debugger;
    this.spinnerStatus = true;
    this.contractInspectionsService.getAllInspections(this.contractInspectionsRequestModel).subscribe((data => {
      debugger;
      this.contractInspectionsModel = data.model;
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.contractInspectionsRequestModel.offset = e;
      if (e === 0) {
        this.contractInspectionsRequestModel.pageIndex = 0;
      } else {
        this.contractInspectionsRequestModel.pageSize = Math.floor( e / this.contractInspectionsRequestModel.pageSize);
      }
      this.getAllInspections();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.contractInspectionsRequestModel.sortColumnName = columnName;
    this.contractInspectionsRequestModel.isSortDsc = sortedByDsc;
    this.getAllInspections();
  }
}
