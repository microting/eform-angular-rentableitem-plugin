import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {EformReportModel} from 'src/app/common/models/eforms/report';
import {ContractInspectionModel} from '../../../../models';

@Component({
  selector: 'app-installation-case-header',
  templateUrl: './contract-inspection-case-header.component.html',
  styleUrls: ['./contract-inspection-case-header.component.scss']
})
export class ContractInspectionCaseHeaderComponent implements OnInit {
  @Input() contractInspectionModel: ContractInspectionModel = new ContractInspectionModel();
  @ViewChild('reportCropperModal', {static: false}) reportCropperModal;
  constructor() { }

  ngOnInit() {
  }
}
