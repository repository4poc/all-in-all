
resource "azurerm_kubernetes_cluster" "aks_allinall" {
  name                = "aks-${var.name}-01"
  location            = var.location
  resource_group_name = var.resource_group_name
  dns_prefix          = "allinallaks1"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_D2_v2"
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    Environment = var.name
  }

}

data "azurerm_container_registry" "acr" {
  name                = "allinallacr"
  resource_group_name = var.resource_group_name
}

resource "azurerm_role_assignment" "kubernetes_registry" {
  scope                = data.azurerm_container_registry.acr.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.aks_allinall.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}