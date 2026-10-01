variable "name" {
  type = string
}

variable "display_name" {
  type = string
}

variable "management_group_id" {
  type = string
}

variable "policy_definition_id" {
  type = string
}

variable "parameters" {
  type    = string
  default = null
}

variable "description" {
  type    = string
  default = null
}

variable "metadata" {
  type    = string
  default = null
}
