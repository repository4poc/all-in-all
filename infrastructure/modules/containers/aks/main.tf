
resource "azurerm_kubernetes_cluster" "aks" {
  name                              = "aks-${var.appname}-${var.env}-se-01"
  location                          = var.region
  resource_group_name               = var.resource_group_name
  dns_prefix                        = "aks-${var.appname}-${var.env}-se-01"
  oidc_issuer_enabled               = true
  role_based_access_control_enabled = true

  identity {
    type = "SystemAssigned"
  }

  sku_tier = var.sku_tier

  # Infrastructure Resource Group
  node_resource_group = "rg-${var.appname}-${var.env}-aks-infra"

  # Automatically upgrades Kubernetes minor versions
  automatic_upgrade_channel = var.kube_version_upgrade
  #Automatically updates node OS and VM image
  node_os_upgrade_channel = "NodeImage"

  # Default - System Pool
  default_node_pool {
    name       = "system"
    node_count = var.system_node_count
    vm_size    = var.aks_sys_nodepool_vm_size

    only_critical_addons_enabled = true

    upgrade_settings {
      max_surge = "33%"
    }

    node_labels = {
      role = "system"
    }

  }


  # Maintenance Window
  maintenance_window {
    allowed {
      day   = "Sunday"
      hours = [2, 6]
    }
  }

  tags = var.tags
}

resource "azurerm_kubernetes_cluster_node_pool" "apps" {
  name                  = "apps"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks.id
  vm_size               = var.aks_app_nodepool_vm_size

  auto_scaling_enabled = true

  min_count = var.apps_min
  max_count = var.apps_max

  node_labels = {
    workload = "apps"
  }

  mode = "User"

  lifecycle {
    ignore_changes = [
      node_count,
      min_count,
      max_count
    ]
  }

  node_taints = [
    "workload=apps:NoSchedule"
  ]

  upgrade_settings {
    max_surge = "50%"
  }

  zones = (var.sku_tier == "standard") ? ["1", "2", "3"] : null
}

resource "azurerm_kubernetes_cluster_node_pool" "backend" {
  name                  = "backend"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks.id
  vm_size               = var.aks_app_nodepool_vm_size

  auto_scaling_enabled = true
  min_count            = var.backend_min
  max_count            = var.backend_max

  mode = "User"

  node_labels = {
    workload = "backend"
  }

  lifecycle {
    ignore_changes = [
      node_count
    ]
  }

  upgrade_settings {
    max_surge = "33%"
  }

  node_taints = [
    "workload=backend:NoSchedule"
  ]

  zones = (var.sku_tier == "standard") ? ["1", "2", "3"] : null

}

resource "azurerm_role_assignment" "kubernetes_registry" {
  scope                            = var.acr_id
  role_definition_name             = "AcrPull"
  principal_id                     = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}
