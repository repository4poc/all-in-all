resource "azurerm_container_registry" "acr" {
  name                = "allinallacr"
  resource_group_name = var.resource_group_name
  location            = var.location
  sku                 = var.sku

  admin_enabled = false

  identity {
    type = "SystemAssigned"
  }

  tags = var.tags

  # In case you dont't want acr to be delete from terraform 
  lifecycle {
    prevent_destroy = false
  }
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