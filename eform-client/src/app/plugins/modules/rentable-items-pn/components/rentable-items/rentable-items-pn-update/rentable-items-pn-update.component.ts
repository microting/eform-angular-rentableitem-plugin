import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {RentableItemPnModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {RentableItemsPnService} from 'src/app/plugins/modules/rentable-items-pn/services';
import {formatTimezone} from 'src/app/common/helpers';

@Component({
  selector: 'app-rentable-items-pn-update',
  templateUrl: './rentable-items-pn-update.component.html',
  styleUrls: ['./rentable-items-pn-update.component.scss']
})
export class RentableItemsPnUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onRentableItemUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedRentableItemModel: RentableItemPnModel = new RentableItemPnModel();
  spinnerStatus = false;

  constructor(private rentableItemsService: RentableItemsPnService) { }

  ngOnInit() {
  }

  show(rentableItemModel: RentableItemPnModel) {
    this.selectedRentableItemModel = rentableItemModel;
    this.frame.show();
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
}
