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

# Configure the Kubernetes Provider
provider "kubernetes" {
  host                   = module.aks.kube_admin_config[0].host
  client_certificate     = base64decode(module.aks.kube_admin_config[0].client_certificate)
  client_key             = base64decode(module.aks.kube_admin_config[0].client_key)
  cluster_ca_certificate = base64decode(module.aks.kube_admin_config[0].cluster_ca_certificate)
}

# Configure the Helm Provider
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
  location = var.region
  tags     = var.tags
}

resource "azurerm_storage_account" "input_data_storage" {
  name                = "allinallinputstorage007"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location

  account_tier             = "Standard"
  account_replication_type = "LRS"

  account_kind = "StorageV2"

  # Enable anonymous blob access
  allow_nested_items_to_be_public = true

  access_tier = "Hot"

  min_tls_version = "TLS1_2"

  public_network_access_enabled = true

  shared_access_key_enabled = true

  infrastructure_encryption_enabled = false

  cross_tenant_replication_enabled = false

  blob_properties {
    versioning_enabled = false

    delete_retention_policy {
      days = 7
    }
  }

  tags = var.tags
}

# Public Blob Container
resource "azurerm_storage_container" "input_data_container" {
  name                  = "input"
  storage_account_id    = azurerm_storage_account.input_data_storage.id
  container_access_type = "container"
}

resource "azurerm_storage_account" "output_data_storage" {
  name                = "allinalloutputstorage007"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location

  account_tier             = "Standard"
  account_replication_type = "LRS"

  account_kind = "StorageV2"

  # Enable anonymous blob access
  allow_nested_items_to_be_public = true

  access_tier = "Hot"

  min_tls_version = "TLS1_2"

  public_network_access_enabled = true

  shared_access_key_enabled = true

  infrastructure_encryption_enabled = false

  cross_tenant_replication_enabled = false

  blob_properties {
    versioning_enabled = false

    delete_retention_policy {
      days = 7
    }
  }

  tags = var.tags
}
# Public Blob Container
resource "azurerm_storage_container" "output_container" {
  name                  = "output"
  storage_account_id    = azurerm_storage_account.output_data_storage.id
  container_access_type = "container"
}

resource "random_string" "suffix" {
  length  = 6
  upper   = false
  special = false
}


# Azure AI Search - cheap/dev configuration
resource "azurerm_search_service" "ai_search" {
  name                = "srch-poc-${random_string.suffix.result}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location

  sku             = "free"
  replica_count   = 1
  partition_count = 1

  public_network_access_enabled = true

  tags = var.tags
}






# Create a Cognitive Account with kind AIServices which will enable Foundry capabilities
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

# Create a Cognitive Account Project which will be the container for all AI assets like models, agents, etc. This is required to deploy models in Foundry.
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

# Deploying GPT-5.4 Mini model using native azurerm provider support
resource "azurerm_cognitive_deployment" "gpt5" {
  name                 = "gpt-5-deployment"
  cognitive_account_id = azurerm_cognitive_account.foundry.id

  sku {
    name     = "GlobalStandard"
    capacity = 1

  }

  model {
    format  = "OpenAI"
    name    = "gpt-5"
    version = "2025-08-07"
  }

  depends_on = [
    azurerm_cognitive_account.foundry,
    azurerm_cognitive_account_project.project
  ]
}

# Recommended newer embedding model
resource "azurerm_cognitive_deployment" "text_embedding_ada_002" {
  name                 = "text-embedding-ada-002-deployment"
  cognitive_account_id = azurerm_cognitive_account.foundry.id

  sku {
    name     = "GlobalStandard"
    capacity = 1
  }

  model {
    format  = "OpenAI"
    name    = "text-embedding-ada-002"
    version = "2"
  }

  depends_on = [
    azurerm_cognitive_account.foundry,
    azurerm_cognitive_account_project.project
  ]
}


resource "azapi_resource" "claude_opus_4_7_deployment" {
  type      = "Microsoft.CognitiveServices/accounts/deployments@2025-10-01-preview"
  name      = "claude-opus-4-7-deployment"
  parent_id = azurerm_cognitive_account.foundry.id

  schema_validation_enabled = false

  body = {
    properties = {
      model = {
        format  = "Anthropic"
        name    = "claude-opus-4-7"
        version = "1"
      }

      modelProviderData = {
        organizationName = "Varinder"
        industry         = "consulting"
        countryCode      = "FI"
      }
    }

    sku = {
      name     = "GlobalStandard"
      capacity = 1
    }
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
