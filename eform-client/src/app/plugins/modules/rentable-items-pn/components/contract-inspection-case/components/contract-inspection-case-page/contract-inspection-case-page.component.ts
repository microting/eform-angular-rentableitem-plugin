import {Component, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CasesService} from 'src/app/common/services/cases';
import {EFormService} from 'src/app/common/services/eform';
import {CaseEditElementComponent} from '../../../../../../../modules/cases/components';
import {TemplateDto} from '../../../../../../../common/models/dto';
import {CaseEditRequest, ReplyElementDto, ReplyRequest} from '../../../../../../../common/models/cases';
import {AuthService} from '../../../../../../../common/services/auth';
import {ContractInspectionModel, RentableItemCustomerModel, RentableItemPnModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {
  ContractInspectionsService,
  ContractRentableItemService, ContractsService,
  RentableItemsPnService
} from 'src/app/plugins/modules/rentable-items-pn/services';

@Component({
  selector: 'app-installation-case-page',
  templateUrl: './contract-inspection-case-page.component.html',
  styleUrls: ['./contract-inspection-case-page.component.scss']
})
export class ContractInspectionCasePageComponent implements OnInit {
  @ViewChildren(CaseEditElementComponent) editElements: QueryList<CaseEditElementComponent>;
  @ViewChild('caseConfirmation', {static: false}) caseConfirmation;
  id: number;
  customerId: number;
  rentableItemId: number;
  eFormId: number;
  currenteForm: TemplateDto = new TemplateDto;
  contractInspectionModel: ContractInspectionModel = new ContractInspectionModel();
  replyElement: ReplyElementDto = new ReplyElementDto();
  reverseRoute: string;
  customerModel: RentableItemCustomerModel = new RentableItemCustomerModel();
  selectedRentableItemModel: RentableItemPnModel = new RentableItemPnModel();
  requestModels: Array<CaseEditRequest> = [];
  replyRequest: ReplyRequest = new ReplyRequest();

  get userClaims() {
    return this.authService.userClaims;
  }

  constructor(private activateRoute: ActivatedRoute,
              private casesService: CasesService,
              private eFormService: EFormService,
              private router: Router,
              private authService: AuthService,
              private contractInspectionsService: ContractInspectionsService,
              private contractService: ContractsService,
              private rentableItemsService: RentableItemsPnService) {
    this.activateRoute.params.subscribe(params => {
      this.id = +params['sdkCaseId'];
      this.customerId = +params['customerId'];
      this.rentableItemId = +params['rentableItemId'];
      this.eFormId = +params['templateId'];
    });
  }

  ngOnInit() {
    this.loadTemplateInfo();
    // this.loadInstallationInfo();
  }

  loadCase() {
    if (!this.id || this.id === 0) {
      return;
    }
    this.casesService.getById(this.id, this.currenteForm.id).subscribe(operation => {
      if (operation && operation.success) {
        this.replyElement = operation.model;
        this.loadRentableItem();
      }
    });
  }

  loadRentableItem() {
    this.rentableItemsService.readReantableItem(this.rentableItemId).subscribe(operation => {
      if (operation && operation.success) {
        this.selectedRentableItemModel = operation.model;
      }
    });
  }

  loadTemplateInfo() {
    if (this.eFormId) {
      this.eFormService.getSingle(this.eFormId).subscribe(operation => {
        if (operation && operation.success) {
          this.currenteForm = operation.model;
          this.loadCase();
          this.getCustomer(this.customerId);
        }
      });
    }
  }

  getCustomer(customerId: number) {
    this.contractService.getSingleCustomer(customerId).subscribe( data => {
      if (data && data.success) {
        this.customerModel = data.model;
        // this.selectedContractModel.customerId = this.customerModel.id;
      }
    });
  }

  saveCase(navigateToPosts?: boolean) {
    this.requestModels = [];
    this.editElements.forEach(x => {
      x.extractData();
      this.requestModels.push(x.requestModel);
    });
    this.replyRequest.id = this.replyElement.id;
    this.replyRequest.label = this.replyElement.label;
    this.replyRequest.elementList = this.requestModels;
    this.casesService.updateCase(this.replyRequest, this.currenteForm.id).subscribe(operation => {
      if (operation && operation.success) {
        this.replyElement = new ReplyElementDto();
        this.router.navigate(['/plugins/rentable-items-pn/inspections']).then();
        // this.isNoSaveExitAllowed = true;
        // if (navigateToPosts) {
        //   this.router.navigate(['/cases/posts/', this.id , this.currentTemplate.id, 'new']).then();
        // } else if (this.isSaveClicked) {
        //   this.navigateToReverse();
        // }
      }
    });
  }
  // loadInstallationInfo() {
  //   if (this.contractInspectionId) {
  //     this.contractInspectionsService.getInspection(this.contractInspectionId).subscribe(operation => {
  //       if (operation && operation.success) {
  //         this.contractInspectionModel = operation.model;
  //         this.loadCase();
  //       }
  //     });
  //   }
  // }


  goToSection(location: string): void {
    window.location.hash = location;
    setTimeout(() => {
      document.querySelector(location).parentElement.scrollIntoView();
    });
  }
}
