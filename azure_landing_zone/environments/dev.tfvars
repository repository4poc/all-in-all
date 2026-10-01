policy_configuration = {
  policy_scope = "MG" # Possible Value : MG/SUB
  
  allowed_locations = [
    "westeurope",
    "northeurope"
  ]

  allowed_resource_types = [
    "Microsoft.Compute/virtualMachines",
    "Microsoft.Storage/storageAccounts",
    "Microsoft.KeyVault/vaults"
  ]

  required_tags = [
    "Environment",
    "Application",
    "Owner",
    "CostCenter"
  ]
}

regions=[
  "westeurope","swedencentral"
]
environment = "dev"

resource_groups = [
  "app",
  "data",
  "storage",
  "network",
  "monitoring"
]

virtual_networks = {
  hub = {
    address_space = {
      westeurope    = "10.0.0.0/16"
      swedencentral = "10.2.0.0/16"
    }

    subnets = {
      GatewaySubnet = {
        subnet_address_prefix = {
          westeurope    = "10.0.1.0/26"
          swedencentral = "10.2.1.0/26"
        }
        network_security_group_rules = []
      }

      AzureFireWall = {
        subnet_address_prefix = {
          westeurope    = "10.0.2.0/24"
          swedencentral = "10.2.2.0/24"
        }
        network_security_group_rules = []
      }

      AzureBastionSubnet = {
        subnet_address_prefix = {
          westeurope    = "10.0.3.0/24"
          swedencentral = "10.2.3.0/24"
        }
        network_security_group_rules = []
      }
    }
  }

  spoke = {
    address_space = {
      westeurope    = "10.1.0.0/16"
      swedencentral = "10.3.0.0/16"
    }

    subnets = {
      WebSubnet = {
        subnet_address_prefix = {
          westeurope    = "10.1.1.0/24"
          swedencentral = "10.3.1.0/24"
        }
                network_security_group_rules = [
          {
            name                       = "Allow-HTTP-Inbound"
            priority                   = 100
            direction                  = "Inbound"
            access                     = "Allow"
            protocol                   = "Tcp"
            source_port_range          = "*"
            destination_port_range     = "80"
            source_address_prefix      = "*"
            destination_address_prefix = "*"
          },
          {
            name                       = "Allow-HTTPS-Inbound"
            priority                   = 110
            direction                  = "Inbound"
            access                     = "Allow"
            protocol                   = "Tcp"
            source_port_range          = "*"
            destination_port_range     = "443"
            source_address_prefix      = "*"
            destination_address_prefix = "*"
          },
          {
            name                       = "Allow-Backend-From-Web"
            priority                   = 120
            direction                  = "Outbound"
            access                     = "Allow"
            protocol                   = "Tcp"
            source_port_range          = "*"
            destination_port_range     = "8080"
            source_address_prefix      = "*"
            destination_address_prefix = "10.1.2.0/24"
          }
        ]
      }

      BackendSubnet = {
        subnet_address_prefix = {
          westeurope    = "10.1.2.0/24"
          swedencentral = "10.3.2.0/24"
        }
        network_security_group_rules = [
          {
            name                       = "Allow-App-Port-From-Web"
            priority                   = 100
            direction                  = "Inbound"
            access                     = "Allow"
            protocol                   = "Tcp"
            source_port_range          = "*"
            destination_port_range     = "8080"
            source_address_prefix      = "10.1.1.0/24"
            destination_address_prefix = "*"
          }
        ]
      }
    }
  }
}

features = {
  storage_account = false
  aks             = true
  acr             = true
  key_vault       = false
  log_analytics_workspace = false
  virtual_network = true
  firewall        = false
  bastion         = false
  policy          = true
}