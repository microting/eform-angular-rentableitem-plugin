import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';
import contractsPage from '../../../Page objects/rentableitem-general/rentable-item-Contract.page';
import customersPage from '../../../Page objects/Customers/Customers.page';
import customersModalPage from '../../../Page objects/Customers/CustomersModal.page';

describe('Rentable Items - Contracts - add', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  // it('should go to customers page', function () {
  //   customersPage.goToCustomersPage();
  // });
  // it('should create a customer', function () {
  //   customersPage.newCustomerBtn.click();
  //   $('#spinner-animation').waitForDisplayed(90000, true);
  //   const customerObject = {
  //     createdBy: 'John Smith',
  //     customerNo: '1',
  //     contactPerson: 'Samantha Black',
  //     companyName: 'Oles olie',
  //     companyAddress: 'ABC Street 22',
  //     zipCode: '021551',
  //     cityName: 'Odense',
  //     phone: '123124',
  //     email: 'user@user.com',
  //     eanCode: '2222115',
  //     vatNumber: '7945641',
  //     countryCode: 'DK',
  //     cadastralNumber: 'eal10230',
  //     propertyNumber: 1235,
  //     apartmentNumber: 52,
  //     completionYear: 1960,
  //     floorsWithLivingSpace: 3
  //   };
  //   $('#spinner-animation').waitForDisplayed(90000, true);
  //   customersModalPage.createCustomer(customerObject);
  // });
  it('should create rentable item with all parameters', function () {
    rentableItemsPage.goToRentableItemsPage();
    const date = Math.floor((Math.random() * 28) + 1);
    const brand = 'Apple';
    const model = 'MacBook';
    const serialNumber = Guid.create().toString();
    const vinNumber = Guid.create().toString();
    const plateNumber = Guid.create().toString();
    const eForm = 'Number 1';
    rentableItemsPage.createRentableItem(brand, model, date, eForm, serialNumber, vinNumber, plateNumber);
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
    const date2 = Math.floor((Math.random() * 28) + 1);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'MacBook';
    const customerName = 'Oles olie';
    contractsPage.createContract(date1, date2, contractNr, customerName, rentableItemName);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(contractNr);
    expect(contract.customerId).equal(1);
    $('#spinner-animation').waitForDisplayed(90000, true);
    contractsPage.cleanup();
  });
  it('should NOT create contract', function () {
    $('#spinner-animation').waitForDisplayed(90000, true);
    const date1 = Math.floor((Math.random() * 14) + 1);
    const date2 = Math.floor((Math.random() * 28) + 1);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'MacBook';
    const customerName = 'Oles olie';
    contractsPage.createContractCancel(date1, date2, contractNr, customerName, rentableItemName);
    $('#spinner-animation').waitForDisplayed(90000, true);
    expect(contractsPage.rowNum).equal(0);
  });
});
