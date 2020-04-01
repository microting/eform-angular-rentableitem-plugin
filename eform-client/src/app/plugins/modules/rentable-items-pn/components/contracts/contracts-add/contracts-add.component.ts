import {ChangeDetectorRef, Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractModel,
  CustomerRequestModel,
  CustomersModel,
  RentableItemPnModel,
  RentableItemsPnModel,
  RentableItemsPnRequestModel} from '../../../models';
import {ContractsService, RentableItemsPnService} from '../../../services';
import {formatTimezone} from '../../../../../../common/helpers';
import {debounceTime, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-contracts-add',
  templateUrl: './contracts-add.component.html',
  styleUrls: ['./contracts-add.component.scss']
})
export class ContractsAddComponent implements OnInit {
  @ViewChild('frame', {static: false}) frame;
  @Output() onContractCreated: EventEmitter<void> = new EventEmitter<void>();
  newContractModel: ContractModel = new ContractModel();
  spinnerStatus = false;
  frameShow = true;
  rentableItems: Array<RentableItemPnModel> = [];
  customersRequestModel: CustomerRequestModel = new CustomerRequestModel();
  customersModel: CustomersModel = new CustomersModel();
  rentableItemsRequestModel: RentableItemsPnRequestModel = new RentableItemsPnRequestModel();
  rentableItemsModel: RentableItemsPnModel = new RentableItemsPnModel();
  typeahead = new EventEmitter<string>();
  typeahead2 = new EventEmitter<string>();

  constructor(private rentableItemsService: RentableItemsPnService,
              private cd: ChangeDetectorRef,
              private contractService: ContractsService,
              ) {
    this.typeahead
      .pipe(
        debounceTime(200),
        switchMap(term => {
          this.customersRequestModel.name = term;
            return this.contractService.getCustomer(this.customersRequestModel);
        })
      )
      .subscribe(items => {
        this.customersModel = items.model;
        this.cd.markForCheck();
      });
    this.typeahead2
      .pipe(
        debounceTime(200),
        switchMap(term2 => {
          this.rentableItemsRequestModel.nameFilter = term2;
            return this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel);
        })
      )
      .subscribe( items2 => {
        this.rentableItemsModel = items2.model;
        this.cd.markForCheck();
      });
  }

  ngOnInit() {
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
        this.newContractModel.rentableItems = [];
        this.customersModel.customers = [];
        this.onContractCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
  addNewRentableItem(e: any) {
    if (!this.newContractModel.rentableItems.includes(e)) {
      this.newContractModel.rentableItems.push(e);
      this.newContractModel.rentableItemIds.push(e.id);
    }
  }
  removeRentableItem(rentableItem: any) {
    const index = this.newContractModel.rentableItems.indexOf(rentableItem);
    this.newContractModel.rentableItems.splice(index, 1);
  }
  removeCustomer(customer: any) {
    const index = this.customersModel.customers.indexOf(customer);
    this.customersModel.customers.splice(index,  1);
  }
  getRentableItems() {
    this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((result => {
      if (result && result.success) {
        this.rentableItemsModel = result.model;
      }
      this.spinnerStatus = false;
    }));
  }
  getCustomer() {
    this.contractService.getCustomer(this.customersRequestModel).subscribe((result => {
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
    this.newContractModel.customerId = g.id;
  }
}
