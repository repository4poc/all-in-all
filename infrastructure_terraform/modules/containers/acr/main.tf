data "azurerm_resource_group" "rg_acr" {
  name     = "rg-shared"
}

resource "azurerm_container_registry" "acr" {
  name                = "allinallacr"
  resource_group_name = data.azurerm_resource_group.rg_acr.name
  location            = var.region
  sku                 = var.sku

  identity {
    type = "SystemAssigned"
  }

  admin_enabled = false

  # In case you dont't want acr to be delete from terraform 
  lifecycle {
    prevent_destroy = false
  }

  tags = {
    environment = "shared"
  }
}

resource "azurerm_management_lock" "acr_lock" {
  name       = "acr-delete-lock"
  scope      = azurerm_container_registry.acr.id
  lock_level = "CanNotDelete"
  notes      = "Prevent accidental deletion of ACR"
}

output "acr_name" {
  value = azurerm_container_registry.acr.name
}

output "acr_id" {
  value = azurerm_container_registry.acr.id
}

output "acr_login_server" {
  value = azurerm_container_registry.acr.login_server
}