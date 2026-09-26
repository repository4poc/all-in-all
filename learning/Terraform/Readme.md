## Providers

Terraform provide provider to connect to different target

- AWS
- Azure
- GCP

![alt text](images/{93CEA61C-D68F-4192-9691-DB97A61C04C9}.png)

## Terraform Documentation

https://registry.terraform.io/browse/providers

## Create First Terraform Configuration File

`main.tf` - Uses HCL (HashiCorp Configuration Language)

```
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=5.0.0"
    }
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {}
}

```

## Authentication and Authorization onto Azure

- MS EntraID : Service for Authentication and Authorization

There are multiple ways of authentication in Terraform

![alt text](images/{4CC63DDE-B977-4CD8-9803-73A9898BB3A9}.png)

1. Define a User in Entra ID, Login with via Azure Cli
   - UserName
   - Password

   ```
   az login
   ```

2. Define an Application Object, Login with
   - Service Principle
   - Client Secret

## Create User and use Azure CLI (az login)

1. Install the Azure CLI tool
2. Login with Azure CLI

   ![alt text](images/{F32338A3-AE95-409D-824A-BFE1E5BE0868}.png)

Using Azure RBAC, we need to give `Contributor` role to the `Subscription`

- Have different subscriptions for Dev,Test,Prod
- Management Group --> Subscription(-Dev/Test/Prod) --> Resource Group --> Resources

## Write Terraform File - Azure resource group

https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group

- Azure reousrce group - It logically group resources

`Resource Block`

```
# Create a resource group
resource "azurerm_resource_group" "rg" {
  name     = "${var.appname}-${var.environment}"
  location = var.region
  tags     = var.tags
}

```

```
terraform init -var-file="envs/dev.tfvars"
terraform validate
terraform plan -out main.tfplan -var-file="envs/dev.tfvars"
terraform apply "main.tfplan"

```

![alt text](images/{BD6EFD88-C09E-4813-B9FF-D1F14B9538DC}.png)

## Terraform init

Initialize Working Directory

![alt text](images/{4711CB0A-B0A2-4BFF-BF45-A1B043F44D69}.png)

1. Downloads providers

   ```
   provider "azurerm" {
        features {}
   }
   ```

2. Initializes the backend ,If you configure a remote backend

   ```
   terraform {
        backend "azurerm" {
            # configuration
        }
   }
   ```

   Note: If you are using backend, create it it a separate resource group `rg-terraform-state` in each subscription

3. Downloads modules

   Terraform prepares the required modules.

   ```
    module "network" {
        source = "./modules/network"
    }
   ```

4. Creates the .terraform directory

   Terraform stores downloaded providers, modules, and other initialization information there

```
terraform init = "Prepare this Terraform project so Terraform can work with it."
```

## terraform validate

To validate your configuration files:

Terraform validate checks things such as:

1. Terraform syntax is valid
2. Resource blocks are structured correctly
3. Provider/resource arguments are valid
4. References to variables and resources are valid
5. Required arguments are present
6. Configuration is internally consistent

## Terraform Plan

shows you what Terraform intends to change in your infrastructure before it actually makes those changes.

```
terraform plan -var-file="envs/dev.tfvars"

terraform plan -out main.tfplan -var-file="envs/dev.tfvars"
```

Note we can incorporate GenAI, that readout this tfplan file and provide a notification onto teams with summary of the change and wait for approval (Human-in-the-loop).

## Terraform Apply

Actually makes the infrastructure changes described by your Terraform configuration or Plan

```
terrform apply "main.tfplan"
```

## New Terraform Generated Files (terraform.tfstate and .terraform.lock.hcl)

On `terraform apply`, `terraform.tfstate` file is generated on local, which contains the current state of environment. If we delete, it we will lost the remote cloud state. So it will assume all resource need to recreate fresh. So it is very important to keep it safe, for terraform to work.

![alt text](images/{7DADF1B3-68E7-4B6F-8FA9-2C15E8C7AE86}.png)

