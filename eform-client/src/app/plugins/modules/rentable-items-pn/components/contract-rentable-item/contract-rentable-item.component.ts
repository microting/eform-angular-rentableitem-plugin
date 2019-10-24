import {Component, OnInit, ViewChild} from '@angular/core';
import {ContractModel, RentableItemsPnModel} from "../../models";
import {ContractRentableItemService} from "../../services";

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

  constructor(private contractRentableItemService: ContractRentableItemService) { }

  ngOnInit() {
  }
  show(contractId: number, contract: ContractModel) {
    this.frame.show();
    this.selectedContractModel = contract;
    this.getAllRentableItemsOnContract(contractId);
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

}
