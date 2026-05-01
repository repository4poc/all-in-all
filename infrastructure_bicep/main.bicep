targetScope = 'resourceGroup'

param location string = resourceGroup().location
param environment string
param projectName string

var namePrefix = '${projectName}-${environment}'

module databricksWorkspace './modules/databricks-workspace.bicep' = {
  name: 'deploy-databricks-workspace'
  params: {
    location: location
    environment: environment
    projectName: projectName
    namePrefix: namePrefix
  }
}

output workspaceName string = databricksWorkspace.outputs.workspaceName
output workspaceResourceId string = databricksWorkspace.outputs.workspaceResourceId
output workspaceUrl string = databricksWorkspace.outputs.workspaceUrl
