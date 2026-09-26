variable "name" {}
variable "resource_group_name" {}
variable "location" {  default = "swedencentral"}
variable "sku" {
  default = "Basic"
}
variable "tags" {
  type = map(string)
  default = {}
}