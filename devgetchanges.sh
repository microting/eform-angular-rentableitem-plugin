#!/bin/bash

cd ~

rm -fR Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/src/app/plugins/modules/rentable-items-pn

cp -a Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/rentable-items-pn Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/src/app/plugins/modules/rentable-items-pn

rm -fR Documents/workspace/microting/eform-angular-rentableitem-plugin/eFormAPI/Plugins/RentableItems.Pn

cp -a Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/RentableItems.Pn Documents/workspace/microting/eform-angular-rentableitem-plugin/eFormAPI/Plugins/RentableItems.Pn

# Test files rm
rm -fR Documents/workspace/microting/eform-angular-rentable-items-plugin/eform-client/e2e/Tests/rentable-items-settings/
rm -fR Documents/workspace/microting/eform-angular-rentable-items-plugin/eform-client/e2e/Tests/rentable-items-general/
rm -fR Documents/workspace/microting/eform-angular-rentable-items-plugin/eform-client/e2e/Page\ objects/Monitoring/
rm -fR Documents/workspace/microting/eform-angular-rentable-items-plugin/eform-client/wdio-headless-plugin-step2.conf.js

# Test files cp
cp -a Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/rentable-items-settings Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/e2e/Tests/rentableitem-settings
cp -a Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Tests/rentable-items-general Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/e2e/Tests/rentableitem-general
cp -a Documents/workspace/microting/eform-angular-frontend/eform-client/e2e/Page\ objects/rentableitem-general Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/e2e/Page\ objects/rentableitem-general
cp -a Documents/workspace/microting/eform-angular-frontend/eform-client/wdio-plugin-step2.conf.js Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/wdio-headless-plugin-step2.conf.js
