import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {RentableItemPnModel} from '../../../models';
import {RentableItemsPnService} from '../../../services';

@Component({
  selector: 'app-rentable-items-pn-delete',
  templateUrl: './rentable-items-pn-delete.component.html',
  styleUrls: ['./rentable-items-pn-delete.component.scss']
})
export class RentableItemsPnDeleteComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onRentableItemDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedRentableItemModel: RentableItemPnModel = new RentableItemPnModel();

  constructor(private rentableItemsService: RentableItemsPnService) { }

  ngOnInit() {
  }

  show(rentableItemModel: RentableItemPnModel) {
    this.selectedRentableItemModel = rentableItemModel;
    this.frame.show();
  }
  deleteRentableItem() {
    this.rentableItemsService.deleteRentableItem(this.selectedRentableItemModel.id).subscribe((data) => {
      if (data && data.success) {
        this.onRentableItemDeleted.emit();
        this.frame.hide();
      }
    });
  }
}
