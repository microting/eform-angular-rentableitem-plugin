import { Component, OnInit } from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {Papa} from 'ngx-papaparse';
import {RentableItemsImportModel} from '../../models';
import {RentableItemsPnService} from '../../services';
import {EFormService} from '../../../../../common/services/eform';
import {TemplateListModel, TemplateRequestModel} from '../../../../../common/models/eforms';
import {RentableItemsPnHeadersModel} from '../../models/rentableItems-pn-headers.model';

const URL = '';
@Component({
  selector: 'app-importer',
  templateUrl: './importer.component.html',
  styleUrls: ['./importer.component.scss']
})
export class ImporterComponent implements OnInit {
  tableData: any = null;
  public data: any = [];
  uploader: FileUploader;
  spinnerStatus = false;
  totalColumns: number;
  totalRows: number;
  papa: Papa = new Papa();
  rentableItemImportModel: RentableItemsImportModel;
  rentableItemsHeadersModel: RentableItemsPnHeadersModel;
  templateRequestModel: TemplateRequestModel = new TemplateRequestModel();
  templatesModel: TemplateListModel = new TemplateListModel();
  headerValue: number;

  constructor(private eFormService: EFormService,
              private rentableItemsService: RentableItemsPnService) {
  this.rentableItemImportModel = new RentableItemsImportModel();
  this.rentableItemsHeadersModel = new RentableItemsPnHeadersModel();

    this.uploader = new FileUploader(
      {
        url: URL,
        autoUpload: true,
        isHTML5: true,
        removeAfterUpload: true
      });
    this.uploader.onAfterAddingFile = (fileItem => {
      fileItem.withCredentials = false;
      // console.log(fileItem._file);
    });
  }

  ngOnInit() {
    this.getAlleForms();
  }
  csv2Array(fileInput) {
    const file = fileInput;
    this.papa.parse(fileInput.target.files[0], {
      skipEmptyLines: true,
      header: false,
      complete: (results) => {
        this.tableData = results.data;
        console.log(this.tableData);
        // debugger;
        this.headerValue = 0;
        this.tableData[0].forEach((header) => {
          this.rentableItemsHeadersModel = new RentableItemsPnHeadersModel();
          this.rentableItemsHeadersModel.headerLabel = header;
          this.rentableItemsHeadersModel.headerValue = this.headerValue;
          this.rentableItemImportModel.headerList.push(this.rentableItemsHeadersModel);
          this.headerValue += 1;
        });
        this.rentableItemImportModel.importList = JSON.stringify(this.tableData);
      }
    });
    return this.tableData;
  }

  import() {
    this.spinnerStatus = true;
    // this.customerImportModel.importList = this.tableData;
    this.rentableItemImportModel.headers = JSON.stringify(this.rentableItemImportModel.headerList);
    // debugger;
    return this.rentableItemsService.import(this.rentableItemImportModel).subscribe(((data) => {
      if (data && data.success) {
        this.rentableItemImportModel = new RentableItemsImportModel();
      } this.spinnerStatus = false;
    }));
  }

  getAlleForms() {
    this.spinnerStatus = true;
    this.eFormService.getAll(this.templateRequestModel).subscribe(data => {
      if (data && data.success) {
        this.templatesModel = data.model;
      }
      this.spinnerStatus = false;
    });
  }

  onSelectedChanged(e: any) {
    // debugger;
    this.rentableItemImportModel.eFormID = e.id;
  }
}
