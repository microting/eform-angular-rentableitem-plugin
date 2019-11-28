import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import inspectionsPage from '../../../Page objects/rentableitem-general/rentable-item-Inspection.page';
import contractsPage from '../../../Page objects/rentableitem-general/rentable-item-Contract.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';

describe('Rentable Items - Inspections - Delete', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('should go to inspection page', function () {
    rentableItemsPage.rentableItemDropdown();
    browser.pause(1000);
    rentableItemsPage.rentableItemDropdownItemName('Inspections').click();
    browser.waitForVisible('#deleteInspectionBtn', 20000);
  });
  it('should NOT delete inspection', function () {
    const inspection = inspectionsPage.getFirstRowObject();
    inspection.deleteBtn.click();
    browser.pause(2000);
    inspectionsPage.contractInspectionDeleteCancelBtn.click();
    browser.refresh();
    browser.pause(8000);
    expect(inspectionsPage.rowNum === 1);
  });
  it('should delete inspection', function () {
    const inspection = inspectionsPage.getFirstRowObject();
    inspection.deleteBtn.click();
    browser.pause(2000);
    inspectionsPage.contractInspectionDeleteDeleteBtn.click();
    browser.refresh();
    browser.pause(8000);
    expect(inspectionsPage.rowNum === 0);
  });
});
