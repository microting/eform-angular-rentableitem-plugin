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
  // it('should create eForm', function () {
  //   const  neweFormLabel = 'Number 1';
  //   rentableItemsPage.createNewEform(neweFormLabel);
  // });
  it('should check if activated', function () {
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.pause(8000);
    const plugin = pluginsPage.getFirstPluginRowObj();
    expect(plugin.id).equal(1);
    expect(plugin.name).equal('Microting Rentable Items plugin');
    expect(plugin.version).equal('1.0.0.0');
    expect(plugin.status).equal('Aktiveret');
  });
  it('should check if menu point is there', function () {
    expect(rentableItemsPage.rentableItemDropdownName.getText()).equal('Lejelige ting');
    rentableItemsPage.rentableItemDropdown();
    browser.pause(4000);
    expect(rentableItemsPage.rentableItemDropdownItemName('Lejelige ting').getText()).equal('Lejelige ting');
    browser.pause(4000);
    rentableItemsPage.rentableItemDropdown();
    browser.refresh();
  });
  it('should get btn text', function () {
    rentableItemsPage.goToRentableItemsPage();
    rentableItemsPage.getBtnTxt('New rentable item');
  });
  it('should create rentable item with all parameters', function () {
    const date = Math.floor((Math.random() * 28) + 1);
    const brand = Guid.create().toString();
    const model = Guid.create().toString();
    const serialNumber = Guid.create().toString();
    const vinNumber = Guid.create().toString();
    const plateNumber = Guid.create().toString();
    rentableItemsPage.createRentableItem(brand, model, date, serialNumber, vinNumber, plateNumber);
    const rentableItem = rentableItemsPage.getFirstRowObject();
    // expect(rentableItem.registrationDate).equal(date);
    expect(rentableItem.brand).equal(brand);
    expect(rentableItem.model).equal(model);
    expect(rentableItem.plateNumber).equal(plateNumber);
    expect(rentableItem.vinNumber).equal(vinNumber);
    expect(rentableItem.serialNumber).equal(serialNumber);
  });
  it('should delete Rentable Item', function () {
    rentableItemsPage.deleteRentableItem();
    browser.pause(8000);
    const rentableItem = rentableItemsPage.getFirstRowObject();
    expect(rentableItem.id === null);
  });
});
