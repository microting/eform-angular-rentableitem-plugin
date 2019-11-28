import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEform';
import {trackByHourSegment} from 'angular-calendar/modules/common/util';
import {until} from 'selenium-webdriver';
import titleIs = until.titleIs;
import {isBrowserEvents} from '@angular/core/src/render3/discovery_utils';

export class RentableItemContractPage extends Page {
  constructor() {
    super();
  }
  public get rowNum(): number {
    return $$(`//*[@id= 'tableBody']//tr`).length;
  }
  public get rentableItemListonContract(): number {
    return $$(`//*[@id= 'tableBody']//tr//*[@id= 'rentableItemId']`).length;
  }
  public rentableItemDropdownItemName(name) {
    return browser.element(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
  }
  public rentableItemDropdown() {
    browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
  }
  public get newContractBtn() {
    return browser.element('#contractCreateBtn');
  }
  public get startDateSelector() {
    return browser.element('#startDate');
  }
  public get editStartDateSelector() {
    return browser.element('#editStartDate');
  }
  public get endDateSelector() {
    return browser.element('#endDate');
  }
  public get editEndDateSelector() {
    return browser.element('#editEndDate');
  }
  public get contractNumberField() {
    return browser.element('#contractNumber');
  }
  public get editContractNumberField() {
    return browser.element('#editContractNr');
  }
  public get customerSelector() {
    return browser.element(`//*[@id= 'customerSelector']//input`);
  }
  public get editCustomerSelector() {
    return browser.element(`//*[@id= 'editCustomerIdSelector']//input`);
  }
  public get rentableItemSelector() {
    return browser.element(`//*[@id= 'rentableItemSelector']//input`);
  }
  public get editRentableItemSelector() {
    return browser.element(`//*[@id= 'editRentableItemSelector']//input`);
  }
  public get contractCreateSaveBtn() {
    return browser.element('#contractCreateSaveBtn');
  }
  public get contractCreateCancelBtn() {
    return browser.element('#contractCreateDeleteBtn');
  }
  public get contractEditSaveBtn() {
    return browser.element('#contractEditSaveBtn');
  }
  public get contractEditCancelBtn() {
    return browser.element('#contractEditCancelBtn');
  }
  public get selectedContractId() {
    return browser.element('#selectedContractId');
  }
  public get selectedContractNumber() {
    return browser.element('#selectedContractNr');
  }
  public get selectedContractCustomerId() {
    return browser.element('#selectedContractCustomerId');
  }
  public get contractDeleteDeleteBtn() {
    return browser.element('#contractDeleteDeleteBtn');
  }
  public get contractDeleteCancelBtn() {
    return browser.element('#contractDeleteCancelBtn');
  }
  public clickDate(date) {
    browser.element(`//*[text()="${date}"]`).click();
  }
  public selectOption(name) {
    browser.element(`//*[text()="${name}"]`).click();
  }
  public createContract(startDate: number, endDate: number, contractNumber: number, customer: string, rentableItem: string) {
    this.newContractBtn.click();
    browser.waitForVisible('#startDate', 20000);
    this.startDateSelector.click();
    browser.pause(2000);
    this.clickDate(startDate);
    browser.pause(2000);
    this.endDateSelector.click();
    browser.pause(2000);
    this.clickDate(endDate);
    browser.pause(2000);
    this.contractNumberField.addValue(contractNumber);
    browser.pause(2000);
    this.customerSelector.addValue(customer);
    browser.pause(2000);
    this.selectOption(customer);
    browser.pause(2000);
    this.rentableItemSelector.addValue(rentableItem);
    browser.pause(2000);
    this.selectOption(rentableItem);
    browser.pause(2000);
    this.contractCreateSaveBtn.click();
  }
  public createContractCancel(startDate: number, endDate: number, contractNumber: number, customer: string, rentableItem: string) {
    this.newContractBtn.click();
    browser.waitForVisible('#startDate', 20000);
    this.startDateSelector.click();
    browser.pause(2000);
    this.clickDate(startDate);
    browser.pause(2000);
    this.endDateSelector.click();
    browser.pause(2000);
    this.clickDate(endDate);
    browser.pause(2000);
    this.contractNumberField.addValue(contractNumber);
    browser.pause(2000);
    this.customerSelector.addValue(customer);
    browser.pause(2000);
    this.selectOption(customer);
    browser.pause(2000);
    this.rentableItemSelector.addValue(rentableItem);
    browser.pause(2000);
    this.selectOption(rentableItem);
    browser.pause(2000);
    this.contractCreateCancelBtn.click();
  }

  public editContract(startDate: number, endDate: number, contractNumber: number, newCustomer: string, newItem: string) {
    const contractForEdit = this.getFirstContractObject();
    contractForEdit.editBtn.click();
    browser.waitForVisible('#editStartDate', 20000);
    this.editStartDateSelector.click();
    browser.pause(2000);
    this.clickDate(startDate);
    browser.pause(2000);
    this.editEndDateSelector.click();
    browser.pause(2000);
    this.clickDate(endDate);
    browser.pause(2000);
    this.editContractNumberField.clearElement();
    browser.pause(1000);
    this.editContractNumberField.addValue(contractNumber);
    browser.pause(2000);
    this.editCustomerSelector.addValue(newCustomer);
    browser.pause(2000);
    this.selectOption(newCustomer);
    browser.pause(2000);
    this.editRentableItemSelector.addValue(newItem);
    browser.pause(2000);
    this.selectOption(newItem);
    browser.pause(2000);
    this.contractEditSaveBtn.click();
    browser.pause(4000);
  }
  public editContractCancel(startDate: number, endDate: number, contractNumber: number, newCustomer: string, newItem: string) {
    const contractForEdit = this.getFirstContractObject();
    contractForEdit.editBtn.click();
    browser.waitForVisible('#editStartDate', 20000);
    this.editStartDateSelector.click();
    browser.pause(2000);
    this.clickDate(startDate);
    browser.pause(2000);
    this.editEndDateSelector.click();
    browser.pause(2000);
    this.clickDate(endDate);
    browser.pause(2000);
    this.editContractNumberField.clearElement();
    browser.pause(1000);
    this.editContractNumberField.addValue(contractNumber);
    browser.pause(2000);
    this.editCustomerSelector.addValue(newCustomer);
    browser.pause(2000);
    this.selectOption(newCustomer);
    browser.pause(2000);
    this.editRentableItemSelector.addValue(newItem);
    browser.pause(2000);
    this.selectOption(newItem);
    browser.pause(2000);
    this.contractEditCancelBtn.click();
    browser.pause(4000);
  }
  public editContractDeleteRentableItem() {
    const contractForEdit = this.getFirstContractObject();
    contractForEdit.editBtn.click();
    browser.pause(2000);
    const rentableItemToDelete = this.getFirstRentableItemObject();
    rentableItemToDelete.deleteBtn.click();
    browser.pause(2000);
    this.contractEditSaveBtn.click();
    browser.pause(4000);
  }
  public deleteContract() {
    const deleteObject = this.getFirstContractObject();
    if (deleteObject != null) {
      browser.pause(8000);
      deleteObject.deleteBtn.click();
      browser.pause(4000);
      this.contractDeleteDeleteBtn.click();
      browser.pause(2000);
      browser.refresh();
    }
  }
  public deleteContractCancel() {
    const deleteObject = this.getFirstContractObject();
    if (deleteObject != null) {
      browser.pause(8000);
      deleteObject.deleteBtn.click();
      browser.pause(4000);
      this.contractDeleteCancelBtn.click();
      browser.pause(2000);
      browser.refresh();
    }
  }

  public getFirstRentableItemObject(): RentableItemRowObject {
      return new RentableItemRowObject(1);
  }

  public getFirstContractObject(): ContractsRowObject {
    return new ContractsRowObject(1);
  }

  public cleanup() {
    const deleteObject = this.getFirstContractObject();
    if (deleteObject != null) {
      browser.pause(8000);
      deleteObject.deleteBtn.click();
      browser.pause(4000);
      this.contractDeleteDeleteBtn.click();
      browser.pause(2000);
      browser.refresh();
    }
  }
}

const contractsPage = new RentableItemContractPage();
export default contractsPage;

export class ContractsRowObject {
  constructor(rowNum) {
    if ($$('#contractId')[rowNum - 1]) {
      this.id = +$$('#contractId')[rowNum - 1];
      try {
        this.startDate = +$$('#contractStartDate')[rowNum - 1];
      } catch (e) {}
      try {
        this.endDate = +$$('#contractEndData')[rowNum - 1];
      } catch (e) {}
      try {
        this.customerId = +$$('#contractCustomerId')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.contractNumber = +$$('#contractNr')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.editBtn = $$('#contractEditBtn')[rowNum - 1];
      } catch (e) {}
      try {
        this.deleteBtn = $$('#contractDeleteBtn')[rowNum - 1];
      } catch (e) {}
      try {
        this.inspectionBtn = $$('#createInspectionBtn')[rowNum - 1];
      } catch (e) {}
    }
  }

  id;
  startDate;
  endDate;
  customerId;
  contractNumber;
  inspectionBtn;
  deleteBtn;
  editBtn;
}

export class RentableItemRowObject {
  constructor(rowNum) {
    if ($$('#rentableItemId')[rowNum - 1]) {
      try {
        this.modelName = $$('#rentableItemModelName')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.brand = $$('#rentableItemBrand')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.serialNumber = +$$('#rentableItemSerialNumber')[rowNum - 1];
      } catch (e) {}
      try {
        this.deleteBtn = $$('#removeRentableItemBtn')[rowNum - 1];
      } catch (e) {}
    }
  }
  id;
  modelName;
  brand;
  serialNumber;
  deleteBtn;
}
