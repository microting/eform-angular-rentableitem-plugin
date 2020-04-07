import loginPage from '../../Page objects/Login.page';
import myEformsPage from '../../Page objects/MyEforms.page';
import pluginPage from '../../Page objects/Plugin.page';

import {expect} from 'chai';
import pluginsPage from './application-settings.plugins.page';
import inspectionsPage from '../../Page objects/rentableitem-general/rentable-item-Inspection.page';
import {Guid} from 'guid-typescript';
import deviceUsersPage from '../../Page objects/DeviceUsers.page';
import rentableItemsSettingsPage from '../../Page objects/rentableitem-general/rentable-item-Settings.page';

describe('Application settings page - site header section', function () {
  before(function () {
    loginPage.open('/auth');
  });
  it('should go to plugin settings page', function () {
    loginPage.login();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#plugin-name').waitForDisplayed(50000);
    $('#spinner-animation').waitForDisplayed(90000, true);

    const plugin = pluginsPage.getFirstPluginRowObj();
    expect(plugin.id).equal(1);
    expect(plugin.name).equal('Microting Rentable Items plugin');
    expect(plugin.version).equal('1.0.0.0');
  });

  it('should activate the plugin', function () {
    const plugin = pluginsPage.getFirstPluginRowObj();
    expect(plugin.id).equal(1);
    expect(plugin.name).equal('Microting Rentable Items plugin');
    expect(plugin.version).equal('1.0.0.0');

    plugin.pluginSettingsBtn.click();
    $('#pluginOKBtn').waitForDisplayed(40000);
    pluginPage.pluginOKBtn.click();
    browser.pause(50000); // We need to wait 50 seconds for the plugin to create db etc.
    loginPage.open('/');

    loginPage.login();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#plugin-name').waitForDisplayed(50000);
    $('#spinner-animation').waitForDisplayed(90000, true);

    const plugin2 = pluginsPage.getSecondPluginRowObj();
    expect(plugin2.id).equal(2);
    expect(plugin2.name).equal('Microting Customers Plugin');
    expect(plugin2.version).equal('1.0.0.0');

    plugin2.pluginSettingsBtn.click();
    $('#pluginOKBtn').waitForDisplayed(40000);
    pluginPage.pluginOKBtn.click();
    browser.pause(50000); // We need to wait 50 seconds for the plugin to create db etc.
    loginPage.open('/');

    loginPage.login();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#plugin-name').waitForDisplayed(50000);
    $('#spinner-animation').waitForDisplayed(90000, true);
  });


  it('should create eform', function () {
    loginPage.open('/');
    const label = 'Number 1';
    $('#spinner-animation').waitForDisplayed(90000, true);
    inspectionsPage.createNewEform(label);
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
  it('should create a new device user', function () {
    myEformsPage.Navbar.goToDeviceUsersPage();
    $('#newDeviceUserBtn').waitForDisplayed(20000);
    const name = 'Alice';
    const surname = 'Springs';
    $('#spinner-animation').waitForDisplayed(90000, true);
    deviceUsersPage.createNewDeviceUser(name, surname);
  });
  it('should add eForm and device user to settings', function () {
    const deviceUser = deviceUsersPage.getDeviceUser(1);
    const sdkSiteId = deviceUser.siteId.getText();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    $('#plugin-name').waitForDisplayed(50000);
    $('#spinner-animation').waitForDisplayed(90000, true);
    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsSettingsPage.sdkSiteIdField.addValue(sdkSiteId);
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsSettingsPage.eFormSelector.addValue('Number 1');
    $('#spinner-animation').waitForDisplayed(90000, true);
    inspectionsPage.selectOption('Number 1');
    $('#spinner-animation').waitForDisplayed(90000, true);
    rentableItemsSettingsPage.saveBtn.click();
    $('#spinner-animation').waitForDisplayed(90000, true);
  });
});
