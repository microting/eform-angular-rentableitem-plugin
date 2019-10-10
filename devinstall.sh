#!/bin/bash
cd ~
pwd

if [ -d "Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/rentable-items-pn" ]; then
	rm -fR Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/rentable-items-pn
fi

cp -av Documents/workspace/microting/eform-angular-rentableitem-plugin/eform-client/src/app/plugins/modules/rentable-items-pn Documents/workspace/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/rentable-items-pn

if [ -d "Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/RentableItems.Pn" ]; then
	rm -fR Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/RentableItems.Pn
fi

cp -av Documents/workspace/microting/eform-angular-rentableitem-plugin/eFormAPI/Plugins/RentableItems.Pn Documents/workspace/microting/eform-angular-frontend/eFormAPI/Plugins/RentableItems.Pn
