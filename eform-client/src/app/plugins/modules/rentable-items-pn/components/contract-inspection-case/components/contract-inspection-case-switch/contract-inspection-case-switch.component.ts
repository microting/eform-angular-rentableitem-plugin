import {Component, Input, OnInit} from '@angular/core';
import {DataItemDto} from 'src/app/common/models';

@Component({
  selector: 'app-installation-case-switch',
  templateUrl: './contract-inspection-case-switch.component.html',
  styleUrls: ['./contract-inspection-case-switch.component.scss']
})
export class ContractInspectionCaseSwitchComponent implements OnInit {
  @Input() dataItemList: Array<DataItemDto> = [];
  constructor() { }

  ngOnInit() {
  }
}
