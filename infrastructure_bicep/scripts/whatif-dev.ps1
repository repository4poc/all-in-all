$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"

az.cmd deployment group what-if `
  --resource-group $ResourceGroup `
  --template-file .\main.bicep `
  --parameters .\environments\dev.parameters.json