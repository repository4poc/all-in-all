# We strongly recommend using the required_providers block to set the
# Azure Provider source and version being used
terraform {
  required_providers {
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 3.0"
    }

    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }

    azapi = {
      source  = "Azure/azapi"
      version = "~> 2.0"
    }
  }
}

# Configure the Microsoft Azure Active Directory Provider
provider "azuread" {}

# Configure the Azure API Management Provider
provider "azapi" {}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {}
}

provider "kubernetes" {
  host                   = module.aks.kube_admin_config[0].host
  client_certificate     = base64decode(module.aks.kube_admin_config[0].client_certificate)
  client_key             = base64decode(module.aks.kube_admin_config[0].client_key)
  cluster_ca_certificate = base64decode(module.aks.kube_admin_config[0].cluster_ca_certificate)
}

provider "helm" {
  kubernetes = {
    host                   = module.aks.kube_admin_config[0].host
    client_certificate     = base64decode(module.aks.kube_admin_config[0].client_certificate)
    client_key             = base64decode(module.aks.kube_admin_config[0].client_key)
    cluster_ca_certificate = base64decode(module.aks.kube_admin_config[0].cluster_ca_certificate)
  }
}

# Create a resource group
resource "azurerm_resource_group" "rg" {
  name     = var.environment
  location = "swedencentral"
  tags     = var.tags
}

module "acr" {
  source              = "./modules/containers/acr"
  resource_group_name = azurerm_resource_group.rg.name
  region              = var.region
  tags                = var.tags
}

module "aks" {
  source                   = "./modules/containers/aks"
  env                      = var.environment
  appname                  = var.appname
  region                   = var.region
  resource_group_name      = azurerm_resource_group.rg.name
  acr_id                   = module.acr.acr_id
  system_node_count        = var.system_node_count
  aks_sys_nodepool_vm_size = var.aks_sys_nodepool_vm_size
  aks_app_nodepool_vm_size = var.aks_app_nodepool_vm_size
  apps_min                 = var.apps_min
  apps_max                 = var.apps_max
  backend_min              = var.backend_min
  backend_max              = var.backend_max
  sku_tier                 = var.sku_tier
  kube_version_upgrade     = var.kube_version_upgrade
  tags                     = var.tags
  tenant_id                = var.tenant_id

  depends_on = [
    module.acr
  ]

}
