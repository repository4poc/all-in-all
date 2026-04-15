
resource "azurerm_kubernetes_cluster" "aks_allinall" {
  name                = "aks-${var.appname}-${var.env}-01"
  location            = var.region
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
    Environment = var.env
    Name = var.appname
  }

}

resource "azurerm_role_assignment" "kubernetes_registry" {
  scope                = var.acr_id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.aks_allinall.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}