import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';
import contractsPage from '../../../Page objects/rentableitem-general/rentable-item-Contract.page';
import customersPage from '../../../Page objects/Customers/Customers.page';
import customersModalPage from '../../../Page objects/Customers/CustomersModal.page';

describe('Rentable Items - Contracts - edit', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('should go to customers page', function () {
    customersPage.goToCustomersPage();
  });
  it('should create multiple customers', function () {
    customersPage.newCustomerBtn.click();
    browser.pause(6000);
    const customerObject2 = {
      createdBy: 'John Smith',
      customerNo: '2',
      contactPerson: 'Jack Black',
      companyName: 'Bents bjelker',
      companyAddress: 'ABC Street 23',
      zipCode: '0215521',
      cityName: 'Odense',
      phone: '1231245',
      email: 'high@user.com',
      eanCode: '22221185',
      vatNumber: '79485641',
      countryCode: 'DK',
      cadastralNumber: 'eal10230',
      propertyNumber: 1235,
      apartmentNumber: 52,
      completionYear: 1960,
      floorsWithLivingSpace: 3
    };
    $('#spinner-animation').waitForDisplayed(90000, true);
    customersModalPage.createCustomer(customerObject2);
  });
  it('should go to rentable items page', function () {
    rentableItemsPage.goToRentableItemsPage();
  });
  it('should create rentable item with all parameters', function () {
    const date2 = Math.floor((Math.random() * 28) + 1);
    const brand2 = 'Bosch';
    const model2 = 'Boremaskine';
    const serialNumber2 = Guid.create().toString();
    const vinNumber2 = Guid.create().toString();
    const plateNumber2 = Guid.create().toString();
    const eForm = 'Number 1';
    rentableItemsPage.createRentableItem(brand2, model2, date2, eForm, serialNumber2, vinNumber2, plateNumber2);
  });
  it('should go to Contracts page', function () {
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed(90000, true);
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed(20000);
  });
  it('should create contract', function () {
    $('#spinner-animation').waitForDisplayed(90000, true);
    const date1 = Math.floor((Math.random() * 14) + 1);
    const date2 = Math.floor((Math.random() * 26) + 1);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'MacBook';
    const customerName = 'Oles olie';
    contractsPage.createContract(date1, date2, contractNr, customerName, rentableItemName);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(contractNr);
    expect(contract.customerId).equal(1);
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should edit contract', function () {
    $('#spinner-animation').waitForDisplayed(90000, true);
    const newStartDate = Math.floor((Math.random() * 14) + 1);
    const newEndDate = Math.floor((Math.random() * 26) + 1);
    const newContractNumber = Math.floor((Math.random() * 28) + 1);
    const newRentableItem = 'Boremaskine';
    const newCustomer = 'Bents bjelker';
    contractsPage.editContract(newStartDate, newEndDate, newContractNumber, newCustomer, newRentableItem);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(newContractNumber);
    expect(contract.customerId).equal(2);
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should NOT edit contract', function () {
    const oldContract = contractsPage.getFirstContractObject();
    const newStartDate = Math.floor((Math.random() * 14) + 1);
    const newEndDate = Math.floor((Math.random() * 28) + 1);
    const newContractNumber = Math.floor((Math.random() * 28) + 1);
    const newRentableItem = 'Boremaskine';
    const newCustomer = 'Bents bjelker';
    contractsPage.editContractCancel(newStartDate, newEndDate, newContractNumber, newCustomer, newRentableItem);
    $('#spinner-animation').waitForDisplayed(90000, true);
    const contract = contractsPage.getFirstContractObject();
    expect(oldContract.customerId).equal(contract.customerId);
    expect(oldContract.contractNumber).equal(contract.contractNumber);
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should remove 1 rentable item', function () {
    contractsPage.editContractDeleteRentableItem();
    $('#spinner-animation').waitForDisplayed(90000, true);
    const contract = contractsPage.getFirstContractObject();
    contract.editBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    expect(contractsPage.rentableItemListonContract).equal(1);
    contractsPage.contractEditCancelBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    contractsPage.cleanup();
  });
});
