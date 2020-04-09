import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';


describe('Rentable Item Plugin - Rentable Item', function () {
  before(function () {
    loginPage.open('/auth');
    loginPage.login();
  });
  it('should create rentable item with all parameters', function () {
    rentableItemsPage.goToRentableItemsPage();
    rentableItemsPage.getBtnTxt('New rentable item');
    const date = Math.floor((Math.random() * 28) + 1);
    const brand = Guid.create().toString();
    const model = Guid.create().toString();
    const serialNumber = Guid.create().toString();
    const vinNumber = Guid.create().toString();
    const plateNumber = Guid.create().toString();
    const eForm = 'Number 1';
    rentableItemsPage.createRentableItem(brand, model, date, eForm, serialNumber, vinNumber, plateNumber);
    const rentableItem = rentableItemsPage.getFirstRowObject();
    // expect(rentableItem.registrationDate).equal(date);
    expect(rentableItem.brand).equal(brand);
    expect(rentableItem.model).equal(model);
    expect(rentableItem.plateNumber).equal(plateNumber);
    expect(rentableItem.vinNumber).equal(vinNumber);
    expect(rentableItem.serialNumber).equal(serialNumber);
  });
  it('should edit Rentable Item', function () {
    loginPage.open('/');
    rentableItemsPage.goToRentableItemsPage();
    const newBrand = Guid.create().toString();
    const newModel = Guid.create().toString();
    const newPlate = Guid.create().toString();
    const newVin = Guid.create().toString();
    const newSerial = Guid.create().toString();
    const newDate = Math.floor((Math.random() * 28) + 1);
    rentableItemsPage.editRentableItem(newBrand, newModel, newSerial, newVin, newPlate, newDate);
    $('#spinner-animation').waitForDisplayed(90000, true);
    const rentableItem = rentableItemsPage.getFirstRowObject();
    expect(rentableItem.brand).equals(newBrand);
    expect(rentableItem.model).equals(newModel);
    expect(rentableItem.serialNumber).equals(newSerial);
    expect(rentableItem.vinNumber).equals(newVin);
    expect(rentableItem.plateNumber).equals(newPlate);
    rentableItemsPage.cleanup();
  });
});
