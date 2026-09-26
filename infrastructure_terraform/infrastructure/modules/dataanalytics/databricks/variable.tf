variable "appname" {}
variable "environment" {}
variable "resource_group_name" {}
variable "location" {}
variable "tags" {
  type = map(string)
}
variable "sku_tier" {}
