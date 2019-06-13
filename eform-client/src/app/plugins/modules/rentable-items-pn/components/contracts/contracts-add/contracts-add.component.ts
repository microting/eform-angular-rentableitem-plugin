import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractModel, RentableItemPnModel, RentableItemsPnModel, RentableItemsPnRequestModel} from '../../../models';
import {ContractsService, RentableItemsPnService} from '../../../services';
import {formatTimezone} from '../../../../../../common/helpers';
import {CustomersPnModel, CustomersPnRequestModel} from '../../../../customers-pn/models/customer';
import {CustomersPnService} from '../../../../customers-pn/services';

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
  customersRequestModel: CustomersPnRequestModel = new CustomersPnRequestModel();
  customersModel: CustomersPnModel = new CustomersPnModel();
  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();

  constructor(private rentableItemsService: RentableItemsPnService,
              private contractService: ContractsService,
              private extCustomerService: CustomersPnService
              ) { }

  ngOnInit() {
    this.getRentableItems();
    this.getAllCustomers();
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
  getAllCustomers() {
    debugger;
    this.extCustomerService.getAllCustomers(this.customersRequestModel).subscribe((result => {
      if (result && result.success) {
        this.customersModel = result.model;
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
  onSelectedChanged(g: any) {
    this.newContractModel.customerId = g;
  }
}
