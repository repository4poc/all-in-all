
resource "azurerm_kubernetes_cluster" "aks" {
  name                = "aks-${var.appname}-${var.env}-se-01"
  location            = var.region
  resource_group_name = var.resource_group_name
  dns_prefix          = "aks-${var.appname}-${var.env}-se-01"
  oidc_issuer_enabled = true

  identity {
    type = "SystemAssigned"
  }

  default_node_pool {
    name       = "system"
    node_count = var.system_node_count
    vm_size    = var.aks_sys_nodepool_vm_size

    only_critical_addons_enabled = true

  }

  tags = var.tags
}

resource "azurerm_kubernetes_cluster_node_pool" "apps" {
  name                  = "apps"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks.id
  vm_size               = var.aks_app_nodepool_vm_size

  auto_scaling_enabled = true
  min_count           = var.apps_min
  max_count           = var.apps_max

  node_labels = {
    workload = "apps"
  }

  node_taints = ["workload=apps:NoSchedule"]

  zones = var.env == "prod" ? ["1", "2", "3"] : null
}


resource "azurerm_kubernetes_cluster_node_pool" "backend" {
  name                  = "backend"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks.id
  vm_size               = var.aks_app_nodepool_vm_size

  auto_scaling_enabled = true
  min_count           = var.backend_min
  max_count           = var.backend_max

  node_labels = {
    workload = "backend"
  }

  node_taints = [
    "workload=backend:NoSchedule"
  ]

  zones = var.env == "prod" ? ["1", "2", "3"] : null

}

resource "azurerm_role_assignment" "kubernetes_registry" {
  scope                = var.acr_id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}