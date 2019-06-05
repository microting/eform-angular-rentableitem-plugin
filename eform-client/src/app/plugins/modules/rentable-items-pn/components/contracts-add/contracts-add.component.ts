import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractModel, RentableItemPnModel, RentableItemsPnModel, RentableItemsPnRequestModel} from '../../models';
import {ContractsService, RentableItemsPnService} from '../../services';
import {formatTimezone} from '../../../../../common/helpers';

@Component({
  selector: 'app-contracts-add',
  templateUrl: './contracts-add.component.html',
  styleUrls: ['./contracts-add.component.scss']
})
export class ContractsAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onContractCreated: EventEmitter<void> = new EventEmitter<void>();
  newContractModel: ContractModel = new ContractModel();
  spinnerStatus = false;
  frameShow = true;
  rentableItems: Array<RentableItemPnModel> = [];
  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  constructor(private rentableItemsService: RentableItemsPnService,
              private contractService: ContractsService) { }

  ngOnInit() {
    this.getRentableItems();

  }

  show() {
    this.newContractModel = new ContractModel();
    this.frame.show();
  }

  createContract() {
    this.spinnerStatus = true;
    this.contractService.createContract(this.newContractModel).subscribe(((data) => {
      if (data && data.success) {
        this.newContractModel = new ContractModel();
        this.onContractCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
  addNewRentableItem(e: any) {
    debugger;
    // let item = new RentableItemPnModel();
    // // for (let rentableItem of this.rentableItemsModel.rentableItems) {
    // //   item = rentableItem;
    // // }
    // this.rentableItemsModel.rentableItems.forEach(function (rentableItem) {
    //   item = rentableItem;
    // });
    if (!this.newContractModel.rentableItems.includes(e)) {
      this.newContractModel.rentableItems.push(e);
    }
  }
  removeRentableItem(rentableItem: any) {
    const index = this.newContractModel.rentableItems.indexOf(rentableItem);
    this.newContractModel.rentableItems.splice(index,  1);
  }
  getRentableItems() {
    this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((result => {
      if (result && result.success) {
        this.rentableItemsModel = result.model;
      }
      this.spinnerStatus = false;
    }));
  }
  onStartDateSelected(e: any) {
    this.newContractModel.contractStart = formatTimezone(e.value._d);
  }
  onEndDateSelected(f: any) {
    this.newContractModel.contractEnd = formatTimezone(f.value._d);
  }
}
