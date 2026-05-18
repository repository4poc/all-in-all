# Type

IaaC

[Go Back Home](../README.md)

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
