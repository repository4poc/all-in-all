# Type

IaaC

[Go Back Home](../README.md)

## How to create a Virtual Machine

**Project Details**

- Subscription :
- Resource Group :

**Instance Details**

- VM Name :
- Region :
- Availability Options:
  - No Infrastructure Redundency
  - Availability Zone
  - Availability Set
  - Virtual Machine Scale Set
- Image:
  - Ubuntu Server 24.04 LTS - x64 Gen2
- Run with Azure Spot discount : Disable (Default) ## TIP: Cost Saving Feature
- Size:
  - Standard_B2ms - 2 vcpu, 8 GiB memory ($63.07)

**Administrator Account**

- Authentication Type :
  - SSH public key : Default
    - SSH Key Type : RSA SSH Format
    - Username :
    - SSH public key source
      - Generate new key pair
      - Use existing public key
  - Password
    - User Name
    - Password

**Inbound port rules**

- public inbound ports: Allowed selected ports ## TIP: Security Feature
  - Selected inbound ports
    - SSH(22) ## This creates a NSG and allow all IP addresses to access your virtual machine via port 22 (It should be avoided and only specific Ips shoud be allowed)

    #Security Best practice

**VM disk encryption**

- Encryption At Host : Disabled by default ## Tip : Security Feature

Azure CLI

```
az feature register \
  --namespace Microsoft.Compute \
  --name EncryptionAtHost
```

PowerShell

```
Register-AzProviderFeature `
  -FeatureName "EncryptionAtHost" `
  -ProviderNamespace "Microsoft.Compute"
```

Terraform itself cannot register Azure subscription features such as Microsoft.Compute/EncryptionAtHost. Feature registration is typically a one-time subscription administration task, so most teams perform it manually.

**OS disk**

- OS disk Size: 127 GB To 2 TiB
- OS disk Type:
  - Local Redundant Storage
    - Standard SSD
    - Premium SSD
    - Standard HDD
  - Zone Redundant Storage
    - Standard SSD
    - Premium SSD
- Delete with VM : Enabled (Default) ## Tip: Security feature, make it disable
- Key Management : For Disk encryption key
  - Platform Managed (Default)
  - Customer Managed
- Enabled Ultra Disk Capability : Disabled (Default)
  Ultra Disk is suited for data-intensive workloads such as SAP HANA, top tier databases, and transaction-heavy workloads. Adding this capability on results in a reservation charge that is only imposed if you enabled Ultra Disk capability on the VM without attaching an Ultra Disk.

**Data disks**

- Add new disk
  - LUN
  - Name
  - Size (GiB)
  - Disk type
  - Host caching
  - Delete with VM

**Network interface**

- Virtual network\* : (Required Field)
- Public IP
- NIC network security group :
  - Basic (Default)
  - Advanced
- Public inbound ports\* : (Required Field)
  - Select inbound ports\* :
    - HTTP (80)
    - HTTPS (443)
    - RDP (3386)
    - SSH (22) ## Tip : This will allow all IP addresses to access your virtual machine. This is only recommended for testing. Use the Advanced controls in the Networking tab to create rules to limit inbound traffic to known IP addresses.
- Delete NIC when VM is deleted : Disabled (Default)
- Enable accelerated networking : Enables low latency and high throughput on the network interface
  - This feature is only enabled for selected VM Types

**Load balancing**

- Load balancing options
  - None (Default)
  - Azure Load Balancer : Supports all TCP/UDP network traffic, port-forwarding, and outbound flows.
  - Azure Application Gateway : Web traffic load balancer for HTTP/HTTPS with URL-based routing, SSL termination, session persistence, and web application firewall.

**Identity**

- System assigned managed identity
  : Disabled (Default)

- Login with Microsoft Entra ID: Disabled (Default)

- Auto-shutdown : Disabled (Default) Configures your virtual machine to automatically shutdown daily ## Tip : Cost Saving Feature
  - Shutdown time
  - Time Zone
  - Notification before shutdown
    - Email ID:

**Backup and Recovery**

