import {Component, EventEmitter, Input, OnInit, Output, QueryList, ViewChildren} from '@angular/core';
import {EformReportDataItem, EformReportElement, ElementDto} from 'src/app/common/models';
import {UUID} from 'angular2-uuid';

@Component({
  selector: 'app-installation-case-block',
  templateUrl: './contract-inspection-case-block.component.html',
  styleUrls: ['./contract-inspection-case-block.component.scss']
})
export class ContractInspectionCaseBlockComponent implements OnInit {
  @Input() element: ElementDto = new ElementDto();

  constructor() { }

  ngOnInit() {
  }
}
