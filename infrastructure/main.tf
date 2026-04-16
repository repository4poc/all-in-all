# We strongly recommend using the required_providers block to set the
# Azure Provider source and version being used
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {}
}

module "acr" {
  source              = "./modules/containers/acr"
  region              = var.region
  resource_group_name = "rg_shared"
  tags                = var.tags
}

module "aks" {
  source                   = "./modules/containers/aks"
  env                      = var.environment
  appname                  = var.appname
  region                   = var.region
  resource_group_name      = var.environment
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
}
