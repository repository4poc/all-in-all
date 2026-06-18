$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"
$Location = "swedencentral"

az.cmd group create `
  --name $ResourceGroup `
  --location $Location

az.cmd deployment group create `
  --resource-group $ResourceGroup `
  --template-file .\main.bicep `
  --parameters .\environments\dev.parameters.json