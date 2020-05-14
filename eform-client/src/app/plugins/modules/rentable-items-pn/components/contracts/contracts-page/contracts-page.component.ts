import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {AuthService, LocaleService} from 'src/app/common/services/auth';

import {ContractsService} from '../../../services';
import {ContractModel, ContractsModel, ContractsRequestModel} from '../../../models';
import {PageSettingsModel} from 'src/app/common/models';
import {SharedPnService} from 'src/app/plugins/modules/shared/services';
import {Subject} from 'rxjs';
import {debounce, debounceTime} from 'rxjs/operators';
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
  @ViewChild('contractRentableItem') contractRentableItem;
  contractsRequestModel: ContractsRequestModel = new ContractsRequestModel();
  contractsModel: ContractsModel = new ContractsModel();
  localPageSettings: PageSettingsModel = new PageSettingsModel();
  searchSubject = new Subject();

  constructor(private contractsService: ContractsService,
              private translateService: TranslateService,
              private localeService: LocaleService,
              private sharedPnService: SharedPnService) {
    this.searchSubject.pipe(
      debounceTime(500)
    ).subscribe(val => {
      this.contractsRequestModel.name = val.toString();
      this.getAllContracts();
    });
  }

  ngOnInit() {
    this.getLocalPageSettings();
    this.setTranslation();
    //this.getAllContracts();
  }

  getLocalPageSettings() {
    // debugger;
    this.localPageSettings = this.sharedPnService.getLocalPageSettings
    ('rentableItemsPnContracts').settings;
    this.getAllContracts();
  }

  updateLocalPageSettings() {
    this.sharedPnService.updateLocalPageSettings
    ('rentableItemsPnContracts', this.localPageSettings);
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
  showContractRentableItem(contractId: number, contract: ContractModel) {
    this.contractRentableItem.show(contractId, contract);
  }
  getAllContracts() {
    this.contractsRequestModel.isSortDsc = this.localPageSettings.isSortDsc;
    this.contractsRequestModel.sortColumnName = this.localPageSettings.sort;
    this.contractsRequestModel.pageSize = this.localPageSettings.pageSize;
    this.contractsService.getAllContracts(this.contractsRequestModel).subscribe((data => {
      this.contractsModel = data.model;

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

  onSearchInputChanged(value: any) {
    this.searchSubject.next(value);
  }
}
