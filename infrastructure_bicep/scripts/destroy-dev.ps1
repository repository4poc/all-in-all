$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"

Write-Host "Deleting resource group: $ResourceGroup"

az.cmd group delete `
  --name $ResourceGroup `
  --yes `
  --no-wait

Write-Host "Deletion started (async)"