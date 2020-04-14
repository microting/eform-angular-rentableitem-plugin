import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractInspectionModel, ContractsModel, ContractsRequestModel} from '../../../models';
import {ContractInspectionsService, ContractsService} from '../../../services';
import {formatTimezone} from '../../../../../../common/helpers';

@Component({
  selector: 'app-inspections-update',
  templateUrl: './contract-inspections-update.component.html',
  styleUrls: ['./contract-inspections-update.component.scss']
})
export class ContractInspectionsUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onInspectionUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedContractInspectionModel: ContractInspectionModel = new ContractInspectionModel();
  spinnerStatus = false;

  constructor(private contractInspectionsService: ContractInspectionsService) {
  }

  ngOnInit() {
  }
  show(contractInspectionModel: ContractInspectionModel) {
    this.selectedContractInspectionModel = contractInspectionModel;
    this.frame.show();
  }
  updateInspection() {
    this.spinnerStatus = true;
    this.contractInspectionsService.updateInspection(this.selectedContractInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        this.onInspectionUpdated.emit();
        this.frame.hide();
      }
      this.spinnerStatus = false;
    }));
  }

  onDoneAtSelected(e: any) {
    this.selectedContractInspectionModel.doneAt = formatTimezone(e.value._d);
  }
}
