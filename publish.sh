#!/bin/sh -v
set -e
dotnet pack
dotnet nuget push SlnEditor/bin/Debug/SlnEditor.1.0.0.nupkg --api-key="$apikey" --source https://api.nuget.org/v3/index.json
