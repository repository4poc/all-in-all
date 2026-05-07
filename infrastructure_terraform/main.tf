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
  }
}

provider "azuread" {}

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
  location = "West Europe"
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

resource "azurerm_cognitive_account" "foundry" {
  name                = "foundry${var.appname}${var.environment}007"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  kind     = "AIServices"
  sku_name = "S0"

  custom_subdomain_name      = "foundry${var.appname}${var.environment}007"
  project_management_enabled = true

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_cognitive_account_project" "project" {
  name                 = "myproject"
  cognitive_account_id = azurerm_cognitive_account.foundry.id
  location             = azurerm_resource_group.rg.location

  identity {
    type = "SystemAssigned"
  }
  depends_on = [
    azurerm_cognitive_account.foundry
  ]
}

resource "azurerm_cognitive_deployment" "gpt5_mini" {
  name                 = "gpt-5.4-mini-deployment"
  cognitive_account_id = azurerm_cognitive_account.foundry.id

  sku {
    name     = "GlobalStandard"
    capacity = 1

  }

  model {
    format  = "OpenAI"
    name    = "gpt-5.4-mini"
    version = "2026-03-17"
  }

  depends_on = [
    azurerm_cognitive_account.foundry,
    azurerm_cognitive_account_project.project
  ]
}

module "databricks" {
  count               = 0 # Skipped , put it to 1
  source              = "./modules/dataanalytics/databricks"
  appname             = var.appname
  environment         = var.environment
  tags                = var.tags
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.region
  sku_tier            = "premium"
}
