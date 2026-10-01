variable "name" {
  type = string
}


variable "resource_group" {
  type = string
}

variable "region" {
  type = string
}

variable "address_space" {
  type = list(string)
}

variable "dns_servers" {
  type = list(string)
}

