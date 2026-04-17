#!/bin/sh
set -e # exit on error

if ! git diff --cached --quiet; then
  echo "Aborted: staged changes detected in git"
  exit 1
fi

latest=`mise ls-remote dotnet-core | grep -P '^\d+\.\d+\.\d+$' | sort --version-sort | tail -n 1`
echo "Updating to dotnet-core@$latest"

mise use "dotnet-core@$latest"

# Extract major version (e.g., "10" from "10.0.202")
major_version=$(echo "$latest" | cut -d. -f1)
target_framework="net${major_version}.0"
echo "Updating target framework to $target_framework"

# Update test project (single target - always tracks latest)
sed -i "s|<TargetFramework>net[0-9]*\.0</TargetFramework>|<TargetFramework>${target_framework}</TargetFramework>|g" Tests/Tests.csproj

# Note: SlnEditor library targets netstandard2.0 for broad compatibility - not updated here

if git diff --quiet mise.toml \
  Tests/Tests.csproj; then
  echo "Already on latest dotnet, nothing to do"
  exit 0
fi

echo "Running regression tests..."
dotnet test

echo "Committing upgrades..."
git commit --include mise.toml \
--include Tests/Tests.csproj \
--message "chore: Upgrade build to dotnet $latest"
