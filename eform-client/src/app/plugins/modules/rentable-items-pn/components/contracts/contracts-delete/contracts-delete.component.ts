import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractModel} from '../../../models';
import {ContractsService} from '../../../services';

@Component({
  selector: 'app-contract-delete',
  templateUrl: './contracts-delete.component.html',
  styleUrls: ['./contracts-delete.component.scss']
})
export class ContractsDeleteComponent implements OnInit {
  @ViewChild('frame', {static: false}) frame;
  @Output() onContractDeleted: EventEmitter<void> = new EventEmitter<void>();
  selectedContractModel: ContractModel = new ContractModel();
  spinnerStatus = false;
  constructor(private contractsService: ContractsService) { }

  ngOnInit() {
  }
  show(contractModel: ContractModel) {
    this.selectedContractModel = contractModel;
    this.frame.show();
  }
  deleteContract() {
    this.spinnerStatus = true;
    this.contractsService.deleteContract(this.selectedContractModel.id).subscribe((data) => {
      if (data && data.success) {
        this.onContractDeleted.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    });
  }
}
