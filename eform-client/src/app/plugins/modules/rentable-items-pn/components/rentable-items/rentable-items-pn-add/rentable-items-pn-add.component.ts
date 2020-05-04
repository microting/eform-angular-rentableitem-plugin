import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {RentableItemPnModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {RentableItemsPnService} from 'src/app/plugins/modules/rentable-items-pn/services';
import {formatTimezone} from 'src/app/common/helpers';
import {TemplateListModel, TemplateRequestModel} from '../../../../../../common/models/eforms';
import {EFormService} from '../../../../../../common/services/eform';

@Component({
  selector: 'app-rentable-items-pn-add',
  templateUrl: './rentable-items-pn-add.component.html',
  styleUrls: ['./rentable-items-pn-add.component.scss']
})
export class RentableItemsPnAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onRentableItemCreated: EventEmitter<void> = new EventEmitter<void>();
  newRentableItemModel: RentableItemPnModel = new RentableItemPnModel();
  frameShow = true;
  templateRequestModel: TemplateRequestModel = new TemplateRequestModel();
  templatesModel: TemplateListModel = new TemplateListModel();
  constructor(private eFormService: EFormService,
              private rentableItemsService: RentableItemsPnService) { }

  ngOnInit() {
  }

  show() {
    this.newRentableItemModel = new RentableItemPnModel();
    this.getAlleForms();
    this.frame.show();
  }

  getAlleForms() {
    this.eFormService.getAll(this.templateRequestModel).subscribe(data => {
      if (data && data.success) {
        this.templatesModel = data.model;
      }

    });
  }

  createRentableItem() {
    this.rentableItemsService.createRentableItem(this.newRentableItemModel).subscribe(((data) => {
      if (data && data.success) {
        this.newRentableItemModel = new RentableItemPnModel();
        this.onRentableItemCreated.emit();
        this.frame.hide();
      }
    }));
  }

  onRegistrationDateSelected(e: any) {
    this.newRentableItemModel.registrationDate = formatTimezone(e.value._d);
  }
  onSelectedChanged(e: any) {
    this.newRentableItemModel.eFormId = e.id;
  }
}
