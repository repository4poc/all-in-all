## Virtual Network Peering

- Virtual networks are isolated from each other
  - so resources in vNet1 can not communicate to resource in vNet2 using private IP
- The vNets can be in different regions and subscriptions as well
- we can connect two virtual network with different address range using Virtual Network Peering

## How to create a vNet peering

It will create peering connection from both the vNets.
![alt text](images/{CED2470B-1028-4383-BFE4-B7B9C2B9CFB8}.png)

## Ways to connecting to On-premises networks

Virtual Network Peering , can connect two networks on the cloud, but what if we want to connect to an on-premise network. For this we need to setup VPN connection

VPN Connection Types:

- Point-To-Site VPN Connection
  - This is done using Azure Virtual Network Gateway
  - We attach this Azure Virtual Network Gateway to the Azure Virtual Network
  - We need an empty Gateway Subnet that will be used by the Azure Virtual Network Gateway, to facilitate the Client machine to connect to the resources in the virutal netowrk using private IP
    ![alt text](images/{3CBAF39B-EE14-4BCC-A7CB-B9D369A1E57C}.png)
- Site-To-Site VPN Connection
  - Here On-Premise router connects to the Azure Virtual Network Gateway
    ![alt text](images/{E80715D3-D8B8-4AD0-B869-C481BB2F1A77}.png)
- Azure Express Route :
  ![alt text](images/{F4032B9F-C2BF-42FC-9624-72F036C9C9E2}.png)

**Usecase Scenario** : There are two Virtual machines, one in Azure Cloud and other in the On-premise network and you need to setup communication between them using private ip address.

![alt text](images/{DB187CD9-3048-43B3-BC61-16F8E9BE455E}.png)

The Azure Virtual Network Gateway should be create as part of the Azure Virtual Network with Gateway Subnet, which will contain the resource required for the Azure Virtual Network Gateway to function.

![alt text](images/{035F9D9A-CAA9-4449-ACCD-9417E2848F0C}.png)

## How to Create an Azure Virtual Network Gateway

**Steps**

1. Create Gateway Subnet inside a Azure Virtual Network
   ![alt text](images/{53D99E19-0945-4819-8FF0-564A927EC94C}.png)
   ![alt text](images/{703522E6-4EDF-4767-A91F-E0D89B3A0926}.png)

2. Create Azure Virtual Network Gateway Resource

   **Project Details**
   - Subscription
     - Resource Group

   **Instance Details**
   - Name
   - Region
   - Gateway Type
     - VPN (Default)
       - Route Based (Default)
       - Policy Based
     - Express
   - SKU
     - VpnGw2AZ

- Virutal Network : < Choose Virtual Network >
  - Gateway Subnet : < Choose Gateway Subnet >
- Public IP # This will be assigned to Virtual Network Gateway
  - Create New
    - IP Name :
    - SKU : Standard (Only)
  - Existing
- Availability Zone : Zone Redundant
- Enable active-active mode : Enabled (Default)
  - Enabled
    - Second Public IP # This will be assigned to Virtual Network Gateway
      - Create New
        - IP Name :
        - SKU : Standard (Only)
      - Existing
    - Availability Zone : Zone Redundant
    - Enable active-active mode : Enabled (Default)
  - Disabled

**Tags**

- Name/Value

## How to setup Point-To-Site VPN Connection

**Steps**

- In Azure Virtual Network Gateway > Point-To-Site Configuration

  ![alt text](images/{4F6CC628-31C4-4CF5-8573-A50FD53CBBBD}.png)

  ![alt text](images/{8EB33963-4725-4FFC-B111-3953349053E3}.png)

  ![alt text](images/{CAFD6B13-62B1-40A3-9758-9533D20DBEC1}.png)

- Geneate the Certificate and then use it to connect to the Azure Virtual Network Gateway.

## How to setup Site-To-Site VPN connection
