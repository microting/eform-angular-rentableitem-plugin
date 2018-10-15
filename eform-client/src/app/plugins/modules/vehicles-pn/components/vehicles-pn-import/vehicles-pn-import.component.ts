import { Component, OnInit } from '@angular/core';
import {Papa} from 'ngx-papaparse';
import { VehiclePnImportModel} from '../../models/vehicle-pn-import.model';
import {VehiclesPnService} from '../../services';
import {CustomerPnHeadersModel} from '../../../customers-pn/models/customer';
import {VehiclesPnHeadersModel} from '../../models/vehicles-pn-headers.model';

@Component({
  selector: 'app-vehicles-pn-import',
  templateUrl: './vehicles-pn-import.component.html',
  styleUrls: ['./vehicles-pn-import.component.scss']
})
export class VehiclesPnImportComponent implements OnInit {
  papa: Papa = new Papa();
  tableData: any = null;
  vehicleImportModel: VehiclePnImportModel;
  vehicleHeaderModel: VehiclesPnHeadersModel;
  spinnerStatus = false;
  options = [
    {value: 0, label: 'ContractNumber'},
    {value: 1, label: 'CustomerName'},
    {value: 2, label: 'Brand'},
    {value: 3, label: 'Model'},
    {value: 4, label: 'RegistrationDate'},
    {value: 5, label: 'Win Number'},
    {value: 6, label: 'ContractStartDate'},
    {value: 7, label: 'ContractEndDate'}
  ];
  constructor(private vehicleService: VehiclesPnService) {
    this.vehicleImportModel = new VehiclePnImportModel();
    this.options.forEach((option) => {
      this.vehicleHeaderModel = new VehiclesPnHeadersModel();
      this.vehicleHeaderModel.headerLabel = option.label;
      this.vehicleHeaderModel.headerValue = option.value;
      this.vehicleImportModel.headerList.push(this.vehicleHeaderModel);
      // console.log(label);
    }
    );
  }

  ngOnInit() {
  }
  csv2Array(fileInput) {
    this.papa.parse(fileInput.target.files[0], {
      skipEmptyLines: true,
      header: false,
      complete: (results) => {
        this.tableData = results.data;
        console.log(this.tableData);
        this.vehicleImportModel.importList = JSON.stringify(this.tableData);
      }
    });
    return this.tableData;
  }
  importVehicles() {
    this.spinnerStatus = true;
    // this.customerImportModel.importList = this.tableData;
    // debugger;
    this.vehicleImportModel.headers = JSON.stringify(this.vehicleImportModel.headerList);
    return this.vehicleService.importVehicle(this.vehicleImportModel).subscribe(((data) => {
      if (data && data.success) {
        this.vehicleImportModel = new VehiclePnImportModel();
      } this.spinnerStatus = false;
    }));
  }
  onSelectedChanged(e: any, columnIndex: any) {
    this.vehicleImportModel.headerList[e.value].headerValue = columnIndex;
  }
}
