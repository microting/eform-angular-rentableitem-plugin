import {Component, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CasesService} from 'src/app/common/services/cases';
import {EFormService} from 'src/app/common/services/eform';
import {CaseEditElementComponent} from '../../../../../../../modules/cases/components';
import {TemplateDto} from '../../../../../../../common/models/dto';
import {ReplyElementDto} from '../../../../../../../common/models/cases';
import {AuthService} from '../../../../../../../common/services/auth';
import {ContractInspectionModel} from 'src/app/plugins/modules/rentable-items-pn/models';
import {
  ContractInspectionsService,
  ContractRentableItemService,
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
  contractInspectionId: number;
  templateId: number;
  currentTemplate: TemplateDto = new TemplateDto;
  contractInspectionModel: ContractInspectionModel = new ContractInspectionModel();
  replyElement: ReplyElementDto = new ReplyElementDto();
  reverseRoute: string;

  get userClaims() {
    return this.authService.userClaims;
  }

  constructor(private activateRoute: ActivatedRoute,
              private casesService: CasesService,
              private eFormService: EFormService,
              private router: Router,
              private authService: AuthService,
              private contractInspectionsService: ContractInspectionsService) {
    this.activateRoute.params.subscribe(params => {
      this.id = +params['id'];
      this.contractInspectionId = +params['installationId'];
      this.templateId = +params['templateId'];
    });
  }

  ngOnInit() {
    this.loadTemplateInfo();
    this.loadInstallationInfo();
  }

  loadCase() {
    if (!this.id || this.id === 0) {
      return;
    }
    this.casesService.getById(this.id, this.currentTemplate.id).subscribe(operation => {
      if (operation && operation.success) {
        this.replyElement = operation.model;
      }
    });
  }

  loadTemplateInfo() {
    if (this.templateId) {
      this.eFormService.getSingle(this.templateId).subscribe(operation => {
        if (operation && operation.success) {
          this.currentTemplate = operation.model;
          this.loadCase();
        }
      });
    }
  }

  loadInstallationInfo() {
    if (this.contractInspectionId) {
      this.contractInspectionsService.getInspection(this.contractInspectionId).subscribe(operation => {
        if (operation && operation.success) {
          this.contractInspectionModel = operation.model;
          this.loadCase();
        }
      });
    }
  }


  goToSection(location: string): void {
    window.location.hash = location;
    setTimeout(() => {
      document.querySelector(location).parentElement.scrollIntoView();
    });
  }
}
