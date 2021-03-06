import {Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {AuthService, LocaleService} from 'src/app/common/services/auth';


import {
  RentableItemPnModel,
  RentableItemsPnModel,
  RentableItemsPnRequestModel,
  RentableItemsPnSettingsModel
} from '../../../models';
import {RentableItemsPnService, RentableItemsPnSettingsService} from '../../../services';
import {PageSettingsModel} from '../../../../../../common/models/settings';
import {SharedPnService} from '../../../../shared/services';
import {PluginClaimsHelper} from '../../../../../../common/helpers';
import {RentableItemsPnClaims} from '../../../enums';
import {Subject} from 'rxjs';
import {debounceTime} from 'rxjs/operators';
declare var require: any;

@Component({
  selector: 'app-rentable-items-pn-page',
  templateUrl: './rentable-items-pn-page.component.html',
  styleUrls: ['./rentable-items-pn-page.component.scss']
})
export class RentableItemsPnPageComponent implements OnInit {
  @ViewChild('createRentableItemModal') createRentableItemModal;
  @ViewChild('editRentableItemModal') editRentableItemModal;
  @ViewChild('deleteRentableItemModal') deleteRentableItemModal;
  localPageSettings: PageSettingsModel = new PageSettingsModel();

  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();

  settingsModel: RentableItemsPnSettingsModel = new RentableItemsPnSettingsModel();
  searchSubject = new Subject();

  get pluginClaimsHelper() {
    return PluginClaimsHelper;
  }

  get rentableItemsPnClaims() {
    return RentableItemsPnClaims;
  }

  constructor(private sharedPnService: SharedPnService,
              private rentableItemsService: RentableItemsPnService,
              private rentableItemsSettingsService: RentableItemsPnSettingsService,
              private translateService: TranslateService,
              private localeService: LocaleService,
              private authService: AuthService) {
    this.searchSubject.pipe(
      debounceTime(500)
    ).subscribe(val => {
      this.rentableItemsRequestModel.nameFilter = val.toString();
      this.getAllRentableItems();
    });
  }

  ngOnInit() {
    this.setTranslation();
    this.getAllInitialData();
  }

  setTranslation() {
    const lang = this.localeService.getCurrentUserLocale();
    const i18n = require(`../../../i18n/${lang}.json`);
    this.translateService.setTranslation(lang, i18n, true);
  }

  showCreateRentableItemModal() {
    this.createRentableItemModal.show();
  }

  showEditRentableItemModal(model: RentableItemPnModel) {
    this.editRentableItemModal.show(model);
  }

  showDeleteRentableItemModal(model: RentableItemPnModel) {
    this.deleteRentableItemModal.show(model);
  }
  get currentRole(): string {
    return this.authService.currentRole;
  }
  getLocalPageSettings() {
    this.localPageSettings = this.sharedPnService.getLocalPageSettings
    ('rentableItemsPnSettings', 'RentableItems').settings;
    this.getAllInitialData();
  }
  updateLocalPageSettings() {
    this.sharedPnService.updateLocalPageSettings
    ('rentableItemsPnSettings', this.localPageSettings, 'RentableItems');
    this.getLocalPageSettings();
  }
  getAllInitialData() {
    this.rentableItemsSettingsService.getAllSettings().subscribe((data) => {
      if (data && data.success) {
        this.settingsModel = data.model;
        this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((result => {
          if (result && result.success) {
            this.rentableItemsModel = result.model;
          }

        }));
      }
    });
  }
  //
  // getAllSettings() {
  //   this.spinnerStatus = true;
  //   this.rentableItemsSettingsService.getAllSettings().subscribe((data) => {
  //     if (data && data.success) {
  //       this.settingsModel = data.model;
  //     }
  //   });
  //
  // }

  getEmail() {

    this.rentableItemsService.getEmail().subscribe();
  }

  getAllRentableItems() {
    this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((data => {
      this.rentableItemsModel = data.model;

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
    this.rentableItemsRequestModel.sort = columnName;
    this.rentableItemsRequestModel.isSortDsc = sortedByDsc;
    this.getAllRentableItems();
  }

  onSearchInputChanged(value: any) {
    this.searchSubject.next(value);
  }
}
