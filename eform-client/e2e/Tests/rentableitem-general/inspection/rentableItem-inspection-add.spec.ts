import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import inspectionsPage from '../../../Page objects/rentableitem-general/rentable-item-Inspection.page';
import deviceUsersPage, {DeviceUsersRowObject} from '../../../Page objects/DeviceUsers.page';
import pluginPage from '../../../Page objects/Plugin.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';
import rentableItemsSettingsPage from '../../../Page objects/rentableitem-general/rentable-item-Settings.page';
import contractsPage from '../../../Page objects/rentableitem-general/rentable-item-Contract.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';

describe('Rentable Items - Inspections - Add', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('should go to contracts page', function () {
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
    browser.pause(8000);
    browser.refresh();
    browser.pause(10000);
  });
  it('should create an inspection', function () {
    const contract = contractsPage.getFirstContractObject();
    contract.inspectionBtn.click();
    browser.pause(4000);
    inspectionsPage.siteSelectorBox.addValue('Alice');
    browser.pause(2000);
    inspectionsPage.selectOption('Alice Springs');
    inspectionsPage.contractInspectionCreateSaveBtn.click();
    browser.pause(10000);
  });
  it('should verify the inspection is created', function () {
    browser.pause(10000);
    rentableItemsPage.rentableItemDropdown();
    browser.pause(1000);
    rentableItemsPage.rentableItemDropdownItemName('Inspections').click();
    browser.waitForVisible('#deleteInspectionBtn', 20000);
    expect(inspectionsPage.rowNum).equal(1);
  });
});
