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
import {Subject} from 'rxjs';
import {PageSettingsModel} from 'src/app/common/models';
import {SharedPnService} from 'src/app/plugins/modules/shared/services';
import {debounceTime} from 'rxjs/operators';


declare  var require: any;

@Component({
  selector: 'app-inspections-page',
  templateUrl: './contract-inspections-page.component.html',
  styleUrls: ['./contract-inspections-page.component.scss']
})
export class ContractInspectionsPageComponent implements OnInit {
  @ViewChild('createInspectionModal') createInspectionModal;
  @ViewChild('editInspectionModal') editInspectionModal;
  @ViewChild('deleteInspectionModal') deleteInspectionModal;

  contractInspectionsRequestModel: ContractInspectionsRequestModel = new ContractInspectionsRequestModel();
  contractInspectionsModel: ContractInspectionsModel = new ContractInspectionsModel();
  contractsRequestModel: ContractsRequestModel = new ContractsRequestModel();
  contractsModel: ContractsModel = new ContractsModel();
  localPageSettings: PageSettingsModel = new PageSettingsModel();
  searchSubject = new Subject();

  constructor(private contractService: ContractsService,
              private contractInspectionsService: ContractInspectionsService,
              private translateService: TranslateService,
              private localeService: LocaleService,
              private sharedPnService: SharedPnService) {
    this.searchSubject.pipe(
      debounceTime(500)
    ).subscribe(val => {
      this.contractInspectionsRequestModel.name = val.toString();
      this.getAllInspections();
    });
  }

  ngOnInit() {
    this.setTranslation();
    this.getLocalPageSettings();
    // this.getAllContracts();
  }

  getLocalPageSettings() {
    // debugger;
    this.localPageSettings = this.sharedPnService.getLocalPageSettings
    ('rentableItemsPnContractInspections').settings;
    this.getAllInspections();
  }

  updateLocalPageSettings() {
    this.sharedPnService.updateLocalPageSettings
    ('rentableItemsPnContractInspections', this.localPageSettings);
    this.getAllInspections();
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
    this.contractInspectionsRequestModel.isSortDsc = this.localPageSettings.isSortDsc;
    this.contractInspectionsRequestModel.sortColumnName = this.localPageSettings.sort;
    this.contractInspectionsRequestModel.pageSize = this.localPageSettings.pageSize;
    this.contractInspectionsService.getAllInspections(this.contractInspectionsRequestModel).subscribe((data => {
      this.contractInspectionsModel = data.model;

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

  downloadPDF(inspection: any) {
    window.open('/api/rentable-items-pn/inspection/report' +
      inspection.id + '?token=none&fileType=pdf', '_blank');
  }

  downloadDocx(inspection: any) {
    window.open('/api/rentable-items-pn/inspections/report' +
      inspection.id + '?token=none&fileType=docx', '_blank');
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.contractInspectionsRequestModel.sortColumnName = columnName;
    this.contractInspectionsRequestModel.isSortDsc = sortedByDsc;
    this.getAllInspections();
  }

  onSearchInputChanged(value: any) {
    this.searchSubject.next(value);
  }
}