For Azure + Azure DevOps, the common recommended approach is to store your Terraform state in an Azure Storage Account using the azurerm backend, rather than keeping terraform.tfstate in your git repository or on the pipeline agent.

```
terraform {
  backend "azurerm" {
    resource_group_name  = "terraform-state-rg"
    storage_account_name = "mystateaccount"
    container_name       = "tfstate"
    key                  = "dev.tfstate"
  }
}
```

Why remote state (on Storage account) is better than Git

The state file contains important information about your infrastructure and can contain sensitive values.

Using Azure Storage gives you:

1. Centralized state — your team and Azure DevOps use the same state.
2. State locking — helps prevent two Terraform runs from modifying the same state simultaneously.
3. Persistence — the state isn't lost when an Azure DevOps agent is destroyed.
4. Access control — use Azure RBAC rather than putting credentials in the repository.
5. Versioning/recovery — Azure Storage can be configured for blob versioning and soft delete.

![alt text](images/{28647823-DF51-4C7C-9B20-0087D5FDA9B2}.png)

## Authenticate using Application Object

`Steps`

- MS EntraID --> Application Registration --> Create Registration

  ![alt text](images/{A500AE37-2EE1-4308-BA7F-91BA687E3464}.png)

- Assign `contributor` role to the `Service Principle` at `Subscription-Dev/Test/Prod` Level

  ![alt text]({5C3AEDD8-A79C-4508-9E3D-D11CB401414D}.png)

- Use `Client ID`, `Client Secret Value`, `Tenant ID`

  ![alt text](images/{A5E9D75E-916E-41CA-93CB-72C15E798087}.png)

  ![alt text](images/{F5E8322D-4030-48B0-A3D1-6F13A4FAF34E}.png)

When you create an App Registration for Terraform, you're typically creating a Service Principal (a non-human identity) that Terraform uses to authenticate to Azure.

In the context of Terraform authentication to Azure, choose:

✅ Accounts in this organizational directory only (Single tenant)

Why?

Terraform is usually deployed into:

- Your Azure subscription
- Your Entra ID tenant
- Your Azure DevOps organization

For modern Terraform deployments

Many teams no longer use client secrets at all. Instead they use:

- Managed Identity (Azure-hosted workloads)
- Workload Identity Federation (Azure DevOps/GitHub Actions)

### When would you use Multitenant?

Only if you're building a product that other companies will use.

```
Your SaaS Application
      ↓
Contoso users
Fabrikam users
Wingtip users
```

## Create Azure Storage Account

![alt text](images/{7E8EA4BF-8C06-45F7-BFD6-0C29FCB74A31}.png)

![alt text](images/{784B273A-8AEB-47CE-8D10-0F50F831CC65}.png)

![alt text](images/{0C980EB2-84B7-489A-A68A-0BBFD3FEDB1A}.png)

```
resource "azurerm_resource_group" "rg" {
  name     = "rg-${var.appname}-${var.tenant_code}-${var.environment}"
  location = var.region
  tags     = var.tags
}

## Note: We cant use - in storage account name

resource "azurerm_storage_account" "sa" {
  name                     = "sa${var.appname}${var.tenant_code}${var.environment}"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

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

  tags = {
    purpose = "terraform-state"
  }
}

```

A Resource Group per tenant per environment is commonly useful when you need:

- Tenant-level isolation of Azure resources
- Separate RBAC permissions for customers/teams
- Tenant-level cost tracking
- Independent deployment/lifecycle management
- Ability to delete or recreate one tenant's infrastructure
- Different policies or locks for different tenants
- Clear operational boundaries

```
Production
├── myapp-tenant-a-prod
│   ├── App Service
│   ├── Storage
│   └── Key Vault
│
├── myapp-tenant-b-prod
│   ├── App Service
│   ├── Storage
│   └── Key Vault
│
└── myapp-tenant-c-prod
    ├── App Service
    ├── Storage
    └── Key Vault
```

That's a reasonable strong-isolation SaaS architecture.

But, if all tenants use the same:

