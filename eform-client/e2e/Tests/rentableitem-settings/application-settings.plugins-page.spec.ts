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
    browser.waitForExist('#plugin-name', 50000);
    browser.pause(20000);

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
    browser.waitForVisible('#pluginOKBtn', 40000);
    pluginPage.pluginOKBtn.click();
    browser.pause(50000); // We need to wait 50 seconds for the plugin to create db etc.
    browser.refresh();

    loginPage.login();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.waitForExist('#plugin-name', 50000);
    browser.pause(10000);

    const plugin2 = pluginsPage.getSecondPluginRowObj();
    expect(plugin2.id).equal(2);
    expect(plugin2.name).equal('Microting Customers Plugin');
    expect(plugin2.version).equal('1.0.0.0');

    plugin2.pluginSettingsBtn.click();
    browser.waitForVisible('#pluginOKBtn', 40000);
    pluginPage.pluginOKBtn.click();
    browser.pause(50000); // We need to wait 50 seconds for the plugin to create db etc.
    browser.refresh();

    loginPage.login();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.waitForExist('#plugin-name', 50000);
    browser.pause(10000);
  });


  it('should create eform', function () {
    loginPage.open('/');
    const label = 'Number 1';
    browser.pause(8000);
    inspectionsPage.createNewEform(label);
    browser.pause(8000);
  });
  it('should create a new device user', function () {
    myEformsPage.Navbar.goToDeviceUsersPage();
    browser.waitForVisible('#newDeviceUserBtn', 20000);
    const name = 'Alice';
    const surname = 'Springs';
    browser.pause(2000);
    deviceUsersPage.createNewDeviceUser(name, surname);
  });
  it('should add eForm and device user to settings', function () {
    const deviceUser = deviceUsersPage.getDeviceUser(1);
    const sdkSiteId = deviceUser.siteId.getText();
    myEformsPage.Navbar.advancedDropdown();
    myEformsPage.Navbar.clickonSubMenuItem('Plugins');
    browser.waitForExist('#plugin-name', 50000);
    browser.pause(20000);
    const plugin = pluginsPage.getFirstPluginRowObj();
    plugin.settingsBtn.click();
    browser.pause(20000); // has to wait for spinner to go away
    rentableItemsSettingsPage.sdkSiteIdField.addValue(sdkSiteId);
    browser.pause(2000);
    rentableItemsSettingsPage.eFormSelector.addValue('Number 1');
    browser.pause(2000);
    inspectionsPage.selectOption('Number 1');
    browser.pause(2000);
    rentableItemsSettingsPage.saveBtn.click();
    browser.pause(4000);
  });
});
