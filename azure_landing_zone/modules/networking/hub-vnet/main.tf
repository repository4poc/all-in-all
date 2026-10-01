locals {
  rg_key = "rg-${var.resource_group}-${var.region}"
}

resource "azurerm_virtual_network" "hub_virtual_network" {
  name                = "hub-vnet"
  location            = var.region
  resource_group_name = local.rg_key
  address_space       = var.address_space //["10.0.0.0/16"]
  dns_servers         = var.dns_servers   //["10.0.0.4", "10.0.0.5"]

  subnet {
    name             = "GatewaySubnet"
    address_prefixes = [cidrsubnet(var.address_space[0], 8, 1)] //["10.0.1.0/26"]
  }

  subnet {
    name             = "AzureBastionSubnet"
    address_prefixes = [cidrsubnet(var.address_space[0], 8, 2)] //["10.0.2.0/24"]
  }


  subnet {
    name             = "AzureFirewallSubnet"
    address_prefixes = [cidrsubnet(var.address_space[0], 8, 3)] //["10.0.3.0/24"]
  }


  tags = {
    Environment = "Dev"
  }
}
