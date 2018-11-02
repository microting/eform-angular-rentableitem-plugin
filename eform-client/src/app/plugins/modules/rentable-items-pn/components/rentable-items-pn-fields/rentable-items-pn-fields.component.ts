import {ChangeDetectorRef, Component, EventEmitter, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {debounceTime, switchMap} from 'rxjs/operators';
import {AdvEntitySearchableGroupListModel, AdvEntitySearchableGroupListRequestModel} from 'src/app/common/models/advanced';
import {EntitySearchService} from 'src/app/common/services/advanced';
import {RentableItemsPnFieldStatus} from '../../enums';
import {RentableItemsPnSettingsModel, RentableItemsFieldPnUpdateModel, RentableItemsFieldsPnUpdateModel} from '../../models';
import {RentableItemsPnFieldsService, RentableItemsPnSettingsService} from '../../services';

@Component({
  selector: 'app-rentable-items-pn-fields',
  templateUrl: './rentable-items-pn-fields.component.html',
  styleUrls: ['./rentable-items-pn-fields.component.scss']
})
export class RentableItemsPnFieldsComponent implements OnInit {
  isChecked = false;
  spinnerStatus = false;
  rentableItemsFieldsUpdateModel: RentableItemsFieldsPnUpdateModel = new RentableItemsFieldsPnUpdateModel();
  rentableItemsPnSettingsModel: RentableItemsPnSettingsModel = new RentableItemsPnSettingsModel();
  advEntitySearchableGroupListModel: AdvEntitySearchableGroupListModel = new AdvEntitySearchableGroupListModel();
  advEntitySearchableGroupListRequestModel: AdvEntitySearchableGroupListRequestModel
    = new AdvEntitySearchableGroupListRequestModel();
  typeahead = new EventEmitter<string>();
  get fieldStatusEnum() { return RentableItemsPnFieldStatus; }

  constructor(
    private rentableItemsFieldsService: RentableItemsPnFieldsService,
    private entitySearchService: EntitySearchService,
    private cd: ChangeDetectorRef,
    private rentableItemsSettingsService: RentableItemsPnSettingsService,
    private router: Router) {
    this.typeahead
      .pipe(
        debounceTime(200),
        switchMap(term => {
          this.advEntitySearchableGroupListRequestModel.nameFilter = term;
          return this.entitySearchService.getEntitySearchableGroupList(this.advEntitySearchableGroupListRequestModel);
        })
      )
      .subscribe(items => {
        this.advEntitySearchableGroupListModel = items.model;
        this.cd.markForCheck();
      });
  }

  ngOnInit() {
    this.advEntitySearchableGroupListRequestModel.pageSize = 15;
    this.getAllFields();
    this.getSettings();
  }

  getAllFields() {
    this.spinnerStatus = true;
    this.rentableItemsFieldsService.getAllFields().subscribe((data) => {
      if (data && data.success) {
        this.rentableItemsFieldsUpdateModel = data.model;
      } this.spinnerStatus = false;
    });
  }
  
  getSettings() {
    this.rentableItemsSettingsService.getAllSettings().subscribe((data => {
      if (data && data.success) {
        this.rentableItemsPnSettingsModel = data.model;
      }
    }));
  }

  updateSettings() {
    this.spinnerStatus = true;
    this.rentableItemsSettingsService.updateSettings(this.rentableItemsPnSettingsModel).subscribe((data) => {
      if (data && data.success) {
        this.spinnerStatus = true;
        this.rentableItemsFieldsService.updateFields(this.rentableItemsFieldsUpdateModel).subscribe((data) => {
          if (data && data.success) {
            this.router.navigate(['/plugins/customers-pn']).then();
          } this.spinnerStatus = false;
        });
      } this.spinnerStatus = false;
    });
  }

  checkBoxChanged(e: any, field: RentableItemsFieldPnUpdateModel) {
    if (e.target && e.target.checked) {
      this.rentableItemsFieldsUpdateModel.fields.find(x => x.id === field.id).fieldStatus = RentableItemsPnFieldStatus.Enabled;
    } else if (e.target && !e.target.checked) {
      this.rentableItemsFieldsUpdateModel.fields.find(x => x.id === field.id).fieldStatus = RentableItemsPnFieldStatus.Disabled;
    }
  }

  onSelectedChanged(e: any) {
    this.rentableItemsPnSettingsModel.relatedEntityId = e.microtingUUID;
  }
}
