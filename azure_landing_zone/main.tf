

data "azurerm_policy_definition" "allowed_locations" {
  display_name = "Allowed locations"
}

data "azurerm_policy_definition" "allowed_resource_types" {
  display_name = "Allowed resource types"
}

data "azurerm_policy_definition" "require-tag" {
  display_name = "Require a tag on resources"
}

data "azurerm_policy_definition" "require-diagnostic-settings-eks-to-law" {
  display_name = "Deploy - Configure diagnostic settings for Azure Kubernetes Service to Log Analytics workspace"
}

data "azurerm_subscription" "current" {}

data "azurerm_management_group" "current" {
  name = "Landing_Zone_Management_Group"
}


resource "azurerm_management_group_policy_assignment" "allowed_locations" {
  name                 = "allowed-locations"
  display_name         = "Allowed Location"
  policy_definition_id = data.azurerm_policy_definition.allowed_locations.id
  management_group_id  = data.azurerm_management_group.current.id

  parameters = jsonencode({
    listOfAllowedLocations = {
      value = [
        "westeurope",
        "northeurope"
      ]
    }
  })
}

resource "azurerm_management_group_policy_assignment" "allowed_resource_types" {
  name                 = "allowed-resource-types"
  display_name         = "Allowed Resource Types"
  policy_definition_id = data.azurerm_policy_definition.allowed_resource_types.id
  management_group_id  = data.azurerm_management_group.current.id

  parameters = jsonencode({
    listOfResourceTypesAllowed = {
      value = [
        "Microsoft.Compute/virtualMachines",
        "Microsoft.Storage/storageAccounts",
        "Microsoft.OperationalInsights/workspaces"
      ]
    }
  })
}


resource "azurerm_management_group_policy_assignment" "required-tag" {
  name                 = "required-tag"
  display_name         = "Required Tag"
  policy_definition_id = data.azurerm_policy_definition.require-tag.id
  management_group_id  = data.azurerm_management_group.current.id

  parameters = jsonencode({
    tagName = {
      value = "Environment"
    }
  })
}

/*
resource "azurerm_management_group_policy_assignment" "require_diagnostic_settings" {
  name                 = "require_diagnostic"
  display_name         = "Deploys the diagnostic settings for Azure Kubernetes Service to stream resource logs to a Log Analytics workspace."
  policy_definition_id = data.azurerm_policy_definition.require-diagnostic-settings-eks-to-law.id
  management_group_id  = data.azurerm_management_group.current.id

  parameters = jsonencode({
    logAnalytics = {
      value = azurerm_log_analytics_workspace.law.id
    }
  })

  identity {
    type = "SystemAssigned"
  }

  location = "westeurope"

}

resource "azurerm_resource_group" "example" {
  name     = "example-resources"
  location = "West Europe"
}

resource "azurerm_log_analytics_workspace" "law" {
  name                = "Law2284"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = { "Environment" : "Dev" }

}

output "policy_identity_principal_id" {
  value = azurerm_management_group_policy_assignment.require_diagnostic_settings.identity[0].principal_id
}*/

resource "azurerm_resource_group" "resource_groups" {
  for_each = {
    for item in flatten([
      for region in var.regions : [
        for rg in var.resource_groups : {
          key    = "${rg}-${region}"
          name   = "rg-${rg}-${region}"
          region = region
        }
      ]
    ]) : item.key => item
  }

  name     = each.value.name
  location = each.value.region

  tags = {
    Environment = var.environment
  }

}

resource "azurerm_virtual_network" "virtual_network" {
  for_each = {
    for item in flatten([
      for region in var.regions : [
        for vnet_key, vnet in var.virtual_networks : {
          key    = "vnet-${vnet_key}-${region}"
          name   = "vnet-${vnet_key}-${region}"
          region = region

          # Select the CIDR configured for this region.
          address_space = vnet.address_space[region]
        }
      ]
    ]) : item.key => item
  }

  name                = each.value.name
  location            = each.value.region
  resource_group_name = "rg-network-${each.value.region}"
  address_space       = [each.value.address_space]

  tags = {
    Environment = var.environment
  }

  depends_on = [azurerm_resource_group.resource_groups]
}

