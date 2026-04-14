variable "name" {}
variable "resource_group_name" {}
variable "location" {}
variable "sku" {
  default = "Basic"
}
variable "tags" {
  type = map(string)
  default = {}
}