param location string
param environment string
param projectName string
param namePrefix string

resource databricksWorkspace 'Microsoft.Databricks/workspaces@2024-05-01' = {
  name: 'dbx-${namePrefix}'
  location: location
  sku: {
    name: 'premium'
  }
  properties: {
    managedResourceGroupId: '${subscription().id}/resourceGroups/rg-managed-dbx-${namePrefix}'
  }
  tags: {
    environment: environment
    project: projectName
    managedBy: 'bicep'
  }
}

output workspaceName string = databricksWorkspace.name
output workspaceResourceId string = databricksWorkspace.id
output workspaceUrl string = databricksWorkspace.properties.workspaceUrl
