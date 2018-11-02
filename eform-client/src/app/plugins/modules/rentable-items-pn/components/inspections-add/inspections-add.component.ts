import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import { InspectionModel} from '../../models';
import {InspectionsService} from '../../services';
import {DeviceUserService} from '../../../../../common/services/device-users';
import {SimpleSiteModel} from '../../../../../common/models/device-users';
import {SiteDto} from 'src/app/common/models/dto';


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
  sitesDto: Array<SiteDto> = [];
  frameShow = true;
  constructor(private inspectionService: InspectionsService, private deviceUsersService: DeviceUserService) { }

  ngOnInit() {
    this.loadAllSimpleSites();
  }

  show() {
    this.newInspectionModel = new InspectionModel();
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
    this.inspectionService.createInspection(this.newInspectionModel).subscribe(((data) => {
      if (data && data.success) {
        this.newInspectionModel = new InspectionModel();
        this.onInspectionCreated.emit();
        this.frame.hide();
      } this.spinnerStatus = false;
    }));
  }
}
