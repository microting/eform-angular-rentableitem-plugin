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
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    rentableItemsPage.rentableItemDropdownItemName('Inspections').click();
    $('#deleteInspectionBtn').waitForDisplayed({timeout: 20000});
  });
  it('should NOT delete inspection', function () {
    const inspection = inspectionsPage.getFirstRowObject();
    inspection.deleteBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    inspectionsPage.contractInspectionDeleteCancelBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    expect(inspectionsPage.rowNum === 1);
  });
  it('should delete inspection', function () {
    const inspection = inspectionsPage.getFirstRowObject();
    inspection.deleteBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    inspectionsPage.contractInspectionDeleteDeleteBtn.click();
    $('#spinner-animation').waitForDisplayed({timeout: 90000, reverse: true});
    expect(inspectionsPage.rowNum === 0);
  });
});
