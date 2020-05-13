import {Component, OnInit, ViewChild} from '@angular/core';
import {ContractModel, RentableItemsPnModel, RentableItemCustomerModel} from "../../models";
import {ContractRentableItemService, ContractsService, RentableItemsPnService} from '../../services';

@Component({
  selector: 'app-contract-rentable-item',
  templateUrl: './contract-rentable-item.component.html',
  styleUrls: ['./contract-rentable-item.component.scss']
})
export class ContractRentableItemComponent implements OnInit {
  @ViewChild('frame') frame;
  frameShow = true;
  selectedContractModel: ContractModel = new ContractModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  customerModel: RentableItemCustomerModel = new RentableItemCustomerModel();

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
    this.contractRentableItemService.getAllRentableItemsFromContract(contractId).subscribe( data => {
      if (data && data.success) {
        this.rentableItemsModel = data.model;
      }
    });

  }

  getCustomer(customerId: number) {
    this.contractService.getSingleCustomer(customerId).subscribe( data => {
      if (data && data.success) {
        this.customerModel = data.model;
      }
    });

  }

}
