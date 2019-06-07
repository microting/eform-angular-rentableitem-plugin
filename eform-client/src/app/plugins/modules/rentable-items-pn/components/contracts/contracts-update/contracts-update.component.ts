import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractsService} from '../../../services';
import {ContractModel} from '../../../models';
import {formatTimezone} from '../../../../../../common/helpers';

@Component({
  selector: 'app-contracts-update',
  templateUrl: './contracts-update.component.html',
  styleUrls: ['./contracts-update.component.scss']
})
export class ContractsUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onContractUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedContractModel: ContractModel = new ContractModel();
  spinnerStatus = false;

  constructor(private contractService: ContractsService) { }

  ngOnInit() {
  }

  show(contractModel: ContractModel) {
    this.selectedContractModel = contractModel;
    this.frame.show();
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

  onStartDateSelected(e: any) {
    this.selectedContractModel.contractStart = formatTimezone(e.value._d);
  }

  onEndDateSelected(e: any) {
    this.selectedContractModel.contractEnd = formatTimezone(e.value._d);
  }
}
