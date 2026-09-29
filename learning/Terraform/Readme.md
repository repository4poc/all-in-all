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

![alt text](images/{FC63351E-1098-4BAD-9E1B-C3EC015706CB}.png)

- local.virtual_network.name
- local.virtual_network.address_prefixes[0]
- local.subnet_address_prefix[0]
- local.subnet_address_prefix[1]
- local.subnets[0].name
- local.subnets[0].address_prefixes[0]
- local.subnets[0].name
- local.subnets[1].address_prefixes[1]

![alt text](images/{488ECF85-3E69-40FD-B21D-96F2A2D322E9}.png)

![alt text](images/{420A1AAE-B8D2-4606-A857-AD448171FFF6}.png)

## Create Public IP Address

![alt text](images/{258D00BE-25DA-4DC4-A2D0-2FD43484F0E9}.png)

![alt text](images/{FFE1BB8E-4707-48EC-83EE-9A07BDDEA1BA}.png)

![alt text](images/{CC5A4769-52C6-4676-B55C-27E2F0F8FEB7}.png)

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

## Count - meta argument

It is used to create multiple similar infrastructure

- count=3
- count.index (0,1,2)

![alt text](images/{12EB8D1D-20F2-4B88-AD66-88FAADE316FB}.png)

![alt text](images/{330ED79D-BB75-4694-88F8-66F2D440F066}.png)

![alt text](images/{9676D726-6B14-4247-9A23-A3B85BC616EE}.png)

## For each - meta argument

It is used to create multiple distinct/different infrastructure using

```
foreach = toset(["value1","value2"])

each.key
```

```
foreach = tomap({"key"="value","key","value})

- each.key
- each.value
```

`toset(["value1","value2"])`

![alt text](images/{D33593C7-C559-4187-BBC6-CEC5DB5AD138}.png)

OR

`tomap({"key"="value","key","value})`

![alt text](images/{88F5B7CB-EAEA-4487-A198-6D2FE9E3BAC7}.png)

## for each and variable

`variables.tf`

![alt text](images/{74B36844-C330-4C22-922A-9B4F8BC2985B}.png)

`dev.tfvar`

![alt text](images/{72EA4765-AD81-420F-86F0-9CFEEEF09FCE}.png)

`main.tf`

![alt text](images/{5D22983B-216B-43C4-BE54-55D6DBFDFC88}.png)

![alt text](images/{345186D3-E009-4D92-8228-5327AE783098}.png)

![alt text](images/{68865898-4C09-4110-A893-70BEAB8CFF5F}.png)

`Correction`

![alt text](images/{F4D78F98-434F-4478-9D4D-C61AD4B653AB}.png)

## High Availability Approaches (For VM)

![alt text](images/{6FC8F5CF-BB12-4DE3-9251-A856C60C21C8}.png)

### Problem Statement

![alt text](images/{7A439CD3-9FA8-4245-8EBA-E61601D9C360}.png)

### Solution

- Availability Set
- Availability Zone

`Availability Set (Resource)`

- All VMs resides in the same data center.

- Fault Domain (Group of VM share same poweer source and network)
  - 3 (Max)
- Update Domain (Group of VM can be rebooted at same time) - 20 (Max)
  ![alt text](images/{981E2382-8FED-49DC-B706-4F6B0FDCC0BD}.png)

  ![alt text](images/{44EF1A88-45DA-4DAF-B755-3E10AC65B2E5}.png)

  ![alt text](images/{763440D8-2C40-41E8-903F-AC9234784EF6}.png)

  ![alt text](images/{E59C6CE4-3E12-4538-904E-9247CBD8D090}.png)

  ![alt text](images/{B22AC490-6BB6-48E0-AB7F-CA1CE22FE116}.png)

  ![alt text](images/{AFE4F800-C8C9-4E6B-A62B-EF24840B0B08}.png)

  ![alt text](images/{95BA1B3F-261F-44BD-AF40-B4AA93BAE7D4}.png)

  ![alt text](images/{96FA9FD7-2B32-4B07-A90B-F17D4C83DA4A}.png)

`Availability Zone`

- An availability zone is Group of data centers.

  ![alt text](images/{D562210F-511F-4B5F-ACCE-E7B887DD4F13}.png)

  ![alt text](images/{0350337C-60A8-48E6-B605-054ACBF43518}.png)

  `zone` : 1,2,3

  ![alt text](images/{7C3CB465-830B-447C-B6EC-FB1B7DDD69E9}.png)

