import Page from '../Page';
import XMLForEformRentableItems from '../../Constants/XMLForEform';
import {trackByHourSegment} from 'angular-calendar/modules/common/util';

export class RentableItemInspectionPage extends Page {
  constructor() {
    super();
  }
  public get rowNum(): number {
    browser.pause(500);
    return $$(`//*[@id= 'tableBody']//tr`).length;
  }
  public get newEformBtn() {
    $('#newEFormBtn').waitForDisplayed(20000);
    $('#newEFormBtn').waitForClickable({timeout: 20000});
    return $('#newEFormBtn');
  }
  public get xmlTextArea() {
    $('#eFormXml').waitForDisplayed(20000);
    $('#eFormXml').waitForClickable({timeout: 20000});
    return $('#eFormXml');
  }
  public get createEformBtn() {
    $('#createEformBtn').waitForDisplayed(20000);
    $('#createEformBtn').waitForClickable({timeout: 20000});
    return $('#createEformBtn');
  }
  public get createEformTagSelector() {
    $('#createEFormMultiSelector').waitForDisplayed(20000);
    $('#createEFormMultiSelector').waitForClickable({timeout: 20000});
    return $('#createEFormMultiSelector');
  }
  public get createEformNewTagInput() {
    $('#addTagInput').waitForDisplayed(20000);
    $('#addTagInput').waitForClickable({timeout: 20000});
    return $('#addTagInput');
  }
  public get siteSelectorBox() {
    return $(`//*[@id = 'siteSelector']//input`);
  }
  public get contractInspectionCreateSaveBtn() {
    $('#contractInspectionCreateSaveBtn').waitForDisplayed(20000);
    $('#contractInspectionCreateSaveBtn').waitForClickable({timeout: 20000});
    return $('#contractInspectionCreateSaveBtn');
  }
  public get contractInspectionDeleteDeleteBtn() {
    $('#inspectionDeleteDeleteBtn').waitForDisplayed(20000);
    $('#inspectionDeleteDeleteBtn').waitForClickable({timeout: 20000});
    return $('#inspectionDeleteDeleteBtn');
  }
  public get contractInspectionDeleteCancelBtn() {
    $('#inspectionDeleteCancelBtn').waitForDisplayed(20000);
    $('#inspectionDeleteCancelBtn').waitForClickable({timeout: 20000});
    return $('#inspectionDeleteCancelBtn');
  }

  public rentableItemDropdownItemName(name) {
    return $(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
  }
  public rentableItemDropdown() {
    $(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
  }
  public selectOption(name) {
    $(`//*[text()="${name}"]`).click();
  }
  public getFirstRowObject(): InspectionRowObject {
    return new InspectionRowObject(1);
  }





  createNewEform(eFormLabel, newTagsList = [], tagAddedNum = 0) {
    this.newEformBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
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
      $('#spinner-animation').waitForDisplayed(90000, true);
    }
    // Add existing tags
    const selectedTags: string[] = [];
    if (tagAddedNum > 0) {
      $('#spinner-animation').waitForDisplayed(90000, true);
      for (let i = 0; i < tagAddedNum; i++) {
        this.createEformTagSelector.click();
        const selectedTag = $('.ng-option:not(.ng-option-selected)');
        selectedTags.push(selectedTag.getText());
        console.log('selectedTags is ' + JSON.stringify(selectedTags));
        selectedTag.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
      }
    }
    this.createEformBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
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
