import {Component, OnInit, ViewChild} from '@angular/core';
import {ContractModel, RentableItemsPnModel, CustomerModel} from "../../models";
import {ContractRentableItemService, ContractsService, RentableItemsPnService} from '../../services';

@Component({
  selector: 'app-contract-rentable-item',
  templateUrl: './contract-rentable-item.component.html',
  styleUrls: ['./contract-rentable-item.component.scss']
})
export class ContractRentableItemComponent implements OnInit {
  @ViewChild('frame') frame;
  frameShow = true;
  spinnerStatus = false;
  selectedContractModel: ContractModel = new ContractModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  customerModel: CustomerModel = new CustomerModel();

  constructor(private contractRentableItemService: ContractRentableItemService,
              private contractService: ContractsService
  ) { }

  ngOnInit() {
  }
  show(contractId: number, contract: ContractModel) {
    this.frame.show();
    this.selectedContractModel = contract;
    this.getAllRentableItemsOnContract(contractId);
    this.getCustomer(contract.customerId);
  }

  getAllRentableItemsOnContract(contractId: number) {
    this.spinnerStatus = true;
    this.contractRentableItemService.getAllRentableItemsFromContract(contractId).subscribe( data => {
      if (data && data.success) {
        this.rentableItemsModel = data.model;
      }
    });
    this.spinnerStatus = false;
  }

  getCustomer(customerId: number) {
    this.spinnerStatus = true;
    this.contractService.getSingleCustomer(customerId).subscribe( data => {
      if (data && data.success) {
        this.customerModel = data.model;
      }
    });
    this.spinnerStatus = false;
  }

}