```
Azure SQL Server
Azure Service Bus
Application
AKS cluster
```

then putting some resources in different resource groups doesn't necessarily give you data isolation.

Your application/data architecture still needs appropriate tenant isolation.

A common SaaS approach is therefore:

```
                 SaaS Application
                       │
        ┌──────────────┴──────────────┐
        │                             │
   Shared infrastructure       Tenant-specific
        │                       infrastructure
        │                             │
   ┌────┴─────┐              ┌────────┴────────┐
   │           │              │                 │
Tenant A    Tenant B       Premium A         Premium B
Tenant C    Tenant D       resources         resources
```

So you don't necessarily need one complete Azure infrastructure stack per tenant.

### A useful enterprise model

```
Subscription
│
├── rg-myapp-shared-prod
│   ├── Application
│   ├── App Gateway
│   ├── Service Bus
│   └── Monitoring
│
├── rg-myapp-tenant-a-prod
│   └── Tenant-specific resources - DB , Secrets
│
├── rg-myapp-tenant-b-prod
│   └── Tenant-specific resources - DB , Secrets
│
└── rg-myapp-tenant-c-prod
    └── Tenant-specific resources - DB , Secrets
```

```

| Replication | Protection                                      | Typical production use                      |
| ----------- | ----------------------------------------------- | ------------------------------------------- |
| **LRS**     | 3 copies in one datacenter                      | Dev/test, non-critical workloads            |
| **ZRS**     | Copies across availability zones                | **Common production choice**                |
| **GRS**     | Local redundancy + async copy to another region | Production with regional DR                 |
| **GZRS**    | Zone redundancy + geo-replication               | **Strong choice for critical production**   |
| **RA-GRS**  | GRS + read access to secondary region           | DR scenarios needing secondary-region reads |
| **RA-GZRS** | GZRS + read access to secondary region          | **High-criticality workloads**              |


```

| Replication | Protection                                      | Typical production use                      |
| ----------- | ----------------------------------------------- | ------------------------------------------- |
| **LRS**     | 3 copies in one datacenter                      | Dev/test, non-critical workloads            |
| **ZRS**     | Copies across availability zones                | **Common production choice**                |
| **GRS**     | Local redundancy + async copy to another region | Production with regional DR                 |
| **GZRS**    | Zone redundancy + geo-replication               | **Strong choice for critical production**   |
| **RA-GRS**  | GRS + read access to secondary region           | DR scenarios needing secondary-region reads |
| **RA-GZRS** | GZRS + read access to secondary region          | **High-criticality workloads**              |

                    Production
                        │
             ┌──────────┴──────────┐
             │                     │
       Regional outage       Regional outage
       NOT acceptable?        acceptable?
             │                     │
            Yes                    No
             │                     │
           GZRS                  ZRS
             │
       Need secondary
       read access?
             │
          ┌──┴──┐
         Yes    No
          │      │
       RA-GZRS  GZRS

Remember that replication isn't a backup strategy. GRS/GZRS protects against certain infrastructure/region failures, but accidental deletion, corruption, or application-level mistakes can still replicate to the secondary location. You should have an appropriate backup/retention strategy as well.

## Reference to named values instead of hardcording

[resource_type].[terraform_resource_block_name].[resource_propety]

azurerm_resource_group.rg.name

## Destroy you infrastructure

```
terraform destroy -var-file="envs/dev.tfvars"

As a security practice.
It ask for your confirmation, you need to type 'Yes'
```

## depends_on Clause

## Create a VM

A Virtual Machine is deployed within an subnet,

- It gets
  - A vNIC (Virtual Network Interface)
    - IP address (public/private) is associated to the Subnet
  - A OS Disk
    - Host OS
  - A NSG (Network Security Group)
    - Filter incoming and outgoing connections onto the VM
    - It can be attached to either vNIC or Subnet

![alt text](images/{E5A97B4C-2488-4CBC-9083-81D6760D7313}.png)

`Virtual Machine`

![alt text]({9EA8B821-0290-42D4-A201-9F3657DB1CD8}.png)

