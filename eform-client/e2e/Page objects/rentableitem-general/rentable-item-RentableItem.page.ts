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
        browser.pause(500);
        return $$('#tableBody > tr').length;
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
    public get rentableItemDropdownName() {
        return $(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).element('..');
    }
    public rentableItemDropdown() {
        $(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
    }
    public rentableItemDropdownItemName(name) {
        return $(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
    }
    public get rentableItemCreateBtn() {
        $('#rentableItemCreateBtn').waitForDisplayed(20000);
        $('#rentableItemCreateBtn').waitForClickable({timeout: 20000});
        return $('#rentableItemCreateBtn');
    }
    public getBtnTxt(text: string) {
        return $(`//*[contains(@class, 'p-3')]//*[text()="${text}"]`);
    }
    public get rentableItemCreateBrandBox() {
        $('#createBrand').waitForDisplayed(20000);
        $('#createBrand').waitForClickable({timeout: 20000});
        return $('#createBrand');
    }
    public get rentableItemCreateModelBox() {
        $('#createRentableItemModel').waitForDisplayed(20000);
        $('#createRentableItemModel').waitForClickable({timeout: 20000});
        return $('#createRentableItemModel');
    }
    public rentableItemCreateReistrationDate() {
        return $(`//*[contains(@class, 'form')]//*[contains(text(), 'Registration date')]`);
    }
    public get rentableItemCreateVinNumberBox() {
        $('#createVINNumber').waitForDisplayed(20000);
        $('#createVINNumber').waitForClickable({timeout: 20000});
        return $('#createVINNumber');
    }
    public get rentableItemCreateSerialNumberBox() {
        $('#createSerialNumber').waitForDisplayed(20000);
        $('#createSerialNumber').waitForClickable({timeout: 20000});
        return $('#createSerialNumber');
    }
    public get rentableItemCreatePlateNumberBox() {
        $('#createPlateNumber').waitForDisplayed(20000);
        $('#createPlateNumber').waitForClickable({timeout: 20000});
        return $('#createPlateNumber');
    }
    public get eFormSelector() {
        return $(`//*[@id= 'eFormId']//input`);
    }
    public get rentableItemCreateSaveBtn() {
        $('#rentableItemCreateSaveBtn').waitForDisplayed(20000);
        $('#rentableItemCreateSaveBtn').waitForClickable({timeout: 20000});
        return $('#rentableItemCreateSaveBtn');
    }
    public get rentableItemEditBrandBox() {
        $('#editBrand').waitForDisplayed(20000);
        $('#editBrand').waitForClickable({timeout: 20000});
        return $('#editBrand');
    }
    public get rentableItemEditModelBox() {
        $('#editRentableItemModel').waitForDisplayed(20000);
        $('#editRentableItemModel').waitForClickable({timeout: 20000});
        return $('#editRentableItemModel');
    }
    public rentableItemEditReistrationDate() {
        // return $(`//*[contains(@class, 'form')]//*[contains(text(), 'Registration date')]`);
        $('#editRegistrationDate').waitForDisplayed(20000);
        $('#editRegistrationDate').waitForClickable({timeout: 20000});
        return $('#editRegistrationDate');
    }
    public get rentableItemEditVinNumberBox() {
        $('#editVINNumber').waitForDisplayed(20000);
        $('#editVINNumber').waitForClickable({timeout: 20000});
        return $('#editVINNumber');
    }
    public get rentableItemEditSerialNumberBox() {
        $('#editSerialNumber').waitForDisplayed(20000);
        $('#editSerialNumber').waitForClickable({timeout: 20000});
        return $('#editSerialNumber');
    }
    public get rentableItemEditPlateNumberBox() {
        $('#editPlateNumber').waitForDisplayed(20000);
        $('#editPlateNumber').waitForClickable({timeout: 20000});
        return $('#editPlateNumber');
    }
    public clickDate(date) {
        $(`//*[text()="${date}"]`).click();
    }
    public selectOption(name) {
        $(`//*[text()="${name}"]`).click();
    }
    public get rentableItemDeleteBtn() {
        $('#rentableItemDeleteDeleteBtn').waitForDisplayed(20000);
        $('#rentableItemDeleteDeleteBtn').waitForClickable({timeout: 20000});
        return $('#rentableItemDeleteDeleteBtn');
    }
    public get rentableItemEditSaveBtn() {
        $('#rentableItemEditSaveBtn').waitForDisplayed(20000);
        $('#rentableItemEditSaveBtn').waitForClickable({timeout: 20000});
        return $('#rentableItemEditSaveBtn');
    }
    goToRentableItemsPage() {
        this.rentableItemDropdown();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemDropdownItemName('Lejelige ting').click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    createRentableItem(brand: string, model: string, date: number, eFormName: string, serialNumber?: string,
                       VINNumber?: string, plateNumber?: string) {
        this.rentableItemCreateBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateBrandBox.addValue(brand);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateModelBox.addValue(model);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.eFormSelector.addValue(eFormName);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(eFormName);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateSerialNumberBox.addValue(serialNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateVinNumberBox.addValue(VINNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreatePlateNumberBox.addValue(plateNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateReistrationDate().click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(date);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemCreateSaveBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    deleteRentableItem() {
        const rentableItemForDelete = this.getFirstRowObject();
        rentableItemForDelete.deleteBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemDeleteBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    editRentableItem(newBrand: string, newModel: string, newSerial: string, newVin: string, newPlate: string, newDate: number) {
        const rentableItemForEdit = this.getFirstRowObject();
        rentableItemForEdit.editBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditBrandBox.clearElement();
        this.rentableItemEditBrandBox.addValue(newBrand);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditModelBox.clearElement();
        this.rentableItemEditModelBox.addValue(newModel);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditSerialNumberBox.clearElement();
        this.rentableItemEditSerialNumberBox.addValue(newSerial);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditVinNumberBox.clearElement();
        this.rentableItemEditVinNumberBox.addValue(newVin);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditPlateNumberBox.clearElement();
        this.rentableItemEditPlateNumberBox.addValue(newPlate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditReistrationDate().click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(newDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemEditSaveBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    cleanup() {
        const rentableItem = this.getFirstRowObject();
        if (rentableItem.deleteBtn.isVisible()) {
            rentableItem.deleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
            this.rentableItemDeleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
        }
    }
    getFirstRowObject(): RentableItemsRowObject {
        return new RentableItemsRowObject(1);
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
