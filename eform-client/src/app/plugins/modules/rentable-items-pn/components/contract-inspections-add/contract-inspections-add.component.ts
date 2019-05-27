import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ContractInspectionModel, ContractModel} from '../../models';
import {ContractInspectionsService, ContractsService} from '../../services';
import {DeviceUserService} from '../../../../../common/services/device-users';
import {SimpleSiteModel} from '../../../../../common/models/device-users';
import {SiteDto} from 'src/app/common/models/dto';


@Component({
  selector: 'app-inspections-add',
  templateUrl: './contract-inspections-add.component.html',
  styleUrls: ['./contract-inspections-add.component.scss']
})
export class ContractInspectionsAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onInspectionCreated: EventEmitter<void> = new EventEmitter<void>();
  newContractInspectionModel: ContractInspectionModel = new ContractInspectionModel();
  spinnerStatus = false;
  sitesDto: Array<SiteDto> = [];
  selectedContractModel: ContractModel = new ContractModel();
  frameShow = true;
  constructor(private contractService: ContractsService,
              private contractInspectionsService: ContractInspectionsService,
              private deviceUsersService: DeviceUserService) {  }

  ngOnInit() {
    this.loadAllSimpleSites();
  }

  show(contractModel: ContractModel) {
    this.newContractInspectionModel = new ContractInspectionModel();
    this.newContractInspectionModel.contractId = contractModel.id;
    this.selectedContractModel = contractModel;

    this.frame.show();
  }
  loadAllSimpleSites() {
    this.spinnerStatus = true;
    this.deviceUsersService.getAllSimpleSites().subscribe(operation => {
      if (operation && operation.success) {
        this.sitesDto = operation.model;
      }
      this.spinnerStatus = false;
    });
  }

  createInspection() {
    this.spinnerStatus = true;
    this.contractInspectionsService.createInspection(this.newContractInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        this.newContractInspectionModel = new ContractInspectionModel();
        this.onInspectionCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
  onSelectedChanged(e: any) {
    this.newContractInspectionModel.siteId = e.siteId;
  }
}
