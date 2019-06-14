import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEformRentableItems';

export class RentableItemRentableItemPage extends Page{
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
    return browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Rentable Items')]`).element('..');
  }
  public rentableItemDropdown() {
    browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Rentable Items')]`).click();
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
  public get rentableItemCreateSaveBtn() {
    return browser.element('#rentableItemCreateSaveBtn');
  }
  public clickDate(date) {
    browser.element(`//*[text()="${date}"]`).click();
  }
  goToRentableItemsPage() {
    this.rentableItemDropdown();
    browser.pause(2000);
    this.rentableItemDropdownItemName('Rentable Items').click();
    browser.pause(8000);
  }
  createRentableItem(brand: string, model: string, date: number, serialNumber?: string,  VINNumber?: string, plateNumber?: string) {
    this.rentableItemCreateBtn.click();
    browser.pause(8000);
    this.rentableItemCreateBrandBox.addValue(brand);
    browser.pause(2000);
    this.rentableItemCreateModelBox.addValue(model);
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
    }
  }
  id;
  model;
  brand;
  serialNumber;
  vinNumber;
  plateNumber;
  registrationDate;
}
