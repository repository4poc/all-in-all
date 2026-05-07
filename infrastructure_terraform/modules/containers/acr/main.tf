resource "azurerm_container_registry" "acr" {
  name                = "allinallacr"
  resource_group_name = var.resource_group_name
  location            = var.region
  sku                 = var.sku

  identity {
    type = "SystemAssigned"
  }

  admin_enabled = false

  # In case of prod env. make sure it is false to avoid accidental deletion of registry and images
  lifecycle {
    prevent_destroy = false
  }

  tags = var.tags
}
