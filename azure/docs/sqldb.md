Full Managed MS SQL Database without operation overhead of server patching. 

With additional Features
- Backup
- High Availability
- Scaling
- Monitoring

# Type

PaaS > DBaaS

[Go Back Home](../README.md)

# Configuration
- Subscription: subscriptionId
- Resource Group : resource group name
- Database Name : < Database name >
- Database Server:
    - Server Name: < unique-name >.database.windows.net
    - Region: Sweden Central
    - Authentication Method:
        - SQL Authentication
            - Admin User Name : myadmin (can't use admin, administrator,root)
            - Admin Password: *****
        - MS Entra Authentication
            - Select Admin user from Entra ID
        - MS Entra and SQL Authentication
- Want to use SQL elastic pool: Elastic pools provide a simple and cost effective solution for managing the performance of multiple databases within a fixed budget. An elastic pool provides compute (eDTUs) and storage resources that are shared between all the databases it contains. Databases within a pool only use the resources they need, when they need them, within configurable limits. The price of a pool is based only on the amount of resources configured and is independent of the number of databases it contains.
    - Enabled
        - Select / Create Elastic Pool
            - Name : Elastic Pool Name 
    - Disabled
- Compute & Storage
        - Service Tier
            - DTU Based Purchasing Model
                - Basic (For less demanding workload)
                    - Max Data (5 GB) - Cost $5 / Month
                - Standard (Budget Friendly)
                    - DTUs -> Max Data (1 TB)
                    - ( 10,20,50 DTU - Max Data (250 GB))
                    - ( 100,200,400,.., 3000 - Max Data (1024 GB))
                    - Eg : 50 DTU + 250 GB =  $73 / month
                    - Eg : 100 DTU + 1024 GB =  $300 / month
                - Premium (Highest Availability and Performance)
                    - DTUs -> Max Data (4 TB)
                    - ( 125,250,500,1000 DTU - Max Data (1024 GB))
                    - ( 1750 - 4000 - Max Data (4096 GB))        
            - vCore Based Purchasing Model
                - General Purpose (Most Budget Friendly)
                - Hyperscale (Higly Scalable Compute and storage)
                - Business Critical (Highly Available and Peformance)
                    - Compute Tier : 
                        - Provisioned : Compute resources are pre-allocated, Billed per hours based on vCore allocated
                        - Serverless : Compute resources are auto-scaled, Billed per second based on vCore Used
                    - Storage Tier : 
                        - Max 4 TB Storage
- Backup Storage Redundancy:
    - Locally-Redundant Backup Storage
    - Zone-Redundant Backup Storage
    - Geo-Redundant Backup Storage
    - Geo-Zone-Redundant Backup Storage
- Connectivity Method
    - No Access
    - Public Endpoint :
        - Firewall rules
            - Allow azure services and resources to access this server (Enabled/Disabled)
            - Add current client IP (Enabled/Disabled)
    - Private Endpoint : Private endpoint connections are associated with a private IP address within a Virtual Network. Note that private endpoint connections are defined at the server level and they provide access to all databases in the server.
        - Name: private endpoint name
        - Virtual Network: Select/Create virutal network
        - Subnet: This subnet will be home to the private endpoint
        - Integrate with private DNS zone : To connect privately with your private endpoint, you need a DNS record. We recommend that you integrate your private endpoint with a private DNS zone
            - Enabled : 
            - Disabled
- Server Identity: Use system or user assigned managed identity to enable central access management between this database and other resources
- Enable MS Defender For Cloud (Yes/No)
- Transparent Data Encryption Key Management ; Transparent data encryption encrypts your databases, backups, and logs at rest without any changes to your application. To enable encryption, go to each database. Database level settings if enabled, will override the server level setting.
    - Server Level Key : Default - Service Managed Key
    - Database Level Key : Default - Not Configured
- Data Source
    - None
    - Backup
    - Sample
- Maintenance window (Default : 5 PM - 8 AM )




### Backup and Retension

In Azure SQL Database, backups are automatic and managed by Microsoft — that’s why you don’t see a backup schedule during creation.

| Backup Type            | Frequency          |
| ---------------------- | ------------------ |
| Full backup            | Weekly             |
| Differential backup    | Every 12–24 hours  |
| Transaction log backup | Every 5–10 minutes |


| Tier     | Default PITR Retention |
| -------- | ---------------------- |
| Basic    | 7 days                 |
| Standard | 7–35 days              |
| Premium  | 7–35 days              |



| Feature          | Solves                                  |
| ---------------- | --------------------------------------- |
| Managed Identity | Authentication / authorization          |
| Private Endpoint | Network security / private connectivity |
