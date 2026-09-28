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
    name                       = "AllowHTTPInbound"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "80"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
  }

  security_rule {
    name                       = "AllowHTTPSInbound"
    priority                   = 110
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "443"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
  }


  security_rule {
    name                       = "AllowSSH"
    priority                   = 120
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "22"
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

resource "azurerm_subnet" "web_subnet" {
  name                 = "web_subnet"
  resource_group_name  = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = [local.subnet_values[0]] //["10.0.1.0/24"]
}

resource "azurerm_subnet" "backend_subnet" {
  name                 = "backend_subnet"
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
    subnet_id                     = azurerm_subnet.web_subnet.id
    private_ip_address_allocation = "Dynamic"
    public_ip_address_id          = azurerm_public_ip.publicip.id
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


resource "azurerm_public_ip" "publicip" {
  name                = "publicip-${var.appname}-${var.tenant_code}-${var.environment}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  allocation_method   = "Static"

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}

resource "azurerm_subnet_network_security_group_association" "subnet_nsg_association" {
  subnet_id                 = azurerm_subnet.web_subnet.id
  network_security_group_id = azurerm_network_security_group.nsg.id
}

resource "azurerm_virtual_machine" "vm" {
  name                  = "vm-${var.appname}-${var.tenant_code}-${var.environment}"
  location              = azurerm_resource_group.rg.location
  resource_group_name   = azurerm_resource_group.rg.name
  network_interface_ids = [azurerm_network_interface.vnic.id]
  vm_size               = "Standard_DS1_v2"

  # Comment this line to not delete the OS disk automatically when deleting the VM
  delete_os_disk_on_termination = true

  # comment this line to not delete the data disks automatically when deleting the VM
  delete_data_disks_on_termination = true

  storage_image_reference {
    publisher = "Canonical"
    offer     = "0001-com-ubuntu-server-jammy"
    sku       = "22_04-lts"
    version   = "latest"
  }
  storage_os_disk {
    name              = "osdisk-${var.appname}-${var.tenant_code}-${var.environment}"
    caching           = "ReadWrite"
    create_option     = "FromImage"
    managed_disk_type = "Standard_LRS"
  }
  os_profile {
    computer_name  = "hostname"
    admin_username = "testadmin"
    admin_password = var.admin_password
  }
  os_profile_linux_config {
    disable_password_authentication = false
  }
  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}


resource "azurerm_managed_disk" "datadisk" {
  name                 = "datadisk-${var.appname}-${var.tenant_code}-${var.environment}"
  location             = azurerm_resource_group.rg.location
  resource_group_name  = azurerm_resource_group.rg.name
  storage_account_type = "Standard_LRS"
  create_option        = "Empty"
  disk_size_gb         = "20"

  tags = merge(var.tags, {
    Application = var.appname
    Environment = var.environment
    Tenant      = var.tenant_code
  })
}


resource "azurerm_virtual_machine_data_disk_attachment" "vm_datadisk" {
  managed_disk_id    = azurerm_managed_disk.datadisk.id
  virtual_machine_id = azurerm_virtual_machine.vm.id
  lun                = "10"
  caching            = "ReadWrite"
}


resource "azurerm_key_vault" "keyvault" {
  name                        = "keyvault-${var.appname}-${var.tenant_code}-${var.environment}"
  location                    = azurerm_resource_group.rg.location
  resource_group_name         = azurerm_resource_group.rg.name
  rbac_authorization_enabled  = true
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 0     // 30 for dev/test , 90 for production
  purge_protection_enabled    = false // Enable for Production

  sku_name = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Get",
    ]

    secret_permissions = [
      "Get",
    ]

    storage_permissions = [
      "Get",
    ]
  }
}
