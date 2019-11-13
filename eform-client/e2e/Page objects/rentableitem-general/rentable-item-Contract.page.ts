import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEform';
import {trackByHourSegment} from 'angular-calendar/modules/common/util';
import {until} from 'selenium-webdriver';
import titleIs = until.titleIs;

export class RentableItemContractPage extends Page {
  constructor() {
    super();
  }
  public get rowNum(): number {
    return $$('#tableBody > tr').length;
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
  public get endDateSelector() {
    return browser.element('#endDate');
  }
  public get contractNumberField() {
    return browser.element('#contractNumber');
  }
  public get customerSelector() {
    return browser.element(`//*[@id= 'customerSelector']//input`);
  }
  public get rentableItemSelector() {
    return browser.element(`//*[@id= 'rentableItemSelector']//input`);
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






  public getFirstContractObject(): ContractsRowObject {
    return new ContractsRowObject(1);
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
    }
  }

  id;
  startDate;
  endDate;
  customerId;
  contractNumber;
  rentableItem;
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
