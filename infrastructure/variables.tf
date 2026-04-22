variable "appname" {
  type = string
}
variable "environment" {
  type = string
}

variable "region" {
  type = string
}

variable "enable_monitoring" {
  type = bool
}

variable "sku_tier" {}


variable "system_node_count" {}
variable "aks_sys_nodepool_vm_size" {}

variable "aks_app_nodepool_vm_size" {}
variable "apps_min" {}
variable "apps_max" {}
variable "backend_min" {}
variable "backend_max" {}
variable "kube_version_upgrade" {}

variable "tags" {}
