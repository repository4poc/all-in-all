# Create a resource group
resource "azurerm_resource_group" "rg" {
  name     = "rg-${var.appname}-${var.tenant_code}-${var.environment}"
  location = var.region
  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}


resource "azurerm_storage_account" "sa" {
  name                     = "sa${var.appname}${var.tenant_code}${var.environment}"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS" // GZRS - production

  # Recommended security settings
  https_traffic_only_enabled = false // true - production
  min_tls_version            = "TLS1_2"

  # Terraform state recovery
  blob_properties {
    versioning_enabled = false // true - production

    # Recover deleted blobs
    delete_retention_policy {
      days = 30
    }

    # Recover deleted containers
    container_delete_retention_policy {
      days = 30
    }
  }

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })

  depends_on = [azurerm_resource_group.rg] // Imp: To specify the resource creation order.
}

resource "azurerm_storage_container" "container" {
  name                  = "images"
  storage_account_id    = azurerm_storage_account.sa.id
  container_access_type = "private"
}

resource "azurerm_network_security_group" "nsg" {
  name                = "nsg-${var.appname}-${var.tenant_code}-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  security_rule {
    name                       = "AllowTCP"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
  }

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}

resource "azurerm_virtual_network" "vnet" {
  name                = "vnet-${var.appname}-${var.tenant_code}-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  address_space       = [local.vnet_address_range]                   //["10.0.0.0/16"]
  dns_servers         = [local.dns_servers[0], local.dns_servers[1]] //["10.0.0.4", "10.0.0.5"]

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}

resource "azurerm_subnet" "app_subnet" {
  name                 = "app"
  resource_group_name  = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = [local.subnet_values[0]] //["10.0.1.0/24"]
}

resource "azurerm_subnet" "backend_subnet" {
  name                 = "backend"
  resource_group_name  = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = [local.subnet_values[1]] // ["10.0.2.0/24"]
}

resource "azurerm_network_interface" "vnic" {
  name                = "vnic-${var.appname}-${var.tenant_code}-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  ip_configuration {
    name                          = "internal"
    subnet_id                     = azurerm_subnet.app_subnet.id
    private_ip_address_allocation = "Dynamic"
  }

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}

output "vmid" {
  value = azurerm_network_interface.vnic.id
}
