import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractModel} from '../../models';
import {ContractsService} from '../../services';
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
  constructor(private contractService: ContractsService) { }

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
        this.onContractCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }

  onStartDateSelected(e: any) {
    this.newContractModel.contractStart = formatTimezone(e.value._d);
  }
  onEndDateSelected(f: any) {
    this.newContractModel.contractEnd = formatTimezone(f.value._d);
  }
}
