#!/bin/sh
set -e
sed -i "s|{__TOKEN__}|$TOKEN|g" appsettings.json
exec dotnet dotnet-folder.dll
