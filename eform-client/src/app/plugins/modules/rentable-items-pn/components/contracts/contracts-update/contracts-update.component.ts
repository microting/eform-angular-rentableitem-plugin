import {ChangeDetectorRef, Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractRentableItemService, ContractsService, RentableItemsPnService} from '../../../services';
import {ContractModel, CustomerModel, CustomersModel, RentableItemsPnModel, RentableItemsPnRequestModel} from '../../../models';
import {formatTimezone} from '../../../../../../common/helpers';
import {debounceTime, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-contracts-update',
  templateUrl: './contracts-update.component.html',
  styleUrls: ['./contracts-update.component.scss']
})
export class ContractsUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onContractUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedContractModel: ContractModel = new ContractModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  customerModel: CustomerModel = new CustomerModel();
  customersModel: CustomersModel = new CustomersModel();
  typeahead = new EventEmitter<string>();

  spinnerStatus = false;

  constructor(private contractRentableItemService: ContractRentableItemService,
              private contractService: ContractsService,
              private cd: ChangeDetectorRef,
              private rentableItemsService: RentableItemsPnService) {
    this.typeahead
      .pipe(
        debounceTime(200),
        switchMap(term => {
          this.rentableItemsRequestModel.nameFilter = term;
          return this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel);
        })
      )
      .subscribe( items => {
        this.rentableItemsModel = items.model;
        this.cd.markForCheck();
      });
  }

  ngOnInit() {
  }

  show(contractModel: ContractModel) {
    this.selectedContractModel = contractModel;
    this.frame.show();
    this.getAllRentableItemsOnContract(contractModel.id);
    this.getCustomer(contractModel.customerId);
  }

  updateContract() {
    this.spinnerStatus = true;
    this.contractService.updateContract(this.selectedContractModel).subscribe(((data) => {
      if (data && data.success) {
        this.onContractUpdated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
  getAllRentableItemsOnContract(contractId: number) {
    this.spinnerStatus = true;
    this.contractRentableItemService.getAllRentableItemsFromContract(contractId).subscribe( data => {
      if (data && data.success) {
        this.rentableItemsModel = data.model;
        this.selectedContractModel.rentableItems = this.rentableItemsModel.rentableItems;
        this.selectedContractModel.rentableItemIds = this.rentableItemsModel.rentableItemIds;
      }
    });
    this.spinnerStatus = false;
  }
  addNewRentableItem(e: any) {
    debugger;
    if (!this.selectedContractModel.rentableItems.includes(e)) {
      this.selectedContractModel.rentableItems.push(e);
      this.selectedContractModel.rentableItemIds.push(e.id);
    }
  }
  removeRentableItem(rentableItem: any) {
    const index = this.selectedContractModel.rentableItems.indexOf(rentableItem);
    this.selectedContractModel.rentableItems.splice(index, 1);
    this.selectedContractModel.rentableItemIds.splice(index, 1);
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
  removeCustomer(customer: any) {
    const index = this.customersModel.customers.indexOf(customer);
    this.customersModel.customers.splice(index,  1);
  }
  onStartDateSelected(e: any) {
    this.selectedContractModel.contractStart = formatTimezone(e.value._d);
  }

  onEndDateSelected(e: any) {
    this.selectedContractModel.contractEnd = formatTimezone(e.value._d);
  }
}
