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
  it('should go to customers page', function () {
    customersPage.goToCustomersPage();
  });
  it('should create a customer', function () {
    customersPage.newCustomerBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const customerObject = {
      createdBy: 'John Smith',
      customerNo: '1',
      contactPerson: 'Samantha Black',
      companyName: 'Oles olie',
      companyAddress: 'ABC Street 22',
      zipCode: '021551',
      cityName: 'Odense',
      phone: '123124',
      email: 'user@user.com',
      eanCode: '2222115',
      vatNumber: '7945641',
      countryCode: 'DK',
      cadastralNumber: 'eal10230',
      propertyNumber: 1235,
      apartmentNumber: 52,
      completionYear: 1960,
      floorsWithLivingSpace: 3
    };
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    customersModalPage.createCustomer(customerObject);
  });
  it('should create rentable item with all parameters', function () {
    rentableItemsPage.goToRentableItemsPage();
    const date = Math.floor((Math.random() * 28) + 1);
    const brand = 'Apple';
    const model = 'MacBook';
    const serialNumber = 'gfoijwf235';
    const vinNumber = 'ihu3t98wrgio34t';
    const plateNumber = '9et9w90342wgr';
    const eForm = 'Number 1';
    rentableItemsPage.createRentableItem(brand, model, date, eForm, serialNumber, vinNumber, plateNumber);
  });
  it('should create contract', function () {
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const date1 = loginPage.randomInt(12, 24);
    const date2 = loginPage.randomInt(12, 24);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'Apple';
    const expectrentableItemName = 'Apple - MacBook - gfoijwf235 - ihu3t98wrgio34t - 9et9w90342wgr';
    const customerName = 'Oles olie';
    const expectcustomerName = 'Oles olie - Samantha Black - ABC Street 22 - Odense - 123124';
    contractsPage.createContract(date1, date2, contractNr, customerName, rentableItemName, expectcustomerName, expectrentableItemName);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(contractNr);
    expect($('#contractCustomer').getText()).equal('Oles olie\nSamantha Black');
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.cleanup();
  });
  it('should NOT create contract', function () {
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const date1 = loginPage.randomInt(12, 24);
    const date2 = loginPage.randomInt(12, 24);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'Apple';
    const expectrentableItemName = 'Apple - MacBook - gfoijwf235 - ihu3t98wrgio34t - 9et9w90342wgr';
    const customerName = 'Oles olie';
    const expectcustomerName = 'Oles olie - Samantha Black - ABC Street 22 - Odense - 123124';
    contractsPage.createContractCancel(date1, date2, contractNr, customerName, rentableItemName, expectcustomerName, expectrentableItemName);
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    expect(contractsPage.rowNum).equal(0);
  });
});