- Backup : Disabled (Default) ## Tip : Security Feature
  - Recovery Services vault
  - Policy subtype : Enhanced
  - Backup Policy
    - Policy name
    - Backup schedule
      - Frequency : Hourly(scheduled)/Daily(once)/Weekly(once)
        - Schedule : Every 4/6/8/12Hr
        - Retain instant recovery snapshot(s) for : 5-17 days (7 days Default)
        - Retention of backup point : 30 days (Default)

- Site Recovery : Disabled (Default) : Azure Site Recovery helps to keep your virtual machines running during outages. Enable it to replicate your virtual machine to a secondary Azure region
- Guest OS updates : Enabled (Default)

**Monitoring**

- alert rules : Disabled (Default)
- Diagnostics : Enabled (Default)
  - Boot Diagnostics
    - Enable with managed storage account (Default)
- Application health monitoring : Disabled (Default)

**Advanced**

- Extensions : Disabled (Default) : Extensions provide post-deployment configuration and automation
- Custom data : Pass a script, configuration file, or other data into the virtual machine while it is being provisioned. The data will be saved on the VM in a known location.
- User data : Pass a script, configuration file, or other data that will be accessible to your applications throughout the lifetime of the virtual machine.
- Proximity placement group : allow you to group Azure resources physically closer together in the same region.

**Tags**

- Name
- Value

## Azure Proximity Placement Group vs Azure Availability Set

| Feature        | Availability Set                                                 | Placement Group                                         |
| -------------- | ---------------------------------------------------------------- | ------------------------------------------------------- |
| Purpose        | Improve VM availability during maintenance and hardware failures | Control physical placement of VMs for performance/scale |
| Scope          | High availability                                                | Physical deployment grouping                            |
| Fault Domains  | Yes                                                              | No                                                      |
| Update Domains | Yes                                                              | No                                                      |
| SLA Benefit    | Yes (when using 2+ VMs)                                          | No                                                      |
| Typical Use    | Web servers, application servers, databases requiring uptime     | Large VM Scale Sets, HPC, low-latency workloads         |
| User Managed   | Yes                                                              | Usually managed by Azure                                |

#

# Costing include

    1. Compute Cost (RAM + CPU)
       Fix: Choose right VM size
       - standard B2/D2/F2
    2. Storage Cost (Boot disk + Data disk)
        Fix: Choose right disk type (SSD Standard, SSD Premium, HDD Standard)
    3. Network Cost
        - Data Transfer outside Azure
          Fix: Use Private Endpoint
        - Load Balander / Public IP
        - VPN / Express Gateway
    4. Backup Cost
        - LRS, GRS
    5. Monitoring Cost
    6. Logging Cost
        - Log Analytics
        - Log Retention
    7. Licensing Cost
       Fix: Use Linux VM
    8. Availability Cost

### Note :

- We donot have 'subnet' resource type, although we have NSG, vNET, vNIC resource types.
- We can resize (RAM & CPU) and existing VM (resize option)

### Linux VM

```
(apt is a package manager for linux)
1. sudo apt update
2. sudo apt install aspnetcore-runtime-10.0

```

#### These are the standard IPv4 private network ranges defined by Internet Engineering Task Force RFC 1918

| Range                           | CIDR             | Size              |
| ------------------------------- | ---------------- | ----------------- |
| `10.0.0.0 – 10.255.255.255`     | `10.0.0.0/8`     | ~16.7 million IPs |
| `172.16.0.0 – 172.31.255.255`   | `172.16.0.0/12`  | ~1 million IPs    |
| `192.168.0.0 – 192.168.255.255` | `192.168.0.0/16` | 65,536 IPs        |

Common examples:

Home routers → 192.168.1.x
Corporate networks → 10.x.x.x
Cloud VPCs → often 10.x.x.x or 172.16.x.x

### From a cost + latency perspective in Finland, the best Azure region today is usually:

Recommended: Sweden Central

Why:

Closest major Azure region to Finland
Lower latency from Helsinki (~18–20 ms)
Usually cheaper than Norway regions
Good service availability
Inside EU geography/data residency boundaries

#### Usage and Quota (In each Subscription)

To know each service quota for each subscription
