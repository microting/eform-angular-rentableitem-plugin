import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEform';
import {trackByHourSegment} from 'angular-calendar/modules/common/util';
import {until} from 'selenium-webdriver';
import titleIs = until.titleIs;

export class RentableItemRentableItemPage extends Page {
  constructor() {
    super();
  }
  public get rowNum(): number {
    return $$('#tableBody > tr').length;
  }
  public get newEformBtn() {
    return browser.element('#newEFormBtn');
  }
  public get xmlTextArea() {
    return browser.element('#eFormXml');
  }
  public get createEformBtn() {
    return browser.element('#createEformBtn');
  }
  public get createEformTagSelector() {
    return browser.element('#createEFormMultiSelector');
  }
  public get createEformNewTagInput() {
    return browser.element('#addTagInput');
  }
  public get rentableItemDropdownName() {
    return browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).element('..');
  }
  public rentableItemDropdown() {
    browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
  }
  public rentableItemDropdownItemName(name) {
    return browser.element(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
  }
  public get rentableItemCreateBtn() {
    return browser.element('#rentableItemCreateBtn');
  }
  public getBtnTxt(text: string) {
    return browser.element(`//*[contains(@class, 'p-3')]//*[text()="${text}"]`);
  }
  public get rentableItemCreateBrandBox() {
    return browser.element('#createBrand');
  }
  public get rentableItemCreateModelBox() {
    return browser.element('#createRentableItemModel');
  }
  public rentableItemCreateReistrationDate() {
    return browser.element(`//*[contains(@class, 'form')]//*[contains(text(), 'Registration date')]`);
  }
  public get rentableItemCreateVinNumberBox() {
    return browser.element('#createVINNumber');
  }
  public get rentableItemCreateSerialNumberBox() {
    return browser.element('#createSerialNumber');
  }
  public get rentableItemCreatePlateNumberBox() {
    return browser.element('#createPlateNumber');
  }
  public get eFormSelector() {
    return browser.element(`//*[@id= 'eFormId']//input`);
  }
  public get rentableItemCreateSaveBtn() {
    return browser.element('#rentableItemCreateSaveBtn');
  }
  public get rentableItemEditBrandBox() {
    return browser.element('#editBrand');
  }
  public get rentableItemEditModelBox() {
    return browser.element('#editRentableItemModel');
  }
  public rentableItemEditReistrationDate() {
    // return browser.element(`//*[contains(@class, 'form')]//*[contains(text(), 'Registration date')]`);
    return browser.element('#editRegistrationDate');
  }
  public get rentableItemEditVinNumberBox() {
    return browser.element('#editVINNumber');
  }
  public get rentableItemEditSerialNumberBox() {
    return browser.element('#editSerialNumber');
  }
  public get rentableItemEditPlateNumberBox() {
    return browser.element('#editPlateNumber');
  }
  public clickDate(date) {
    browser.element(`//*[text()="${date}"]`).click();
  }
  public selectOption(name) {
    browser.element(`//*[text()="${name}"]`).click();
  }
  public get rentableItemDeleteBtn() {
    return browser.element('#rentableItemDeleteDeleteBtn');
  }
  public get rentableItemEditSaveBtn() {
    return browser.element('#rentableItemEditSaveBtn');
  }
  goToRentableItemsPage() {
    this.rentableItemDropdown();
    browser.pause(2000);
    this.rentableItemDropdownItemName('Lejelige ting').click();
    browser.pause(8000);
  }
  createRentableItem(brand: string, model: string, date: number, eFormName: string, serialNumber?: string,
                     VINNumber?: string, plateNumber?: string) {
    this.rentableItemCreateBtn.click();
    browser.pause(8000);
    this.rentableItemCreateBrandBox.addValue(brand);
    browser.pause(2000);
    this.rentableItemCreateModelBox.addValue(model);
    browser.pause(2000);
    this.eFormSelector.addValue(eFormName);
    browser.pause(2000);
    this.selectOption(eFormName);
    browser.pause(2000);
    this.rentableItemCreateSerialNumberBox.addValue(serialNumber);
    browser.pause(2000);
    this.rentableItemCreateVinNumberBox.addValue(VINNumber);
    browser.pause(2000);
    this.rentableItemCreatePlateNumberBox.addValue(plateNumber);
    browser.pause(2000);
    this.rentableItemCreateReistrationDate().click();
    browser.pause(3000);
    this.clickDate(date);
    browser.pause(3000);
    this.rentableItemCreateSaveBtn.click();
    browser.pause(6000);
    browser.refresh();
    browser.pause(14000);
  }
  deleteRentableItem() {
    const rentableItemForDelete = this.getFirstRowObject();
    rentableItemForDelete.deleteBtn.click();
    browser.pause(4000);
    this.rentableItemDeleteBtn.click();
    browser.pause(8000);
    browser.refresh();
  }
  editRentableItem(newBrand: string, newModel: string, newSerial: string, newVin: string, newPlate: string, newDate: number) {
    const rentableItemForEdit = this.getFirstRowObject();
    rentableItemForEdit.editBtn.click();
    browser.pause(8000);
    this.rentableItemEditBrandBox.clearElement();
    this.rentableItemEditBrandBox.addValue(newBrand);
    browser.pause(1000);
    this.rentableItemEditModelBox.clearElement();
    this.rentableItemEditModelBox.addValue(newModel);
    browser.pause(1000);
    this.rentableItemEditSerialNumberBox.clearElement();
    this.rentableItemEditSerialNumberBox.addValue(newSerial);
    browser.pause(1000);
    this.rentableItemEditVinNumberBox.clearElement();
    this.rentableItemEditVinNumberBox.addValue(newVin);
    browser.pause(1000);
    this.rentableItemEditPlateNumberBox.clearElement();
    this.rentableItemEditPlateNumberBox.addValue(newPlate);
    browser.pause(1000);
    this.rentableItemEditReistrationDate().click();
    browser.pause(3000);
    this.clickDate(newDate);
    browser.pause(3000);
    this.rentableItemEditSaveBtn.click();
    browser.pause(4000);
    browser.refresh();
  }
  cleanup() {
    const rentableItem = this.getFirstRowObject();
    if (rentableItem.deleteBtn.isVisible()) {
      rentableItem.deleteBtn.click();
      browser.pause(4000);
      this.rentableItemDeleteBtn.click();
      browser.pause(4000);
      browser.refresh();
      browser.pause(8000);
    }
  }
  getFirstRowObject(): RentableItemsRowObject {
    return new RentableItemsRowObject(1);
  }
  createNewEform(eFormLabel, newTagsList = [], tagAddedNum = 0) {
    this.newEformBtn.click();
    browser.pause(5000);
    // Create replaced xml and insert it in textarea
    const xml = XMLForEformRentableItems.XML.replace('TEST_LABEL', eFormLabel);
    browser.execute(function (xmlText) {
      (<HTMLInputElement>document.getElementById('eFormXml')).value = xmlText;
    }, xml);
    this.xmlTextArea.addValue(' ');
    // Create new tags
    const addedTags: string[] = newTagsList;
    if (newTagsList.length > 0) {
      this.createEformNewTagInput.setValue(newTagsList.join(','));
      browser.pause(5000);
    }
    // Add existing tags
    const selectedTags: string[] = [];
    if (tagAddedNum > 0) {
      browser.pause(5000);
      for (let i = 0; i < tagAddedNum; i++) {
        this.createEformTagSelector.click();
        const selectedTag = $('.ng-option:not(.ng-option-selected)');
        selectedTags.push(selectedTag.getText());
        console.log('selectedTags is ' + JSON.stringify(selectedTags));
        selectedTag.click();
        browser.pause(5000);
      }
    }
    this.createEformBtn.click();
    browser.pause(14000);
    return {added: addedTags, selected: selectedTags};
  }
}


const rentableItemsPage = new RentableItemRentableItemPage();
export default rentableItemsPage;

export class RentableItemsRowObject {
  constructor(rowNum) {
    if ($$('#rentableItemId')[rowNum - 1]) {
      this.id = +$$('#rentableItemId')[rowNum - 1];
      try {
        this.brand = $$('#rentableItemBrand')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.model = $$('#rentableItemModel')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.serialNumber = $$('#rentableItemSerialNumber')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.vinNumber = $$('#rentableItemVINNumber')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.plateNumber = $$('#rentableItemPlateNumber')[rowNum - 1].getText();
      } catch (e) {}
      try {
        this.registrationDate = $$('#rentableItemRegistrationDate')[rowNum - 1];
      } catch (e) {}
      try {
        this.deleteBtn = $$('#rentableItemDeleteBtn')[rowNum - 1];
      } catch (e) {}
      try {
        this.editBtn = $$('#rentableItemEditBtn')[rowNum - 1];
      } catch (e) {}
    }
  }
  id;
  model;
  brand;
  serialNumber;
  vinNumber;
  plateNumber;
  registrationDate;
  deleteBtn;
  editBtn;
}
