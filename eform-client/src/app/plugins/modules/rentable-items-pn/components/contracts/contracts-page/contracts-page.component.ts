import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from 'src/app/common/services/auth';

import {ContractsService} from '../../../services';
import {ContractModel, ContractsModel, ContractsRequestModel} from '../../../models';
declare var require: any;

@Component({
  selector: 'app-contracts-page',
  templateUrl: './contracts-page.component.html',
  styleUrls: ['./contracts-page.component.scss']
})
export class ContractsPageComponent implements OnInit {
  @ViewChild('createContractModal') createContractModal;
  @ViewChild('editContractModal') editContractModal;
  @ViewChild('deleteContractModal') deleteContractModal;
  @ViewChild('createInspectionModal') createInspectionModal;

  contractsRequestModel: ContractsRequestModel = new ContractsRequestModel();
  contractsModel: ContractsModel = new ContractsModel();
  spinnerStatus = false;

  constructor(private contractsService: ContractsService,
              private translateService: TranslateService,
              private localeService: LocaleService) { }

  ngOnInit() {
   this.setTranslation();
   this.getAllContracts();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateContractModal() {
    this.createContractModal.show();
  }

  showEditContractModal(model: ContractModel) {
    this.editContractModal.show(model);
  }
  showDeleteContractModal(model: ContractModel) {
    this.deleteContractModal.show(model);
  }
  showCreateInspectionModal(model: ContractModel) {
    this.createInspectionModal.show(model);
  }
  getAllContracts() {
    this.spinnerStatus = true;
    this.contractsService.getAllContracts(this.contractsRequestModel).subscribe((data => {
      this.contractsModel = data.model;
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.contractsRequestModel.offset = e;
      if (e === 0) {
        this.contractsRequestModel.pageIndex = 0;
      } else {
        this.contractsRequestModel.pageIndex = Math.floor(e / this.contractsRequestModel.pageSize);
      }
      this.getAllContracts();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.contractsRequestModel.sortColumnName = columnName;
    this.contractsRequestModel.isSortDsc = sortedByDsc;
    this.getAllContracts();
  }
}
