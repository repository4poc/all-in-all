variable "resource_group_name" {}
variable "region" {}
variable "appname" {}
variable "env" {}


## ACR
variable "acr_id" {
  type = string
}

## AKS
variable "sku_tier" {}

variable "aks_sys_nodepool_vm_size" {}
variable "aks_app_nodepool_vm_size" {}
variable "system_node_count" {}
variable "apps_min" {}
variable "apps_max" {}
variable "backend_min" {}
variable "backend_max" {}
variable "kube_version_upgrade" {}

variable "tags" {}

variable "tenant_id" {
  type = string
}