## Azure Key Vault

- Stores
  - Secrets
  - Encryption Keys
  - Certificates

![alt text](images/{51B9935D-9947-4AE9-A457-C09BB5C8115C}.png)

### Soft Delete vs Purge Protection

Example

Suppose you delete a cryptographic key from a vault:

With soft delete enabled

1. You delete the key
2. The key moves to a "deleted" state.
3. You can recover it within the retention period (for example, 90 days).
4. An authorized user may still be able to purge (permanently remove) it before the retention period ends.

With soft delete + purge protection enabled

1. You delete the key.
2. The key moves to the deleted state.
3. You can recover it during the retention period.
4. Nobody can permanently purge it until the retention period expires.
5. After the retention period, the service removes it automatically.

| Feature                                    | Soft Delete                                         | Purge Protection                                                        |
| ------------------------------------------ | --------------------------------------------------- | ----------------------------------------------------------------------- |
| Purpose                                    | Allows recovery of deleted items                    | Prevents permanent deletion during retention period                     |
| What happens when deleted?                 | Item is marked as deleted and retained for a period | Item remains in the soft-deleted state and cannot be permanently erased |
| Can it be restored?                        | Yes, during the retention period                    | Yes, during the retention period                                        |
| Can it be permanently deleted immediately? | Usually yes, if purge is allowed                    | No, purge is blocked until retention period expires                     |
| Main protection against                    | Accidental deletion                                 | Malicious or accidental permanent deletion                              |

![alt text](images/{17D80E38-9751-4524-B128-2C1EC90ED74A}.png)

`Permission Model`

In Azure we use RBAC permission model

