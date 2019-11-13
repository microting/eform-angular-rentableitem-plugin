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
    browser.pause(6000);
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
      vatNumber: '7945641'
    };
    browser.pause(2000);
    customersModalPage.createCustomer(customerObject);
  });
  it('should go to rentable items page', function () {
    rentableItemsPage.goToRentableItemsPage();
  });
  it('should create rentable item with all parameters', function () {
    const date = Math.floor((Math.random() * 28) + 1);
    const brand = 'Apple';
    const model = 'MacBook';
    const serialNumber = Guid.create().toString();
    const vinNumber = Guid.create().toString();
    const plateNumber = Guid.create().toString();
    rentableItemsPage.createRentableItem(brand, model, date, serialNumber, vinNumber, plateNumber);
  });
  it('should go to Contracts page', function () {
    contractsPage.rentableItemDropdown();
    browser.pause(2000);
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    browser.waitForVisible('#contractCreateBtn', 20000);
  });
  it('should create contract', function () {
    browser.pause(8000);
    const date1 = Math.floor((Math.random() * 14) + 1);
    const date2 = Math.floor((Math.random() * 28) + 1);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'MacBook';
    const customerName = 'Oles olie';
    contractsPage.createContract(date1, date2, contractNr, customerName, rentableItemName);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(contractNr);
    expect(contract.customerId).equal(1);
  });
});
