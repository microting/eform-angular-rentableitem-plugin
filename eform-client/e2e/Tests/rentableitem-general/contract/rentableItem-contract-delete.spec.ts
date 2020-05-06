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
  // it('should go to Contracts page', function () {
  //   contractsPage.rentableItemDropdown();
  //   $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
  //   contractsPage.rentableItemDropdownItemName('Kontrakter').click();
  //   $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
  // });
  it('should create contract', function () {
    //loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const date1 = Math.floor((Math.random() * 14) + 1);
    const date2 = Math.floor((Math.random() * 28) + 1);
    const contractNr = Math.floor((Math.random() * 100) + 1);
    const rentableItemName = 'MacBook';
    const customerName = 'Oles olie';
    contractsPage.createContract(date1, date2, contractNr, customerName, rentableItemName);
    const contract = contractsPage.getFirstContractObject();
    expect(contract.contractNumber).equal(contractNr);
    expect($('#contractCustomer').getText()).equal('Oles olie\nSamantha Black');
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
  });
  it('should NOT delete contract', function () {
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    contractsPage.deleteContractCancel();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    const contract = contractsPage.getFirstContractObject();
    expect(contract != null);
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
  });
  it('should delete contract', function () {
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    contractsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#contractCreateBtn').waitForDisplayed({timeout: 20000});
    contractsPage.deleteContract();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    loginPage.open('/');
    contractsPage.rentableItemDropdown();
    expect(contractsPage.rowNum).equal(0);
  });
});