`Network Interface Id`

![alt text]({A261854A-4E14-45EA-8789-242A52A3C6D4}.png)

```
resource "azurerm_network_interface" "example" {
  .
  .

  ip_configuration {
    name                          = "internal"
    subnet_id                     = azurerm_subnet.example.id
    private_ip_address_allocation = "Dynamic"
  }
}
```

`Subnet`

![alt text](images/{8C39FBF1-3260-465C-8C49-A35CFC56D622}.png)

`Virtual Network`

![alt text](images/{FAC0EA99-D722-45CC-B8BE-215A48E2CF50}.png)

```
resource "azurerm_virtual_network" "example" {
  name                = "vnet-network"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  address_space       = ["10.0.0.0/16"]
  dns_servers         = ["10.0.0.4", "10.0.0.5"]

  subnet {
    name             = "subnet1"
    address_prefixes = ["10.0.1.0/24"]
  }

  subnet {
    name             = "subnet2"
    address_prefixes = ["10.0.2.0/24"]
    security_group   = azurerm_network_security_group.example.id
  }

  tags = {
    environment = "Production"
  }
}
```

```
resource "azurerm_network_security_group" "example" {
  name                = "acceptanceTestSecurityGroup1"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name

  security_rule {
    name                       = "test123"
    priority                   = 100
    direction                  = "Inbound"
    access                     = "Allow"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
  }

  tags = {
    environment = "Production"
  }
}

In Azure, a Network Interface (NIC) is a separate resource that sits between the VM and the virtual network. That's why the NIC is configured with a subnet, while the VM is configured with a NIC.

resource "azurerm_network_interface" "example" {
  name                = "example-nic"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name

  ip_configuration {
    name                          = "internal"
    subnet_id                     = azurerm_subnet.example.id
    private_ip_address_allocation = "Dynamic"
  }
}
```

### All we are creating is Managed Resources using Managed Services of Azure.

## How to calculate IP Address range

10.0.0.0/16

IPv4 has 32 bits.

/16 means:

- 16 bits = network portion
- 16 bits = host portion (2^16 = 65536 IP Addresses)

So the subnet mask is:

```
255.255.0.0
```

10.0.0.0/16 = 65,536 total IP addresses

Range: 10.0.0.0 - 10.0.255.255

For 10.0.0.0/16:

- Network address: 10.0.0.0 → identifies the subnet itself
- Broadcast address: 10.0.255.255 → sends traffic to all hosts in that subnet
- Usable host addresses: 10.0.0.2 through 10.0.255.253

| Type               | Address                   |
| ------------------ | ------------------------- |
| Network address    | `10.0.0.0`                |
| AWS/Azure reserved | `10.0.0.1`                |
| AWS/Azure reserved | `10.0.0.2`                |
| AWS/Azure reserved | `10.0.0.3`                |
| Usable host range  | `10.0.0.4` – `10.0.0.254` |
| Broadcast/reserved | `10.0.0.255`              |

### Subnet

10.0.1.0/24 = 2^32-24=2^8 = 256 Total IP addresses

`Range`

```
VNet: 10.0.0.0/16
│
├── Subnet A: 10.0.0.0/24
│   └── 5 reserved
│
├── Subnet B: 10.0.1.0/24
│   └── 5 reserved
│
├── Subnet C: 10.0.2.0/24
│   └── 5 reserved
│
├── Subnet D: 10.0.3.0/24
│   └── 5 reserved
│
└── Subnet E: 10.0.4.0/24
    └── 5 reserved
```

`There is no resource as subnet, it exists within the virtual network resource`

- VM Resource (Associated in vNIC)
- Public IP Resource (Attached to vNIC)

- vNIC Resource (Associated on Subnet )

- Subnets (No Resource) (Associated to on vNet )

- NSG Resource (Associated to vNIC or Subnet)

## How to upgrade terraform provider version

```
terraform init -upgrade
```

terraform init -upgrade primarily updates/initializes:

- Terraform providers
- Terraform modules
- Backend configuration/initialization
- Dependency lock information (.terraform.lock.hcl)

