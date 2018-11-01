import {Component, OnInit, ViewChild} from '@angular/core';
import {InspectionsService} from '../../services';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from '../../../../../common/services/auth';
import {InspectionModel, InspectionsModel, InspectionsRequestModel} from '../../models';

declare  var require: any;

@Component({
  selector: 'app-inspections-page',
  templateUrl: './inspections-page.component.html',
  styleUrls: ['./inspections-page.component.scss']
})
export class InspectionsPageComponent implements OnInit {
  @ViewChild('createInspectionModal') createInspectionModal;
  @ViewChild('editInspectionModal') editInspectionModal;

  inspectionsRequestModel: InspectionsRequestModel = new InspectionsRequestModel();
  inspectionsModel: InspectionsModel = new InspectionsModel();
  spinnerStatus = false;

  constructor(private inspectionsService: InspectionsService,
              private translateService: TranslateService,
              private localeService: LocaleService) { }

  ngOnInit() {
    this.setTranslation();
    this.getAllInspections();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateInspectionModal() {
    this.createInspectionModal.show();
  }

  showEditInspectionModal(model: InspectionModel) {
    this.editInspectionModal.show(model);
  }
  getAllInspections() {
    this.spinnerStatus = true;
    this.inspectionsService.getAllInspections(this.inspectionsRequestModel).subscribe(( data => {
      this.inspectionsModel = data.model;
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.inspectionsRequestModel.offset = e;
      if (e === 0) {
        this.inspectionsRequestModel.pageIndex = 0;
      } else {
        this.inspectionsRequestModel.pageSize = Math.floor( e / this.inspectionsRequestModel.pageSize);
      }
      this.getAllInspections();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.inspectionsRequestModel.sortColumnName = columnName;
    this.inspectionsRequestModel.isSortDsc = sortedByDsc;
    this.getAllInspections();
  }
}
