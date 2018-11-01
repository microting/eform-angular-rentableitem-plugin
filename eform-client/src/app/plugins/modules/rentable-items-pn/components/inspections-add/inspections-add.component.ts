import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import { InspectionModel} from '../../models';
import {InspectionsService} from '../../services';

@Component({
  selector: 'app-inspections-add',
  templateUrl: './inspections-add.component.html',
  styleUrls: ['./inspections-add.component.scss']
})
export class InspectionsAddComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onInspectionCreated: EventEmitter<void> = new EventEmitter<void>();
  newInspectionModel: InspectionModel = new InspectionModel();
  spinnerStatus = false;
  frameShow = true;
  constructor(private inspectionService: InspectionsService) { }

  ngOnInit() {
  }

  show() {
    this.newInspectionModel = new InspectionModel();
    this.frame.show();
  }

  createInspection() {
    this.spinnerStatus = true;
    this.inspectionService.createInspection(this.newInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        this.newInspectionModel = new InspectionModel();
        this.onInspectionCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
}