´´´
resource "azurerm_key_vault" "example" {
name = "examplekeyvault"
location = azurerm_resource_group.example.location
resource_group_name = azurerm_resource_group.example.name
rbac_authorization_enabled = false
enabled_for_disk_encryption = true
tenant_id = data.azurerm_client_config.current.tenant_id
soft_delete_retention_days = 7
purge_protection_enabled = false

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
´´´

![alt text](images/{0DA08445-9076-419A-9561-68451B251CD8}.png)

![alt text](images/{27FB892E-118A-4AD3-8D2A-AF306576FCC2}.png)

![alt text](images/{7BE3BC58-1BA1-40FE-BEDD-13B991A0A322}.png)

The are two ways of Authorization in key vault

- RBAC
- Access Policy

| Feature                | Access Policies                                  | Azure RBAC                             |
| ---------------------- | ------------------------------------------------ | -------------------------------------- |
| Authorization scope    | Key Vault only                                   | Azure-wide authorization model         |
| Permission assignment  | Individual permissions (Get, List, Delete, etc.) | Roles (e.g., Key Vault Secrets User)   |
| Management             | Configured inside Key Vault                      | Managed through Azure IAM              |
| Granularity            | Fine-grained per operation                       | Role-based, can be scoped              |
| Integration            | Legacy model                                     | Recommended modern model               |
| Separation of duties   | Limited                                          | Better support                         |
| Consistency with Azure | Separate experience                              | Same model used across Azure resources |

![alt text](images/{474B4A0F-8EEC-42C5-ABF4-F10183491682}.png)

In case you need to create secrets in the key vault using terraform Application Object, you need to assign the `Secret Officer` Role to the Application Object on the Key Vault Resource

![alt text](images/{D5F583E4-C507-4E4C-9C86-BC8F08949058}.png)

## Using Variables as Object

`variables.tf`

![alt text](images/{D65E00F8-059F-4F7A-AC01-894ACE20CC65}.png)

`dev.tfvars`

![alt text](images/{BF419236-C5AF-4582-8FE8-E5BB1A03011D}.png)

`main.tf`

![alt text](images/{CA3D4760-1A10-4842-975B-16FE05D07D15}.png)

## Using Data Sources

In case we need to refer in terraform menifest file a pre-existing resource like key-vault, that is not terraform managed resource.

`Usee Data Block`

![alt text](images/{D4BCE4F6-C7D1-405E-BB7D-0439FBDC12FE}.png)

![alt text](images/{E40F671F-A105-4CF5-881A-FA3101365E8E}.png)

![alt text](images/{5BD0125B-B999-4B84-A952-D8DAD0315E27}.png)

## Create Azure Key Vault Secret

```
resource "azurerm_key_vault_secret" "example" {
    name = "secret-sauce"
    value = "szechuan"
    key_vault_id = azurerm_key_vault.example.id
}
```

![alt text](images/{72F895E0-2E00-4BCB-AB4A-CA11669E806C}.png)

`Refer secret from Key Vault in VM`

![alt text](images/{D61CBE51-C8A0-4955-8FDD-83E203FB32AA}.png)

In an enterprise Terraform setup, the usual pattern is:

- Generate or provide the password once.
- Store it in Azure Key Vault.
- Read it from Key Vault whenever infrastructure needs it.
- Never prompt for it during normal pipeline runs.
- Prevent Terraform from recreating it unless explicitly intended.

### Option 1: Terraform generates the password once (common)

```
resource "random_password" "vm_admin" {
  length  = 24
  special = true
}

resource "azurerm_key_vault_secret" "vm_admin_password" {
    name         = "vm-admin-password"
    value        = random_password.vm_admin.result
    key_vault_id = azurerm_key_vault.main.id
}
```

Use it in the VM:

```
resource "azurerm_windows_virtual_machine" "vm" {
    name                = "vm01"
    admin_username      = "adminuser"
    admin_password      = azurerm_key_vault_secret.vm_admin_password.value
}
```

Why it doesn't regenerate every run

The random_password value is stored in Terraform state
On subsequent runs:

```
terraform plan
```

Terraform sees the resource already exists in state and keeps the same password.

This is why the Terraform state backend must be durable (Azure Storage Account with remote state).

### Option 2: Security team manually creates the secret (very common in enterprises)

```
data "azurerm_key_vault_secret" "vm_admin_password" {
  name         = "vm-admin-password"
  key_vault_id = azurerm_key_vault.main.id
}
```

Use it:

```
resource "azurerm_windows_virtual_machine" "vm" {
  admin_username = "adminuser"
  admin_password = data.azurerm_key_vault_secret.vm_admin_password.value
}
```

Advantages:

- Password lifecycle owned by security team.
- Terraform never knows how the password was generated.
- Password rotation can happen outside Terraform.

Many regulated enterprises prefer this model.

So in each subscription-dev/test/prod, we have separate resource group, shared-resources having (Pre-Existing)

- storage account to store the terraform state
- key vault with secret

## Terraform vs Ansible

- Terraform : Infrastructure management
- Ansible : Configuration management

## Custom Script Extension (CSE) in Virtual Machine

CSE : Allow you to run scripts when the VM is first created or `bootstrap` your new machine with application like webserver.

- Set up webserver with default home page

## Build VM with webserve using Terraform

1. Create Bootstrap script

   `Powershell.ps1`

   ![alt text](images/{6929F153-1672-4342-A166-A6B2043B3FC4}.png)

   ![alt text](images/{220D9717-2AF3-45BE-8707-6BD55A24577B}.png)

2. Store the `Bootstrap` powershell script into the Storage Account

   ![alt text](images/{A3545FFC-F349-4513-967B-9F95F95660E2}.png)

3. Install the CSE in VM with Script

   ![alt text](images/{FAA7E7A9-5AFF-4D9C-AF60-B57A3DC3B5F1}.png)

   ![alt text](images/{9133E85F-0523-45A6-A8BA-98637C2FC3D5}.png)

## Dynamic Block

`Problem Statement`

Long Security Rules list

![alt text](images/{C5EA3DAE-8812-4611-A688-F7476E2C50FD}.png)

`Solution`

Dynamic Block

![alt text](images/{97D99B25-5D68-4A9C-B641-1DB22819A49B}.png)

![alt text](images/{18940941-49F9-4B46-8D3A-453B7A3AA689}.png)

## Reading a local file

![alt text](images/{8AFBA49E-AB0E-4A52-980C-CC136601CB91}.png)

## Build a linux machine with Nginx Web Server

Similar to IIS web Service on Windows.

We can install different package on Linux VM, one of the package is NGiNX

### Enterprise-standard approaches

`Linux`

1. Store the script in a separate file

```
terraform/
├── main.tf
├── variables.tf
├── linux/
│   └── bootstrap.sh.tpl (bootstrap.sh)
└── windows/
    └── bootstrap.ps1.tpl
```

```
# bootstrap.sh.tpl
#!/bin/bash

apt-get update
apt-get install -y nginx
```

```
custom_data = base64encode(
  templatefile("${path.module}/linux/scripts/bootstrap.sh.tpl", {
    environment = var.environment
  })
)
```

This works because Linux VMs can execute shell scripts (often via cloud-init).

So the rule of thumb is:

```
No Terraform variables needed
        ↓
bootstrap.sh
        ↓
file()

Terraform variables needed
        ↓
bootstrap.sh.tpl
        ↓
templatefile()
```

## Linux based machine

![alt text](images/{DDEAF71A-DAA3-48B6-A541-F06A94AD7488}.png)

![alt text](images/{7AC342AB-A074-4382-8A3E-E7A1626BE1E3}.png)

`Make sure to disable the password authentication`

## Provisioners

Used to execute actions on local or remote machine

`To have default.html page`

![alt text](images/{B971C7EE-489B-4176-B266-AE2C3AE384A6}.png)

![alt text](images/{E0DF7BD8-A058-4858-B536-056050587EDE}.png)

## Azure Bastion

![alt text](images/{8FCE5584-B85B-4AAA-8F57-5D7DA1EE3DE9}.png)

![alt text](images/{99F61005-DE6F-4266-8632-47FA093E99D7}.png)

1. AzureBastionSubnet

   ![alt text](images/{24CAD03C-8315-4328-9D3E-9FE84D83568D}.png)

2. PublicIPAddress

   ![alt text](images/{57739AC1-5BBC-4A51-A670-2B963CDEECF0}.png)

3. Bastion

   ![alt text](images/{1D24D9CC-5041-4391-8A7B-7D90552BC210}.png)

4. Connect to the VM using Bastion

   ![alt text]({92798775-1D05-40BF-90FB-52DFC317A38A}.png)

## Azure Load Balancer

![alt text](images/{38C3A621-B392-41BD-9DDA-33BFBBAF8A21}.png)

![alt text](images/{EFDD82F7-9FFB-4F3B-8B2F-FF9062D4F9B6}.png)

![alt text](images/{12580CF2-47AD-4BCF-A017-94A45E289FC2}.png)

![alt text](images/{2EB59ED8-4DE5-462E-A106-138A00767EC3}.png)

- Frontend IP
  - Public IP 1
  - Public IP 2
- Backend Pool
  - Virual Machine A
  - Virual Machine B
  - Virual Machine C
- Health Probe
  - endpoint : /
  - Port :
- Loadbalancing Rules
  - Frontend IP
  - Frontend Port
  - Backend Pool
  - Backend Port
  - Health Probe
  - Session Persistence

## Terraform Modules

Module : Reusable Code.

![alt text](images/{8A722E98-A537-46B4-9BBA-4EC7968DFCCE}.png)

![alt text](images/{B41263E1-A52A-49E4-B534-0E02D4E57AF5}.png)

![alt text](images/{921AD244-EB7E-44FD-A3B8-4982E13F47EA}.png)

Each module folder has

- main.tf
- variables.tf
- outputs.tf

`main.tf`

```

module "acr" {
  source              = "./modules/containers/acr"
  resource_group_name = azurerm_resource_group.rg.name
  region              = var.region
  tags                = var.tags
}

```

The main module can not access the data in the child modules

## How information is passed ffrom one module to another

Using output

1. From sending module make sure you output that information into a variable.

   ![alt text](images/{F8E59E02-377D-4FDA-8588-28C90BAA50C5}.png)

2. In the receiving module, declare the variable and pass the information as variable

   ![alt text](images/{BFD4C6C1-513C-4273-97EE-B2293929F134}.png)

   ![alt text](images/{BE15B63C-CB5E-4573-A354-F07E3DD0779C}.png)

3. Use it in the reciving resource

   ![alt text](images/{C125EAC5-CEE3-4EBC-8DE9-478400EB1ECC}.png)

![alt text](images/{FA82F754-12C2-4DB3-8395-4ACECAF424F0}.png)

### Azure Loadbalancer connect to the private ip address of virtual machine.

## VM Scaleset

![alt text](images/{A606043A-88A0-423E-9F1D-9D8D1345C527}.png)

- You define scaling conditions

![alt text](images/{98DF17D3-6A27-40F5-A973-2D0CBF1A445F}.png)

Load Balancer

- Backend Pool
  - VMs
  - Scaleset

## Azure Traffic Manager

DNS based Global Load Balancer

![alt text](images/{2F1380E4-D388-4CD1-9FF6-4DDA6E26189F}.png)

![alt text](images/{9FEE3D99-54AE-4D9D-B0B1-3E38E306B786}.png)

- Create Traffic Manager Profile
  - Configure
    - Request Type : Priority/Weighted
    - Protocol: HTTP/HTTPS(choose)/TCP
    - Port : 80/443(select)/<custom>
    - PATH : /
  - Endpoints
    - Endpoint Type
      - Azure Endpoint
      - External Endpoint
      - Target Resource Type
        - Cloud Service
        - App Service
        - App Service Slot
        - IP Address
      - Target Resource
      - Custom Header
        - host:<App Service Endpoint>.azurewebsite.net

      https://<name>.trafficmanager.net

`variables.tf`

![alt text](images/{FFE54EC9-AF7F-4F54-93EB-D213D6A496E3}.png)

![alt text](images/{24005566-9933-4824-A363-5AB825485039}.png)

`modules/web/main.tf`

![alt text](images/{CB38F5D9-3B95-4E73-BB99-9395779E43D5}.png)

`main.tf`

![alt text]({B1A35390-AD4F-4115-A733-12DBAB5D8949}.png)

| Feature                 | Azure Load Balancer    | Application Gateway | Traffic Manager | Azure Front Door        |
| ----------------------- | ---------------------- | ------------------- | --------------- | ----------------------- |
| Layer                   | L4 (TCP/UDP)           | L7 (HTTP/HTTPS)     | DNS             | L7 (HTTP/HTTPS)         |
| Scope                   | Regional               | Regional            | Global          | Global                  |
| Traffic Path            | In path                | In path             | DNS only        | In path                 |
| WAF                     | No                     | Yes                 | No              | Yes                     |
| SSL Termination         | No                     | Yes                 | No              | Yes                     |
| URL Routing             | No                     | Yes                 | No              | Yes                     |
| Global Failover         | No                     | Limited             | Yes             | Yes                     |
| CDN/Edge Acceleration   | No                     | No                  | No              | Yes                     |
| Private Backend Support | Limited                | Yes                 | N/A             | Yes (Premium)           |
| Best For                | Network load balancing | Regional web apps   | DNS routing     | Global web applications |

I would choose Traffic Manager when I need DNS-based global routing or failover and don't need a reverse proxy. I would choose Front Door for global HTTP/HTTPS applications where I need the traffic to pass through an Azure edge service for features such as Layer-7 routing, WAF, TLS termination, caching, and acceleration.

| Your requirement                            | Usually consider    |
| ------------------------------------------- | ------------------- |
| Global web application                      | **Front Door**      |
| Global HTTP/HTTPS + WAF                     | **Front Door**      |
| Global HTTP/HTTPS + edge acceleration       | **Front Door**      |
| Simple DNS failover                         | **Traffic Manager** |
| DNS-based geographic routing                | **Traffic Manager** |
| DNS-based priority routing                  | **Traffic Manager** |
| Need actual HTTP request inspection/routing | **Front Door**      |

Traffic Manager = DNS traffic director
Front Door = global web traffic proxy

```

                         Traffic Manager
                         /              \
                        /                \
                       ↓                  ↓
                Front Door A        Front Door B
                Primary platform   DR platform
                       |                  |
                +------+-----+      +-----+------+
                |            |      |            |
              Region 1    Region 2 Region 3   Region 4
```

## Application Gateway

![alt text](images/{4E941357-A3D4-4770-9843-7A25243B4E84}.png)

So Application Gateway based on the URL, direct the request to desired backend Pool

Application Gateways uses its dedicated subnet to provision resources that perform the routing

![alt text](images/{61581608-5378-442C-9F14-03414229A491}.png)

```
                         Internet
                            |
                            v
                     Azure Front Door
                      GLOBAL L7
                            |
              +-------------+-------------+
              |                           |
              v                           v
       West Europe                   East US
       REGIONAL                     REGIONAL
              |                           |
      Application Gateway          Application Gateway
          (optional)                  (optional)
              |                           |
       Azure Load Balancer         Azure Load Balancer
              |                           |
          VM1  VM2 VM3               VM4 VM5 VM6
```

If you need URL routing/WAF at the regional level, you need to have Application Gateway (Mandatory) before Azure Load Balancer

```
                         GLOBAL
                           │
                           ▼
                  ┌─────────────────┐
                  │  Azure Front    │
                  │     Door        │
                  │ Global L7/WAF   │
                  └────────┬────────┘
                           │
              ┌────────────┴────────────┐
              │                         │
              ▼                         ▼
       WEST EUROPE                  EAST US
        REGIONAL                    REGIONAL
              │                         │
       ┌──────▼──────┐           ┌──────▼──────┐
       │Load Balancer│           │Load Balancer│
       └──────┬──────┘           └──────┬──────┘
              │                         │
        ┌─────┼─────┐             ┌─────┼─────┐
        ▼     ▼     ▼             ▼     ▼     ▼
       VM1   VM2   VM3           VM4   VM5   VM6
        │     │     │             │     │     │
      AZ1   AZ2   AZ3           AZ1   AZ2   AZ3
```

```
                         USERS
                           │
                           ▼
                    Azure Front Door
                   Global HTTP/HTTPS
                    WAF / Routing
                           │
             ┌─────────────┴─────────────┐
             │                           │
             ▼                           ▼
       WEST EUROPE                    EAST US
       App Service                   App Service
       Web App                       Web App
             │                           │
       Multiple instances          Multiple instances
```

So we dont not need Application Gateway in front of WebApp

```
❌ Front Door
      ↓
  Application Gateway
      ↓
  App Service
```

- Front Door → global traffic
- App Service → regional web hosting/scaling
- Application Gateway → optional; use when you have a specific regional gateway requirement
- Traffic Manager → generally unnecessary if Front Door is already your global web entry point.-

```
                    Traffic Manager
                          |
              +-----------+-----------+
              |                       |
              v                       v
       App Gateway East         App Gateway West
              |                       |
        +-----+-----+             +---+-----+
        |           |             |         |
      /api        /web          /api       /web
        |           |             |         |
      VMSS        VMSS          VMSS      VMSS
```

```
                    Internet
                       |
                       v
                Traffic Manager
                 (DNS routing)
                       |
              +--------+--------+
              |                 |
              v                 v
       App Gateway A      App Gateway B
              |                 |
       +------+------+    +-----+------+
       |      |      |    |     |      |
      VM1    VM2    VM3  VM4   VM5    VM6
```

![alt text](images/{C4529E10-0655-40A7-81BF-70C156A3FA21}.png)

## What is a Landing Zone?

A Landing Zone is a pre-configured cloud environment that provides the

- Foundational infrastructure
- Governance
- Security controls
- Networking
- Operational capabilities

Required for an organization to

- safely and
- consistently

deploy workloads in the cloud.

## Landing Zone Components

1. Identity and access management
2. Network architecture (hub-spoke, shared services, connectivity)
3. Security controls and policies
4. Logging and monitoring
5. Compliance and governance standards
6. Account/subscription/project structure
7. Shared platform services

## What is an Application Landing Zone?

An Application Landing Zone (ALZ) is a cloud environment specifically designed and provisioned for a particular application. It inherits the organization’s standards and controls from the Landing Zone.

## Application Landing Zone Components

1. Application-specific networking
2. Compute resources (VMs, containers, serverless services)
3. Databases and storage
4. Secrets and key management
5. Application monitoring and alerting
6. CI/CD integrations
7. Application-specific security configurations

The goal is to provide a ready-to-use environment where a specific application can be deployed and managed while remaining compliant with organizational standards.

| Aspect     | Landing Zone                                                                                             | Application Landing Zone                                                                        |
| ---------- | -------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| Definition | Enterprise cloud foundation that establishes standards, governance, and shared services.                 | Environment tailored for a specific application or workload.                                    |
| Purpose    | Enable secure and scalable cloud adoption across the organization.                                       | Enable deployment and operation of an individual application.                                   |
| Scope      | Organization-wide.                                                                                       | Application-specific.                                                                           |
| Ownership  | Cloud platform/central infrastructure team.                                                              | Application or product team.                                                                    |
| Includes   | Identity, networking, security, governance, monitoring, shared services, account/subscription structure. | Compute, databases, storage, application networking, monitoring, secrets, deployment pipelines. |
| Reuse      | Shared by multiple applications.                                                                         | Usually created per application or workload.                                                    |
| Dependency | Independent foundational layer.                                                                          | Built on top of the Landing Zone.                                                               |

![alt text](images/{8B2664F3-DCC9-4647-A381-C19474AD0542}.png)
