# We strongly recommend using the required_providers block to set the
# Azure Provider source and version being used
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.1.0"
    }
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {}
}

module "acr" {
  source = "./modules/containers/acr"
  region = var.region
  resource_group_name = "rg_shared"
}

module "aks" {
  source = "./modules/containers/aks"
  env = var.environment
  appname = var.appname
  region = var.region
  resource_group_name = var.environment
  acr_id = module.acr.acr_id
}
