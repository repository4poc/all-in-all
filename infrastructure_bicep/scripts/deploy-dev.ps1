$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"
$Location = "westeurope"

az.cmd group create `
  --name $ResourceGroup `
  --location $Location

az.cmd deployment group create `
  --resource-group $ResourceGroup `
  --template-file .\main.bicep `
  --parameters .\environments\dev.parameters.json