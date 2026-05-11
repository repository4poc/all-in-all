
resource "azurerm_kubernetes_cluster" "aks" {
  name                              = "aks-${var.appname}-${var.env}-se-01"
  location                          = var.region
  resource_group_name               = var.resource_group_name
  dns_prefix                        = "aks-${var.appname}-${var.env}-se-01"
  oidc_issuer_enabled               = true
  role_based_access_control_enabled = true

  // AKS and EntraID integration
  azure_active_directory_role_based_access_control {
    tenant_id          = var.tenant_id
    azure_rbac_enabled = true
  }

  identity {
    type = "SystemAssigned"
  }

  sku_tier = var.sku_tier

  # Infrastructure Resource Group
  node_resource_group = "rg-${var.appname}-${var.env}-aks-infra"

  # Enable Keda and VPA for workload autoscaling
  workload_autoscaler_profile {
    keda_enabled                    = true
    vertical_pod_autoscaler_enabled = true # Need min 2 replicas for VPA to work, so set system node pool to 2 if you want to use VPA on system node pool as well.
  }

  # Enable Private Cluster if specified
  private_cluster_enabled = false

  # Service Mesh add-on with Istio mode and specific revision
  service_mesh_profile {
    mode      = "Istio"
    revisions = ["1.17.2"]
  }

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

  depends_on = [
    azurerm_kubernetes_cluster.aks
  ]
}

resource "azurerm_kubernetes_cluster_node_pool" "backend" {
  name                  = "backend"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks.id
  vm_size               = var.aks_app_nodepool_vm_size

  auto_scaling_enabled = true
  min_count            = var.backend_min
  max_count            = var.backend_max

  mode = "User"

  #  node_labels = {
  #   workload = "backend"
  #}

  lifecycle {
    ignore_changes = [
      node_count
    ]
  }

  upgrade_settings {
    max_surge = "33%"
  }

  #node_taints = [
  #  "workload=backend:NoSchedule"
  #]

  zones = (var.sku_tier == "standard") ? ["1", "2", "3"] : null

  depends_on = [
    azurerm_kubernetes_cluster.aks,
    azurerm_kubernetes_cluster_node_pool.apps
  ]

}


resource "kubernetes_namespace_v1" "apps" {
  metadata {
    name = "apps"

    # Add label for Istio sidecar injection based on the revision of Istio installed by the service mesh add-on
    labels = {
      "istio.io/rev" = "asm-1.17.2"
    }
  }

  depends_on = [
    azurerm_role_assignment.aks_rbac_cluster_admin
  ]
}

resource "kubernetes_namespace_v1" "argocd" {
  metadata {
    name = "argocd"
  }

  depends_on = [
    azurerm_role_assignment.aks_rbac_cluster_admin
  ]
}

resource "helm_release" "argocd" {
  name      = "argocd"
  namespace = kubernetes_namespace_v1.argocd.metadata[0].name

  repository = "https://argoproj.github.io/argo-helm"
  chart      = "argo-cd"

  values = [
    file("${path.root}/argocd/values.yaml")
  ]

  depends_on = [
    kubernetes_namespace_v1.argocd,
    azurerm_role_assignment.aks_rbac_cluster_admin
  ]
}

data "azurerm_client_config" "current" {}

resource "azurerm_role_assignment" "kubernetes_registry" {
  scope                            = var.acr_id
  role_definition_name             = "AcrPull"
  principal_id                     = azurerm_kubernetes_cluster.aks.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true

  depends_on = [azurerm_kubernetes_cluster.aks]
}

resource "azurerm_role_assignment" "aks_cluster_user" {
  scope                = azurerm_kubernetes_cluster.aks.id
  role_definition_name = "Azure Kubernetes Service Cluster User Role"
  principal_id         = data.azurerm_client_config.current.object_id
}

resource "azurerm_role_assignment" "aks_rbac_cluster_admin" {
  scope                = azurerm_kubernetes_cluster.aks.id
  role_definition_name = "Azure Kubernetes Service RBAC Cluster Admin"
  principal_id         = data.azurerm_client_config.current.object_id
}
