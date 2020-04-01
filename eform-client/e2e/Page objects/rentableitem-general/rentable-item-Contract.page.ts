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
        return $(`//*[contains(@class, 'dropdown')]//div//*[contains(text(), "${name}")]`);
    }
    public rentableItemDropdown() {
        $(`//*[contains(@class, 'dropdown')]//*[contains(text(), 'Lejelige ting')]`).click();
    }
    public get newContractBtn() {
        $('#contractCreateBtn').waitForDisplayed(20000);
        $('#contractCreateBtn').waitForClickable({timeout: 20000});
        return $('#contractCreateBtn');
    }
    public get startDateSelector() {
        $('#startDate').waitForDisplayed(20000);
        $('#startDate').waitForClickable({timeout: 20000});
        return $('#startDate');
    }
    public get editStartDateSelector() {
        $('#editStartDate').waitForDisplayed(20000);
        $('#editStartDate').waitForClickable({timeout: 20000});
        return $('#editStartDate');
    }
    public get endDateSelector() {
        $('#endDate').waitForDisplayed(20000);
        $('#endDate').waitForClickable({timeout: 20000});
        return $('#endDate');
    }
    public get editEndDateSelector() {
        $('#editEndDate').waitForDisplayed(20000);
        $('#editEndDate').waitForClickable({timeout: 20000});
        return $('#editEndDate');
    }
    public get contractNumberField() {
        $('#contractNumber').waitForDisplayed(20000);
        $('#contractNumber').waitForClickable({timeout: 20000});
        return $('#contractNumber');
    }
    public get editContractNumberField() {
        $('#editContractNr').waitForDisplayed(20000);
        $('#editContractNr').waitForClickable({timeout: 20000});
        return $('#editContractNr');
    }
    public get customerSelector() {
        return $(`//*[@id= 'customerSelector']//input`);
    }
    public get editCustomerSelector() {
        return $(`//*[@id= 'editCustomerIdSelector']//input`);
    }
    public get rentableItemSelector() {
        return $(`//*[@id= 'rentableItemSelector']//input`);
    }
    public get editRentableItemSelector() {
        return $(`//*[@id= 'editRentableItemSelector']//input`);
    }
    public get contractCreateSaveBtn() {
        $('#contractCreateSaveBtn').waitForDisplayed(20000);
        $('#contractCreateSaveBtn').waitForClickable({timeout: 20000});
        return $('#contractCreateSaveBtn');
    }
    public get contractCreateCancelBtn() {
        $('#contractCreateDeleteBtn').waitForDisplayed(20000);
        $('#contractCreateDeleteBtn').waitForClickable({timeout: 20000});
        return $('#contractCreateDeleteBtn');
    }
    public get contractEditSaveBtn() {
        $('#contractEditSaveBtn').waitForDisplayed(20000);
        $('#contractEditSaveBtn').waitForClickable({timeout: 20000});
        return $('#contractEditSaveBtn');
    }
    public get contractEditCancelBtn() {
        $('#contractEditCancelBtn').waitForDisplayed(20000);
        $('#contractEditCancelBtn').waitForClickable({timeout: 20000});
        return $('#contractEditCancelBtn');
    }
    public get selectedContractId() {
        $('#selectedContractId').waitForDisplayed(20000);
        $('#selectedContractId').waitForClickable({timeout: 20000});
        return $('#selectedContractId');
    }
    public get selectedContractNumber() {
        $('#selectedContractNr').waitForDisplayed(20000);
        $('#selectedContractNr').waitForClickable({timeout: 20000});
        return $('#selectedContractNr');
    }
    public get selectedContractCustomerId() {
        $('#selectedContractCustomerId').waitForDisplayed(20000);
        $('#selectedContractCustomerId').waitForClickable({timeout: 20000});
        return $('#selectedContractCustomerId');
    }
    public get contractDeleteDeleteBtn() {
        $('#contractDeleteDeleteBtn').waitForDisplayed(20000);
        $('#contractDeleteDeleteBtn').waitForClickable({timeout: 20000});
        return $('#contractDeleteDeleteBtn');
    }
    public get contractDeleteCancelBtn() {
        $('#contractDeleteCancelBtn').waitForDisplayed(20000);
        $('#contractDeleteCancelBtn').waitForClickable({timeout: 20000});
        return $('#contractDeleteCancelBtn');
    }
    public clickDate(date) {
        $(`//*[text()="${date}"]`).click();
    }
    public selectOption(name) {
        $(`//*[text()="${name}"]`).click();
    }
    public createContract(startDate: number, endDate: number, contractNumber: number, customer: string, rentableItem: string) {
        this.newContractBtn.click();
        $('#startDate').waitForDisplayed(20000);
        this.startDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(startDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.endDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(endDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractNumberField.addValue(contractNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.customerSelector.addValue(customer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(customer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemSelector.addValue(rentableItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(rentableItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractCreateSaveBtn.click();
    }
    public createContractCancel(startDate: number, endDate: number, contractNumber: number, customer: string, rentableItem: string) {
        this.newContractBtn.click();
        $('#startDate').waitForDisplayed(20000);
        this.startDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(startDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.endDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(endDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractNumberField.addValue(contractNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.customerSelector.addValue(customer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(customer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.rentableItemSelector.addValue(rentableItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(rentableItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractCreateCancelBtn.click();
    }

    public editContract(startDate: number, endDate: number, contractNumber: number, newCustomer: string, newItem: string) {
        const contractForEdit = this.getFirstContractObject();
        contractForEdit.editBtn.click();
        $('#editStartDate').waitForDisplayed(20000);
        this.editStartDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(startDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editEndDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(endDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editContractNumberField.clearElement();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editContractNumberField.addValue(contractNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editCustomerSelector.addValue(newCustomer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(newCustomer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editRentableItemSelector.addValue(newItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(newItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractEditSaveBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    public editContractCancel(startDate: number, endDate: number, contractNumber: number, newCustomer: string, newItem: string) {
        const contractForEdit = this.getFirstContractObject();
        contractForEdit.editBtn.click();
        $('#editStartDate').waitForDisplayed(20000);
        this.editStartDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(startDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editEndDateSelector.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.clickDate(endDate);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editContractNumberField.clearElement();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editContractNumberField.addValue(contractNumber);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editCustomerSelector.addValue(newCustomer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(newCustomer);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.editRentableItemSelector.addValue(newItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.selectOption(newItem);
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractEditCancelBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    public editContractDeleteRentableItem() {
        const contractForEdit = this.getFirstContractObject();
        contractForEdit.editBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        const rentableItemToDelete = this.getFirstRentableItemObject();
        rentableItemToDelete.deleteBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
        this.contractEditSaveBtn.click();
        $('#spinner-animation').waitForDisplayed(90000, true);
    }
    public deleteContract() {
        const deleteObject = this.getFirstContractObject();
        if (deleteObject != null) {
            $('#spinner-animation').waitForDisplayed(90000, true);
            deleteObject.deleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
            this.contractDeleteDeleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
        }
    }
    public deleteContractCancel() {
        const deleteObject = this.getFirstContractObject();
        if (deleteObject != null) {
            $('#spinner-animation').waitForDisplayed(90000, true);
            deleteObject.deleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
            this.contractDeleteCancelBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
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
            $('#spinner-animation').waitForDisplayed(90000, true);
            deleteObject.deleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
            this.contractDeleteDeleteBtn.click();
            $('#spinner-animation').waitForDisplayed(90000, true);
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
