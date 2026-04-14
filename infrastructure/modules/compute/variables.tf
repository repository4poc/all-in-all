variable "cluster_name" {}
variable "location" {}
variable "resource_group_name" {}
variable "dns_prefix" {}

variable "kubernetes_version" {
  default = "1.29"
}

variable "node_count" {}
variable "vm_size" {}
variable "min_count" {}
variable "max_count" {}

variable "subnet_id" {}
variable "log_analytics_workspace_id" {}

variable "tags" {
  type = map(string)
}