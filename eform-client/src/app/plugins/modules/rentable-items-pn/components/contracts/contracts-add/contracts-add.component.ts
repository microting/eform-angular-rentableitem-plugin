import {ChangeDetectorRef, Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {
  ContractModel, RentableItemCustomerModel,
  CustomerRequestModel,
  CustomersModel,
  RentableItemPnModel,
  RentableItemsPnModel,
  RentableItemsPnRequestModel
} from '../../../models';
import {ContractsService, RentableItemsPnService} from '../../../services';
import {formatTimezone} from '../../../../../../common/helpers';
import {catchError, debounceTime, distinctUntilChanged, switchMap, tap, map} from 'rxjs/operators';
import {concat, Observable, of, Subject} from 'rxjs';

@Component({
  selector: 'app-contracts-add',
  templateUrl: './contracts-add.component.html',
  styleUrls: ['./contracts-add.component.scss']
})
export class ContractsAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onContractCreated: EventEmitter<void> = new EventEmitter<void>();
  newContractModel: ContractModel = new ContractModel();
  frameShow = true;
  peopleInput$ = new Subject<string>();
  peopleLoading = false;
  people$: Observable<RentableItemCustomerModel[]>;
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
    // this.typeahead
    //   .pipe(
    //     debounceTime(500),
    //     switchMap(term => {
    //       if (term !== null) {
    //         this.customersRequestModel.name = term;
    //         return this.contractService.getCustomer(this.customersRequestModel);
    //       }
    //     })
    //   )
    //   .subscribe(items => {
    //     this.customersModel = items.model;
    //     this.cd.markForCheck();
    //   });
    this.typeahead2
      .pipe(
        debounceTime(500),
        switchMap(term2 => {
          if (term2 !== null && term2 !== '') {
            debugger;
            this.rentableItemsRequestModel.nameFilter = term2;
            return this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel);
          }
        })
      )
      .subscribe( items2 => {
        this.rentableItemsModel = items2.model;
        this.cd.markForCheck();
      });
  }

  ngOnInit() {
    this.loadPeople();
  }

  show() {
    this.newContractModel = new ContractModel();
    this.frame.show();
  }


  trackByFn(item: RentableItemCustomerModel) {
    return item.id;
  }

  private loadPeople() {
    this.people$ = concat(
      of([]), // default items
      this.peopleInput$.pipe(
        distinctUntilChanged(),
        tap(() => this.peopleLoading = true),
        switchMap(term => term !== null ? this.getAllRentableItems(term).pipe(map(val => val.model.customers),
          catchError(() => of([])), // empty list on error
          tap(() => this.peopleLoading = false )
        ) : this.returnValue() )
      )
    );
  }

  private getAllRentableItems(term: string = null): Observable<any> {
     this.customersRequestModel.name = term;
     return this.contractService.getCustomer(this.customersRequestModel);
  }

  private returnValue(): Observable<any> {
    this.peopleLoading = false;
    return new Observable<any>();
  }

  createContract() {
    this.newContractModel.customerId = this.newContractModel.rentableItemCustomer.id;
    this.contractService.createContract(this.newContractModel).subscribe(((data) => {
      if (data && data.success) {
        this.newContractModel = new ContractModel();
        this.newContractModel.rentableItems = [];
        this.customersModel.customers = [];
        this.onContractCreated.emit();
        this.frame.hide();
      }
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
    this.newContractModel.rentableItemCustomer = null;
  }
  getRentableItems() {
    this.rentableItemsService.getAllRentableItems(this.rentableItemsRequestModel).subscribe((result => {
      if (result && result.success) {
        this.rentableItemsModel = result.model;
      }

    }));
  }
  getCustomer() {
    this.contractService.getCustomer(this.customersRequestModel).subscribe((result => {
      if (result && result.success) {
        this.customersModel = result.model;
      }

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