resource "azurerm_subnet" "subnet" {
  for_each = {
    for item in flatten([
      for region in var.regions : [
        for vnet_key, vnet in var.virtual_networks : [
          for subnet_key, subnet in vnet.subnets : {
            key      = "${vnet_key}-${region}-${subnet_key}"
            name     = subnet_key
            region   = region
            vnet_key = vnet_key

            # Select the subnet CIDR configured for this region.
            prefix = subnet.subnet_address_prefix[region]
          }
        ]
      ]
    ]) : item.key => item
  }

  name                = each.value.name
  resource_group_name = "rg-network-${each.value.region}"
  virtual_network_name = azurerm_virtual_network.virtual_network[
    "vnet-${each.value.vnet_key}-${each.value.region}"
  ].name
  address_prefixes = [each.value.prefix]


  depends_on = [azurerm_virtual_network.virtual_network]

}

resource "azurerm_network_security_group" "subnet" {
  for_each = {
    for item in flatten([
      for region in var.regions : [
        for vnet_key, vnet in var.virtual_networks : [
          for subnet_key, subnet in vnet.subnets : {
            key    = "${vnet_key}-${region}-${subnet_key}"
            name   = "nsg-${vnet_key}-${region}-${lower(subnet_key)}"
            region = region
            rules  = subnet.network_security_group_rules
          }
        ]
      ]
    ]) : item.key => item
    if length(item.rules) > 0
  }

  name                = each.value.name
  location            = each.value.region
  resource_group_name = "rg-network-${each.value.region}"

  dynamic "security_rule" {
    for_each = each.value.rules

    content {
      name                       = security_rule.value.name
      priority                   = security_rule.value.priority
      direction                  = security_rule.value.direction
      access                     = security_rule.value.access
      protocol                   = security_rule.value.protocol
      source_port_range          = security_rule.value.source_port_range
      destination_port_range     = security_rule.value.destination_port_range
      source_address_prefix      = security_rule.value.source_address_prefix
      destination_address_prefix = security_rule.value.destination_address_prefix
    }
  }

  tags = {
    Environment = var.environment
  }

  depends_on = [azurerm_subnet.subnet]
}

resource "azurerm_subnet_network_security_group_association" "subnet" {
  for_each = azurerm_network_security_group.subnet

  subnet_id                 = azurerm_subnet.subnet[each.key].id
  network_security_group_id = each.value.id

  depends_on = [azurerm_network_security_group.subnet]

}


resource "azurerm_log_analytics_workspace" "log_analytics_workspace" {
  for_each            = toset(var.regions)
  name                = "law-${var.environment}-${each.value}"
  location            = each.value
  resource_group_name = "rg-monitoring-${each.value}"
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = {
    Environment = var.environment
  }

}


data "azurerm_client_config" "current" {}


resource "random_string" "kv_suffix" {
  length  = 3
  upper   = false
  special = false
}

resource "azurerm_key_vault" "key_vault" {
  for_each = toset(var.regions)

  name                        = "kv${var.environment}${substr(each.value, 0, 3)}${random_string.kv_suffix.result}"
  location                    = each.value
  resource_group_name         = "rg-app-${each.value}"
  rbac_authorization_enabled  = false
  enabled_for_disk_encryption = var.environment == "dev" ? false : true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = var.environment == "dev" ? 7 : 90
  purge_protection_enabled    = var.environment == "dev" ? false : true

  sku_name = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Get",
    ]

    secret_permissions = [
      "Get",
    ]

    storage_permissions = [
      "Get",
    ]
  }
}


resource "azurerm_storage_account" "storage_account" {
  for_each = toset(var.regions)

  name                     = "sa${var.environment}${substr(each.value, 0, 3)}${random_string.kv_suffix.result}"
  resource_group_name      = "rg-data-${each.value}"
  location                 = each.value
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = {
    Environment = var.environment
  }
}
