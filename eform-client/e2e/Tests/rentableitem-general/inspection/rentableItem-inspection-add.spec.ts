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
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should create an inspection', function () {
    loginPage.open('/');
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsPage.rentableItemDropdown();
    browser.pause(500);
    rentableItemsPage.rentableItemDropdownItemName('Kontrakter').click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    $('#contractCreateBtn').waitForDisplayed(20000);
    const contract = contractsPage.getFirstContractObject();
    contract.inspectionBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    inspectionsPage.siteSelectorBox.addValue('Alice');
    $('#spinner-animation').waitForDisplayed(90000, true);
    inspectionsPage.selectOption('Alice Springs');
    inspectionsPage.contractInspectionCreateSaveBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should verify the inspection is created', function () {
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsPage.rentableItemDropdown();
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsPage.rentableItemDropdownItemName('Inspections').click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    $('#deleteInspectionBtn').waitForDisplayed(20000);
    expect(inspectionsPage.rowNum).equal(1);
  });
});
