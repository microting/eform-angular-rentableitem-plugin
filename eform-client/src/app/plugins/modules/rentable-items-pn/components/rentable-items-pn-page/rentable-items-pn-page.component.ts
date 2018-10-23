import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from 'src/app/common/services/auth';


import {RentableItemPnModel, RentableItemsPnModel, RentableItemsPnRequestModel} from '../../models';
import {RentableItemsPnService} from '../../services';
declare var require: any;

@Component({
  selector: 'app-rentable-items-pn-page',
  templateUrl: './rentable-items-pn-page.component.html',
  styleUrls: ['./rentable-items-pn-page.component.scss']
})
export class RentableItemsPnPageComponent implements OnInit {
  @ViewChild('createRentableItemModal') createRentableItemModal;
  @ViewChild('editRentableItemModal') editRentableItemModal;

  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  spinnerStatus = false;

  constructor(private rentableItemsService: RentableItemsPnService,
              private translateService: TranslateService,
              private localeService: LocaleService) {

  }

  ngOnInit() {
    this.setTranslation();
    this.getAllRentableItems();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateRentableItemModal() {
    this.createRentableItemModal.show();
  }

  showEditRentableItemModal(model: RentableItemPnModel) {
    this.editRentableItemModal.show(model);
  }

  getAllRentableItems() {
    this.spinnerStatus = true;
    this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((data => {
      this.rentableItemsModel = data.model;
      this.spinnerStatus = false;
    }));
  }

  changePage(e: any) {
    if (e || e === 0) {
      this.rentableItemsRequestModel.offset = e;
      if (e === 0) {
        this.rentableItemsRequestModel.pageIndex = 0;
      } else {
        this.rentableItemsRequestModel.pageIndex = Math.floor(e / this.rentableItemsRequestModel.pageSize);
      }
      this.getAllRentableItems();
    }
  }

  sortByColumn(columnName: string, sortedByDsc: boolean) {
    this.rentableItemsRequestModel.sortColumnName = columnName;
    this.rentableItemsRequestModel.isSortDsc = sortedByDsc;
    this.getAllRentableItems();
  }

}
