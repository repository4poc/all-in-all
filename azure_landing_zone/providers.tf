terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 5.0"
    }
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {
    key_vault {
      purge_soft_delete_on_destroy    = var.environment == "dev" ? false : true
      recover_soft_deleted_key_vaults = var.environment == "dev" ? false : true
    }
  }
}
