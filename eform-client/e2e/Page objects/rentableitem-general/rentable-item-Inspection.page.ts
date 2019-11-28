import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEform';
import {trackByHourSegment} from 'angular-calendar/modules/common/util';
import {until} from 'selenium-webdriver';

export class RentableItemInspectionPage extends Page {
  constructor() {
    super();
  }
  public get rowNum(): number {
    return $$(`//*[@id= 'tableBody']//tr`).length;
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
  public get siteSelectorBox() {
    return browser.element(`//*[@id = 'siteSelector']//input`);
  }
  public get contractInspectionCreateSaveBtn() {
    return browser.element('#contractInspectionCreateSaveBtn');
  }
  public get contractInspectionDeleteDeleteBtn() {
    return browser.element('#inspectionDeleteDeleteBtn');
  }
  public get contractInspectionDeleteCancelBtn() {
    return browser.element('#inspectionDeleteCancelBtn');
  }

  public rentableItemDropdownItemName(name) {
    return browser.element(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
  }
  public rentableItemDropdown() {
    browser.element(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
  }
  public selectOption(name) {
    browser.element(`//*[text()="${name}"]`).click();
  }
  public getFirstRowObject(): InspectionRowObject {
    return new InspectionRowObject(1);
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

const inspectionsPage = new RentableItemInspectionPage();
export default inspectionsPage;

export class InspectionRowObject {
  constructor(rowNum) {
    if ($$('#inspectionId')[rowNum - 1]) {
      this.id = $$('#inspectionId')[rowNum - 1];
      try {
        this.contractId = $$('#inspectionContractId')[rowNum - 1];
      } catch (e) {}
      try {
        this.sdkCaseID = $$('#inspectionSDKCaseId')[rowNum - 1];
      } catch (e) {}
      try {
        this.status = $$('#inspectionStatus')[rowNum - 1];
      } catch (e) {}
      try {
        this.doneAt = $$('#inspectionDoneAt')[rowNum - 1];
      } catch (e) {}
      try {
        this.deleteBtn = $$('#deleteInspectionBtn')[rowNum - 1];
      } catch (e) {}
    }
  }
  id;
  contractId;
  sdkCaseID;
  status;
  doneAt;
  deleteBtn;
}
