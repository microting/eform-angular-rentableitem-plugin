import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';
import contractsPage from '../../../Page objects/rentableitem-general/rentable-item-Contract.page';
import customersPage from '../../../Page objects/Customers/Customers.page';
import customersModalPage from '../../../Page objects/Customers/CustomersModal.page';

describe('Rentable Items - Contracts - Delete', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
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
    browser.pause(4000);
    browser.refresh();
    browser.pause(8000);
  });
  it('should NOT delete contract', function () {
    contractsPage.deleteContractCancel();
    browser.pause(8000);
    const contract = contractsPage.getFirstContractObject();
    expect(contract != null);
    browser.pause(4000);
  });
  it('should delete contract', function () {
    contractsPage.deleteContract();
    browser.pause(8000);
    expect(contractsPage.rowNum).equal(0);
  });
});
