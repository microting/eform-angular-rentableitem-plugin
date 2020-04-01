import {expect} from 'chai';
import loginPage from '../../../Page objects/Login.page';
import {Guid} from 'guid-typescript';
import myEformsPage from '../../../Page objects/MyEforms.page';
import rentableItemsPage from '../../../Page objects/rentableitem-general/rentable-item-RentableItem.page';
import pluginsPage from '../../rentableitem-settings/application-settings.plugins.page';
import {log} from 'util';

describe('Rentable Item Plugin - Rentable Item', function () {
    before(function () {
        loginPage.open('/auth');
        loginPage.login();
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
        const eForm = 'Number 1';
        rentableItemsPage.createRentableItem(brand, model, date, eForm, serialNumber, vinNumber, plateNumber);
        const rentableItem = rentableItemsPage.getFirstRowObject();
        // expect(rentableItem.registrationDate).equal(date);
        expect(rentableItem.brand).equal(brand);
        expect(rentableItem.model).equal(model);
        expect(rentableItem.plateNumber).equal(plateNumber);
        expect(rentableItem.vinNumber).equal(vinNumber);
        expect(rentableItem.serialNumber).equal(serialNumber);
        rentableItemsPage.cleanup();
    });
    it('should create rentable item with only 5 parameters', function () {
        const date = Math.floor((Math.random() * 28) + 1);
        const brand = Guid.create().toString();
        const model = Guid.create().toString();
        const serialNumber = Guid.create().toString();
        const vinNumber = Guid.create().toString();
        const eForm = 'Number 1';
        rentableItemsPage.createRentableItem(brand,  model,  date, eForm, serialNumber, vinNumber, '');
        const rentableItem = rentableItemsPage.getFirstRowObject();
        expect(rentableItem.brand).equal(brand);
        expect(rentableItem.model).equal(model);
        expect(rentableItem.vinNumber).equal(vinNumber);
        expect(rentableItem.serialNumber).equal(serialNumber);
        rentableItemsPage.cleanup();
    });
    it('should create rentable item with only 4 parameters, using serialNumber', function () {
        const date = Math.floor((Math.random() * 28) + 1);
        const brand = Guid.create().toString();
        const model = Guid.create().toString();
        const serialNumber = Guid.create().toString();
        const eForm = 'Number 1';
        rentableItemsPage.createRentableItem(brand,  model,  date, eForm,  serialNumber, '', '');
        const rentableItem = rentableItemsPage.getFirstRowObject();
        expect(rentableItem.brand).equal(brand);
        expect(rentableItem.model).equal(model);
        expect(rentableItem.serialNumber).equal(serialNumber);
        rentableItemsPage.cleanup();
    });
    it('should create rentable item with only 4 parameters, using VINnumber', function () {
        const date = Math.floor((Math.random() * 28) + 1);
        const brand = Guid.create().toString();
        const model = Guid.create().toString();
        const vinNumber = Guid.create().toString();
        const eForm = 'Number 1';
        rentableItemsPage.createRentableItem(brand,  model,  date, eForm,  '', vinNumber, '');
        const rentableItem = rentableItemsPage.getFirstRowObject();
        expect(rentableItem.brand).equal(brand);
        expect(rentableItem.model).equal(model);
        expect(rentableItem.vinNumber).equal(vinNumber);
        rentableItemsPage.cleanup();
    });
});
