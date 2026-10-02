variable "policy_configuration" {
  type = object({
    allowed_locations      = list(string)
    allowed_resource_types = list(string)
    required_tags          = list(string)
  })
}

variable "environment" {
  type = string
}

variable "regions" {
  type = list(string)
}

variable "resource_groups" {
  type = list(string)
}

variable "virtual_networks" {
  type = map(object({
    address_space = map(string)

    subnets = map(object({
      subnet_address_prefix = map(string)
      network_security_group_rules = list(object({
        name                       = string
        priority                   = number
        direction                  = string
        access                     = string
        protocol                   = string
        source_port_range          = string
        destination_port_range     = string
        source_address_prefix      = string
        destination_address_prefix = string
      }))

    }))
  }))
}

variable "features" {
  description = "Enable or disable resource types"
  type = object({
    storage_account         = bool
    aks                     = bool
    acr                     = bool
    key_vault               = bool
    log_analytics_workspace = bool
    virtual_network         = bool
    firewall                = bool
    bastion                 = bool
    policy                  = bool
    resource_group          = bool
  })
}
