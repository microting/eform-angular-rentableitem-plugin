import Page from '../Page';

export class RentableItemSettingsPage extends Page {
  constructor() {
    super();
  }

  public get eFormSelector() {
    return browser.element(`//*[@id= 'eFormId']//input`);
  }

  public get sdkSiteIdField() {
    return browser.element('#sdkSiteIds');
  }

  public get saveBtn() {
    return browser.element('#saveBtn');
  }
}

const rentableItemsSettingsPage = new RentableItemSettingsPage();
export default rentableItemsSettingsPage;
