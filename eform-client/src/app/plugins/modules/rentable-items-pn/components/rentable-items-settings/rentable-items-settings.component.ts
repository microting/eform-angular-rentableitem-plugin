import {ChangeDetectorRef, Component, EventEmitter, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {debounceTime, switchMap} from 'rxjs/operators';
import {TemplateListModel, TemplateRequestModel} from 'src/app/common/models/eforms';
import {EFormService} from 'src/app/common/services/eform';
import {RentableItemsPnSettingsModel} from '../../models';
import {RentableItemsPnSettingsService} from '../../services';

@Component({
  selector: 'app-rentable-items-settings',
  templateUrl: './rentable-items-settings.component.html',
  styleUrls: ['./rentable-items-settings.component.scss']
})
export class RentableItemsSettingsComponent implements OnInit {
  spinnerStatus = false;
  typeahead = new EventEmitter<string>();
  settingsModel: RentableItemsPnSettingsModel = new RentableItemsPnSettingsModel();
  templateRequestModel: TemplateRequestModel = new TemplateRequestModel();
  templatesModel: TemplateListModel = new TemplateListModel();
  constructor(private activateRoute: ActivatedRoute,
              private eFormService: EFormService,
              private rentableItemsSettingsService: RentableItemsPnSettingsService,
              private cd: ChangeDetectorRef) {
    this.typeahead
      .pipe(
        debounceTime(200),
        switchMap(term => {
          this.templateRequestModel.nameFilter = term;
          return this.eFormService.getAll(this.templateRequestModel);
        })
      )
      .subscribe(items => {
        this.templatesModel = items.model;
        this.cd.markForCheck();
      });
  }

  ngOnInit() {
    this.getSettings();
  }

  getSettings() {
    this.spinnerStatus = true;
    this.rentableItemsSettingsService.getAllSettings().subscribe((data) => {
      if (data && data.success) {
        this.settingsModel = data.model;
      } this.spinnerStatus = false;
    });
  }

  updateSettings() {
    this.spinnerStatus = true;
    this.rentableItemsSettingsService.updateSettings(this.settingsModel)
      .subscribe((data) => {
      if (data && data.success) {

      } this.spinnerStatus = false;
    });
  }

  onSelectedChanged(e: any) {
    this.settingsModel.eFormId = e.id;
  }
}
