## Overview

- A Network Resource
- Created inside a Subnet

## Difference between Azure LB and Azure Application Gateway

| Feature                  | Azure Load Balancer  | Azure Application Gateway          |
| ------------------------ | -------------------- | ---------------------------------- |
| OSI Layer                | Layer 4 (TCP/UDP)    | Layer 7 (HTTP/HTTPS/WebSocket)     |
| Traffic Awareness        | Doesn't inspect HTTP | Understands URLs, headers, cookies |
| SSL/TLS Termination      | No                   | Yes                                |
| URL-based Routing        | No                   | Yes                                |
| Host-based Routing       | No                   | Yes                                |
| Web Application Firewall | No                   | Yes (WAF SKU)                      |
| Session Affinity         | Limited              | Cookie-based affinity              |
| Best For                 | Any TCP/UDP workload | Web applications and APIs          |
| Cost                     | Lower                | Higher                             |

**URL Based Routing**

![alt text](images/{4CC19D7F-1CDB-4568-9A5A-379470295A05}.png)

![alt text](images/{6436DC5F-DA3B-4DDE-B48F-A92C6678E52C}.png)

**Domain Based Routing**

![alt text](images/{9040F6F2-7644-41C8-AB3C-8E6671AA8A66}.png)

**Azure Public Load Balancer**

Use when you need to distribute network traffic: It only looks at IP addresses and ports.

Examples:

- RDP (3389)
- SSH (22)
- Custom TCP applications
- Game servers
- Database proxies

**Azure Application Gateway**

Use when you need intelligent web traffic routing:

```
Internet
    |
Application Gateway
    |
+----------------+
| URL Routing    |
+----------------+
   |         |
 /api      /shop
   |         |
Backend1  Backend2


/api   -> API Servers
/admin -> Admin Portal
/shop  -> E-commerce App
```

## How to create Azure Application Gateway

![alt text](images/image-1.png)

**Project details**

- Subscription:
  - Resource Group:

**Instance Details**

- Application gateway name :
- Region
- Tier : Standard (Default)
  - Basic
  - Standard V2
  - WAF V2

**Standard Tier Feature**

- Enable autoscaling : Enabled (Default)
  - Minimum instance count
  - Maximum instance count

**Configure virtual network**

- Virtual Network
  - Subnet

**Frontends**

- Frontend IP Address Type : Public (Default)
  - Public
    - IPV4 IP Address : < Choose >
      - SKU : Standard Only
      - Assignment : Static Only
      - Availablity : Zone Redundancy
  - Private
  - Both

**Backends**

- Backend Pool
  ![alt text](images/{FCC782C7-F1A1-4D82-910F-713B5D4010B4}.png)

**Configuration**
![alt text](images/{8E111DEA-8DDC-4B5C-8868-7BEFD342F3D1}.png)

- Listener
  ![alt text](images/{784F7113-D23E-4432-BD01-094EAFC1820B}.png)

- Backend Targets
  ![alt text](images/{5D01D46D-216F-4A39-99A7-5AE75D5C80E4}.png)
  ![alt text](images/{8BC908DC-BD0B-4A93-BD90-BEDFB7C98010}.png)
  ![alt text](images/{C564E24F-D59F-4162-8B7D-950B7323F28F}.png)

- Path Based Routing Rules

![alt text](images/{4AAF7064-4066-4AFC-AF1B-C26A953EA2B9}.png)
![alt text](images/{A015F4CA-694F-47C3-A52D-C202F967473A}.png)
![alt text](images/{58B68C5F-4C1D-4414-8969-A1E1B087B659}.png)

![alt text](images/{F3EF6417-54E7-4F66-935B-2E7A267EDB6E}.png)

**Tags**

- Name/Value

## Azure Web Application Firewall

An additional feature available on Azure Application Gateway and Azure Frontdoor service
![alt text](images/{D4B93AB0-218B-4485-A4AC-733362060AD7}.png)

Protects against

- SQL Injection Attacks
- Cross Site Scripting Attacks

  ![alt text](images/{77857B5D-4672-4AFD-8E03-4DD45EE63454}.png)

- After you upgrade to WAF Tier, just enable it
  ![alt text](images/{081CD0F3-E8E2-4F33-ACB4-AC7A66C6CF9C}.png)
  ![alt text](images/{201FF2FB-7E56-4CC4-94A1-576B61283638}.png)

- WAF Mode
  - Detective (Default)
  - Preventive

    ![alt text](images/{0D039852-B530-44B5-A3BE-CD6ED2CE9E1D}.png)

- Enable Rules
  ![alt text](images/{14214348-8968-4AAB-9FDE-A039EF545B6E}.png)
