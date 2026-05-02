$ErrorActionPreference = "Stop"

$ResourceGroup = "rg-dbx-dev-weu"
$WorkspaceName = "dbx-analytics-dev"

$ClusterName = "dev-ml-single-node"
$SparkVersion = "17.3.x-cpu-ml-scala2.13"
$NodeTypeId = "Standard_D4ds_v5"
$AutoTerminationMinutes = 120

Write-Host "Getting Databricks workspace..."

$Workspace = az databricks workspace show `
  --resource-group $ResourceGroup `
  --name $WorkspaceName `
  | ConvertFrom-Json

$WorkspaceUrl = "https://$($Workspace.workspaceUrl)"
$WorkspaceResourceId = $Workspace.id

Write-Host "Workspace URL: $WorkspaceUrl"

Write-Host "Getting Azure Databricks access token..."

$DatabricksToken = az account get-access-token `
  --resource "2ff814a6-3304-4ab8-85cb-cd0e6f879c1d" `
  --query accessToken `
  --output tsv

$Headers = @{
  Authorization = "Bearer $DatabricksToken"
  "X-Databricks-Azure-Workspace-Resource-Id" = $WorkspaceResourceId
}

$Body = @{
  cluster_name = $ClusterName
  spark_version = $SparkVersion
  node_type_id = $NodeTypeId
  autotermination_minutes = $AutoTerminationMinutes
  num_workers = 0
  spark_conf = @{
    "spark.databricks.cluster.profile" = "singleNode"
    "spark.master" = "local[*]"
  }
  custom_tags = @{
    "ResourceClass" = "SingleNode"
    "environment" = "dev"
    "project" = "analytics"
    "managedBy" = "powershell"
  }
} | ConvertTo-Json -Depth 10

Write-Host "Creating Databricks compute: $ClusterName"

$Response = Invoke-RestMethod `
  -Method Post `
  -Uri "$WorkspaceUrl/api/2.0/clusters/create" `
  -Headers $Headers `
  -Body $Body `
  -ContentType "application/json"

$ClusterId = $Response.cluster_id

Write-Host "Compute creation request submitted."
Write-Host "Cluster ID: $ClusterId"

do {
  Start-Sleep -Seconds 20

  $Cluster = Invoke-RestMethod `
    -Method Get `
    -Uri "$WorkspaceUrl/api/2.0/clusters/get?cluster_id=$ClusterId" `
    -Headers $Headers

  Write-Host "Cluster state: $($Cluster.state)"

} while ($Cluster.state -in @("PENDING", "RESIZING", "RESTARTING"))

Write-Host "Cluster name: $($Cluster.cluster_name)"
Write-Host "Final cluster state: $($Cluster.state)"