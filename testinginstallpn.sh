#!/bin/bash
sed '/\/\/ INSERT ROUTES HERE/i {' src/app/plugins/plugins.routing.ts -i
sed '/\/\/ INSERT ROUTES HERE/i path: "rentable-items-pn",' src/app/plugins/plugins.routing.ts -i
sed '/\/\/ INSERT ROUTES HERE/i canActivate: [AuthGuard],' src/app/plugins/plugins.routing.ts -i
sed '/\/\/ INSERT ROUTES HERE/i loadChildren: "./modules/rentable-items-pn/rentable-items-pn.module#RentableItemsPnModule"' src/app/plugins/plugins.routing.ts -i
sed '/\/\/ INSERT ROUTES HERE/i },' src/app/plugins/plugins.routing.ts -i

