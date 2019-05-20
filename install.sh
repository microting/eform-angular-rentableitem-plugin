#!/bin/bash

if [ ! -d "/var/www/microting/eform-angular-rentableitem-plugin" ]; then
  cd /var/www/microting
  su ubuntu -c \
  "git clone https://github.com/microting/eform-angular-rentableitem-plugin.git -b stable"
fi

cd /var/www/microting/eform-angular-rentableitem-plugin
su ubuntu -c \
"dotnet restore eFormAPI/Plugins/RentableItems.Pn/RentableItems.Pn.sln"

echo "################## START GITVERSION ##################"
export GITVERSION=`git describe --abbrev=0 --tags | cut -d "v" -f 2`
echo $GITVERSION
echo "################## END GITVERSION ##################"
su ubuntu -c \
"dotnet publish eFormAPI/Plugins/RentableItems.Pn/RentableItems.Pn.sln -o out /p:Version=$GITVERSION --runtime linux-x64 --configuration Release"

if [ -d "/var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/trash-inspection-pn"]; then
	su ubuntu -c \
	"rm -fR /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/trash-inspection-pn"
fi

su ubuntu -c \
"cp -av /var/www/microting/eform-angular-rentableitem-plugin/eform-client/src/app/plugins/modules/trash-inspection-pn /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/trash-inspection-pn"
su ubuntu -c \
"mkdir -p /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/"

if [ -d "/var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/RentableItems"]; then
	su ubuntu -c \
	"rm -fR /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/RentableItems"
fi

su ubuntu -c \
"cp -av /var/www/microting/eform-angular-rentableitem-plugin/eFormAPI/Plugins/RentableItems.Pn/RentableItems.Pn/out /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/RentableItems"


echo "Recompile angular"
cd /var/www/microting/eform-angular-frontend/eform-client
su ubuntu -c \
"/var/www/microting/eform-angular-rentableitem-plugin/testinginstallpn.sh"
su ubuntu -c \
"npm run build"
echo "Recompiling angular done"