Your state file is not removed

## Using Local Variables

```
locals {
    version ="1.0.0"
}


local.version
```

value can be

- string
- number
- bool
- null
- map
- Array
- Expression

## Splitting Terraform Configuration files

main.tf can be split into

- terraform.tf (Provider Configuration)
- locals.tf (Local configuration)

So it does not matter how many configuration files you have, it will consider all the .tf files at the project root level.

## Types and Values - List

`local.tf`

```
locals {
  vnet_address_range = "10.0.0.0/16"
  subnet_values      = ["10.0.1.0/24", "10.0.2.0/24"]
}
```

`main.tf`

```
  subnet {
    name             = "backend"
    address_prefixes = [local.subnet_values[1]]
    security_group   = azurerm_network_security_group.nsg.id
  }
```

## Types and Values - Map

`local.tf`

```
locals {

  virtual_machine1  = {
    vnet_address_range = "10.0.0.0/16"
    subnet_values      = ["10.0.1.0/24", "10.0.2.0/24"]
    dns_servers        = ["10.0.0.4", "10.0.0.5"]
  }


  virtual_machine2  = {
    vnet_address_range = "10.1.0.0/16"
    subnet_values      = ["10.1.1.0/24", "10.1.2.0/24"]
    dns_servers        = ["10.1.1.4", "10.1.1.5"]
  }

}
```

`main.tf`

```
  subnet {
    name             = "backend"
    address_prefixes = [local.virtual_machine1.subnet_values[1]]
    security_group   = azurerm_network_security_group.nsg.id
  }
```

## Output

![alt text](images/{201824D1-61F7-4479-9F9D-80954A43E29B}.png)

## Create Public IP Address

![alt text](images/{258D00BE-25DA-4DC4-A2D0-2FD43484F0E9}.png)

![alt text](images/{FFE1BB8E-4707-48EC-83EE-9A07BDDEA1BA}.png)

```
resource "azurerm_public_ip" "example" {
  name                = "acceptanceTestPublicIp1"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  allocation_method   = "Static"

  tags = {
    environment = "Production"
  }
}
```

## How to associate NSG to Subnet

```
resource "azurerm_subnet_network_security_group_association" "example" {
  subnet_id                 = azurerm_subnet.example.id
  network_security_group_id = azurerm_network_security_group.example.id
}
```

## Create a VM

![alt text](images/{8AB30215-91AC-4599-A888-BA7BAA30CF7E}.png)

![alt text](images/{808D10C0-E78F-49DB-80C4-DE3659E0FF4E}.png)

![alt text](images/{51C13E17-CC2A-45CA-B30C-D6CF3297DEC7}.png)

## Look at your state file

`terraform.tfstate`

## Using input variables

`variables.tf` at the project root

![alt text](images/{9B5F6379-62F3-4056-B449-324506F19CB2}.png)

```
terraform plan -out main.tfplan -var-file="vars/dev.tfvars"

terraform apply "main.tfplan"

terraform destroy -var-file="vars/dev.tfvars"
```

![alt text](images/{54E136AB-48C1-4D4A-9617-3B410B193348}.png)

## Passing secret Values

```
variable "admin_password" {
  type        = string
  description = "This is admin password for the virtual machine"
  sensitive   = true
}

```

```
 admin_password = var.admin_password
```

## Add data disk to VM

1. `azurerm_managed_disk`

   ![alt text](images/{49796F14-4479-41AE-A1BD-F199FB38BBB5}.png)

   ![alt text](images/{41D2E605-3E84-4EE2-8985-D4E821F79E91}.png)

   ```
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
   ```

2. `azurerm_virtual_machine_data_disk_attachment`

   ```
   resource "azurerm_virtual_machine_data_disk_attachment" "vm_datadisk" {
   managed_disk_id    = azurerm_managed_disk.datadisk.id
   virtual_machine_id = azurerm_virtual_machine.vm.id
   lun                = "10"
   caching            = "ReadWrite"
   }
   ```
