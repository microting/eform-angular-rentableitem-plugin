import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {InspectionModel} from '../../models';
import {InspectionsService} from '../../services';
import {formatTimezone} from '../../../../../common/helpers';

@Component({
  selector: 'app-inspections-update',
  templateUrl: './inspections-update.component.html',
  styleUrls: ['./inspections-update.component.scss']
})
export class InspectionsUpdateComponent implements OnInit {
  @ViewChild('frame') frame;
  @Output() onInspectionUpdated: EventEmitter<void> = new EventEmitter<void>();
  selectedInspectionModel: InspectionModel = new InspectionModel();
  spinnerStatus = false;

  constructor(private inspectionService: InspectionsService) {
  }

  ngOnInit() {
  }

  updateInspection() {
    this.spinnerStatus = true;
    this.inspectionService.updateInspection(this.selectedInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        this.onInspectionUpdated.emit();
        this.frame.hide();
      }
      this.spinnerStatus = false;
    }));
  }

  onDoneAtSelected(e: any) {
    this.selectedInspectionModel.doneAt = formatTimezone(e.value);
  }
}
