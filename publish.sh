#!/bin/sh -v
set -e
git clean -xfd
dotnet pack
ls -al SlnEditor/bin/Release/SlnEditor.*.nupkg
dotnet nuget push SlnEditor/bin/Release/SlnEditor.*.nupkg --api-key="$apikey" --source https://api.nuget.org/v3/index.json
