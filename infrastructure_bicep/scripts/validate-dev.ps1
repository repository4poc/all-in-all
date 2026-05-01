$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"

az.cmd deployment group validate `
  --resource-group $ResourceGroup `
  --template-file .\main.bicep `
  --parameters .\environments\dev.parameters.json