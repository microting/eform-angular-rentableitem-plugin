import {AfterViewInit, Component, OnInit} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LocaleService} from 'src/app/common/services/auth';
import {CustomersPnLocalSettings} from 'src/app/plugins/modules/customers-pn/enums';
import {SharedPnService} from 'src/app/plugins/modules/shared/services';
declare var require: any;

@Component({
  selector: 'app-rentable-items-pn-layout',
  template: `<router-outlet></router-outlet>`
})
export class RentableItemsPnLayoutComponent implements AfterViewInit, OnInit {
  constructor(private localeService: LocaleService,
              private translateService: TranslateService,
              private sharedPnService: SharedPnService) {
  }

  ngOnInit() {
    this.sharedPnService.initLocalPageSettings('rentableItemsPnContracts', CustomersPnLocalSettings);
    this.sharedPnService.initLocalPageSettings('rentableItemsPnContractInspections', CustomersPnLocalSettings);
  }

  ngAfterViewInit() {
    setTimeout(() => {
      const lang = this.localeService.getCurrentUserLocale();
      const i18n = require(`../i18n/${lang}.json`);
      this.translateService.setTranslation(lang, i18n, true);
    }, 1000);
  }
}
