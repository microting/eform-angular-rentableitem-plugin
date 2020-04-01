import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {RentableItemPnModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {RentableItemsPnService} from 'src/app/plugins/modules/rentable-items-pn/services';
import {formatTimezone} from 'src/app/common/helpers';
import {TemplateListModel, TemplateRequestModel} from '../../../../../../common/models/eforms';
import {EFormService} from '../../../../../../common/services/eform';

@Component({
  selector: 'app-rentable-items-pn-update',
  templateUrl: './rentable-items-pn-update.component.html',
  styleUrls: ['./rentable-items-pn-update.component.scss']
})
export class RentableItemsPnUpdateComponent implements OnInit {
  @ViewChild('frame', {static: false}) frame;
  @Output() onRentableItemUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedRentableItemModel: RentableItemPnModel = new RentableItemPnModel();
  spinnerStatus = false;
  templateRequestModel: TemplateRequestModel = new TemplateRequestModel();
  templatesModel: TemplateListModel = new TemplateListModel();
  constructor(private eFormService: EFormService,
              private rentableItemsService: RentableItemsPnService) { }

  ngOnInit() {
  }

  show(rentableItemModel: RentableItemPnModel) {
    this.selectedRentableItemModel = rentableItemModel;
    this.getAlleForms();
    this.frame.show();
  }

  getAlleForms() {
    this.spinnerStatus = true;
    this.eFormService.getAll(this.templateRequestModel).subscribe(data => {
      if (data && data.success) {
        this.templatesModel = data.model;
      }
      this.spinnerStatus = false;
    });
  }
  updateRentableItem() {
    this.spinnerStatus = true;
    this.rentableItemsService.updateRentableItem(this.selectedRentableItemModel).subscribe(((data) => {
      if (data && data.success) {
        this.onRentableItemUpdated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }

  onRegistrationDateSelected(e: any) {
    this.selectedRentableItemModel.registrationDate = formatTimezone(e.value._d);
  }

  onSelectedChanged(e: any) {
    this.selectedRentableItemModel.eFormId = e.id;
  }
}
