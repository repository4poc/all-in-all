## SQL Database

- RPO (Recovery Point Objective)
  - Maximum allowed data loss

- RTO (Recovery Time Objective)
  - Maximum allowed downtime

Not in case an application has web app and sql database

**Example**

- Web App RTO = 10 Min
- DataBase RTP = 30 Min
- Overall System RTO = 30 min.

## Change Pricing Tier of Database

- You can change pricing tier of your Azure SQL database on created, as required
- So you can start with the DTU model and if required can upgrade to vCore.
- There will be a very short downtime.

## Handling Transient Errors

Each application should have the capability to handle transient errors like connection timeout during DB upgrade, so you application should be connection retry logic, or use Queues.

- It is always avoidided to directly all database from frontend, better use Queue in between to avoid any sort of transient error scenarios.

- For High Availability, decouple your application components
  - Frontend - Message Service - Function - Database
  - Each component should be HA independently

## SQL Database HA using Transaction Logs

With the use of Transaction logs, we can bring back the database into the consistent state in case of failure.

## Backup Strategy

- **Full backup**
  - once a week
- **Differentical Backup**: Contains data that has changed since the last full backup.
  - Every 12/24 hrs.
- **Transaction Logs** : Contains logs of transactions
  - Every 10 minutes (So RPO = 10 min.)

Everything will be done automatically by the Azure SQL Database service, no need of creating scripts.

With the automated backup, you can perform **Point in Time restore** of the database in case of failure, so by using the restore point, you can create a new database within the same Azure SQL database server.

Database Backup has

- data.mdf File : Database objects with data
- log.ldf File : Transaction logs

So Here:

- RPO = 10 minutes
- RTO = Depends on the database size.

Backup Retension Period

- Basic Tier = 7 days only
- Other Tiers = Upto 35 days

## How to access Azure SQL Database backups and restore to a Point In Time backup

- Go to Azure SQL Server > Backups
  ![alt text](images/{CD809E57-3D40-4416-86C0-DC7221171D8E}.png)
- To see the retention policy > Click Retention policy Tab
  ![alt text](images/{684D4ACF-D949-4DB7-941E-8205C00C5201}.png)
- You can update the retention policy configuration
  ![alt text](images/{50135C5C-8339-49C7-B537-912F18B2EFE6}.png)
- To PITR Restore > Click "Restore"
  ![alt text](images/{56A5F5CF-4F96-4155-90B9-7D888451E612}.png)
  ![alt text](images/{10430D24-8A06-4E53-9928-0DC438B27967}.png)

## Long Term Retension

Used in case you want to retain your backup to more than 35 days.

- The backups are stored in the Azure Storage Account, where you can store upto 10 years.
- Update the Rention policy and configure the Long Term Retention.
  ![alt text](images/{CE90BE4A-8A7F-4627-A3AA-92C528652B37}.png)

## Database Redundancy vs ## Database Backup Redundancy

While you create an Azure SQL Database, there are different backup redundancy options
![alt text](images/{0513B676-CD1E-4ABC-8055-1D4289044733}.png)

We can update this backup redundancy value, after creating the database as
![alt text](images/{A829D548-487D-48CE-B763-A87960D1349F}.png)

Azure SQL Database has 2 layers

- Compute Layer : Where database engine is running
- Data Layer : where data and logs are residing

For a user, it comes as a package, but under the cover, the database engine is hosted on a Azure VM and database data and logs stored in an Azure Storage Account. The transient and cache data is stored on local SSD

- If anything happens to the compute layer, another VM will spinup and connect to the data layer.

So if you choose DTU Based Pricing Model

- Basic
- Standard

It will use VMSS internally on a single datacenter, so not zone redundancy.

![alt text](images/{AD8980D6-EFDE-4CAB-871D-6379A7ACB40A}.png)

In case of Premium Tier, you have the option to make it zone redundant
![alt text](images/{67B9FE50-3355-4F6C-9905-F079733D5E87}.png)

**Backup Redundancy**
![alt text](images/{028F6EC6-67E6-4803-B6C9-196422748E87}.png)
![alt text](images/{2048057F-84AB-46FE-A96F-69A69AE6B266}.png)
![alt text](images/{2710F804-0B87-4B3D-B567-1FB24B36E660}.png)

## How to perform Geo-Restore From Geo redundant backup

**Steps**

- In Primary Azure SQL Database, set
  - Backup Storage Redundancy : Geo redundancy backup storage
    ![alt text](images/{1D670C0A-6F10-4389-BA78-08402D108963}.png)
- Create a new Azure Database Serve in paired region (Check Azure documentation)
  - Sweden Central ↔️ Sweden South
  - North Europe ↔️ South Europe
  - Choose "Backup" for existing data
    ![alt text](images/{C7CF6E20-BDAD-46CB-8D0F-4A2689C8D8C7}.png)
    ![alt text](images/{C77AF2EF-FBDB-407B-8EC2-072976536671}.png)

    ## How to Recover a deleted database

    Under Azure SQL Server > Delete Databases > Restore

    ![alt text](images/{24354142-F145-4E81-80B5-F16FC70E8231}.png)

## Backup Cost

![alt text](images/{737A54EB-9816-4ABF-89A3-E1E3E2CEC5F1}.png)

## MS SQL Server vs Azure SQL Database Backup

- Use MS SQL Server Management Studio

  **MS SQL Server** : See backup option
  ![alt text](images/{1561BDB4-80E5-4B56-9B71-0A36D9EC6173}.png)

  **Azure SQL Server** : No backup option, as it is manged by Azure, You dont have access to underlying machine hosting the database Engine
  ![alt text](images/{AA089327-AA98-4273-B98F-F76282A2D6FC}.png)

  \*In case of Azure SQL Managed Instance, you will have the option of "Backup" from MSMS as it is fully MS SQL Server implementation, Yet canot access the underlying machine.

## Active Geo Replication - Azure SQL Database

- Azure SQL Database Sevice feature to minimise RTO and RPO, by having a replica of your database into another region, with data synchronized between two
- So incase there is a issue in the primary location, you can recover to the secondary location
- The transaction logs are streamed from primary to replica.
- In replica, you can only make read operations, All writes happen in the primary database
- UseCases :
  - Replica can be used for report generation
  - Replica can be used for failover, in case of primary

## How to enable Active Geo Replication

**Steps**

- Choose Azure SQL Database > Replics > Create Replica in paired region
  ![alt text](images/{B6C7E470-072B-45A7-97AA-D4A561F5E2E6}.png)
  ![alt text](images/{7A266B06-2000-4D12-9437-2247C8509720}.png)
- This will create a replicate database in another Azure Server in paired region
  ![alt text](images/{19146A02-144C-4D8D-924D-AA3D28D92C36}.png)
- Ensure to configure Firewall rule on the replica database
  ![alt text](images/{AE6EC077-3B26-4418-8CAB-A7708BD41412}.png)

So weekly or monthly, you can have a validation test, to check the sync is working properly as SRE

## How to failover to Active Geo Replication

- Geo Replica is a Read replica, you cant not write data onto that database
  ![alt text](images/{3121A662-C211-4DB5-9739-8E6DC7D2EE63}.png)

- When you failover, this will switch the replica database to primary role
  ![alt text](images/{CE84735D-81A4-4904-A543-DA9498A27C3F}.png)
  So Primary becomes read replica and replica database become primary, so now can read write to it.
  ![alt text]({90F78F6D-FF51-48FF-B4BA-397D276518A7}.png)

So you can calculate the time duration of this Failover activity and mention it as RTO
But the application need to refer to the replica database URL

## How do we handle,the URL change in case of failover

Use Failover group

## Auto-Failover Group - Azure SQL Database

- An Azure SQL Database feature
- Purpose is to failover from one location to another
- This feature is build on top of geo-relication
- Here the Primary Azure SQL Server has multiple database, so when the failover happend, it happens as a group. All database in primary is failed over to replicas
  ![alt text](images/{DB320779-EE28-4DF3-B5E7-BD03791A94C9}.png)
- In Failover group, when the application need not to change the database url in application code, because the application connects to the **listener endpoint** created for failover group instead of individual database endpoint.
- When a failover occur, the listerner endpoint,automatically sends traffic to the replica.

## How to implement Auto-Failover Group

- Create "SQL Logical Server"
  ![alt text](images/{45FB9E71-0939-4471-99DB-7F615A93580C}.png)
- Choose Primary Azure SQL Server > Failover Group
  ![alt text](images/{27B605E5-0275-45E0-89CE-3416F0EC4199}.png)
- Add Group
  ![alt text](images/{DD4D6808-AB44-429D-9DC6-EAB3A1059892}.png)
  - Server : Choose Replica Server
  - R/W Failover policy
    - Manual
    - Automatic (Default)
  - R/W Grace Period : \_1\_\_ hours
- Databases : Choose database in Primary server to be replicated to the replica server
  ![alt text](images/{6F1E52E2-DBD5-4E3A-9345-4440E7B6B186}.png)
- So now instead of database specific URL in application code, use Listener endpoint.
- Go to Azure SQL Server > Failover
  - Listerner Endpoints for R/W and Read only
    ![alt text](images/{A12D14CD-CF65-48FE-A866-E5759496BC26}.png)

As SRE, you need to perform monthly failover validation.

- You can do so by performaing forced failover via
  ![alt text](images/{B6730ADB-F28A-4F2F-86D3-2459C7AC9E15}.png)

There is small downtime during the failover, count the duration and that will be your RTO:

| Feature                             | Active Geo-Replication | Failover Group |
| ----------------------------------- | ---------------------- | -------------- |
| Automatic failover                  | No                     | Yes            |
| Multiple databases managed together | No                     | Yes            |
| Read-only replicas                  | Yes                    | Yes            |
| Read/write listener endpoint        | No                     | Yes            |
| Simplified application connectivity | No                     | Yes            |
| Recommended for DR                  | Good                   | Best           |

**Benefits of using DTU based - Premium Tier**

The main benefit of Premium Tier is high performance due to (local SSD, higher IOPS, lower latency)

![alt text](images/{CE46A0F3-F20E-4331-9178-B0642FDA9D54}.png)

![alt text](images/{252AE36F-5F0B-4983-ACCA-90BCC3006614}.png)

Today, Microsoft generally recommends the vCore model over DTU. In the vCore world:

| Feature                            | General Purpose | Business Critical |
| ---------------------------------- | --------------- | ----------------- |
| Remote storage                     | ✅ Yes          | ❌ No             |
| Local SSD storage                  | ❌ No           | ✅ Yes            |
| Zone redundancy                    | ✅ Supported    | ✅ Supported      |
| Read scale-out replicas            | ❌ No           | ✅ Yes            |
| Lowest latency                     | ❌ No           | ✅ Yes            |
| Multiple synchronous replicas      | ❌ No           | ✅ Yes            |
| Suitable for mission-critical OLTP | Limited         | ✅ Yes            |

```
Business Critical Primary
    ├── Local HA Replica 1 (same region)
    ├── Local HA Replica 2 (same region)
    └── Local HA Replica 3 (same region)

            +

Geo-Replication / Failover Group

            ↓

Secondary Business Critical Database
    ├── Local HA Replica 1
    ├── Local HA Replica 2
    └── Local HA Replica 3
```

## Availability Set

- For HA within the same Availability Zone
- Consists of
  - Update Domaon (20)
  - Fault Domain (3)

## Availability Zone

- For HA within a Region, by distributing the VMs amoung zones within a Region
- Each zone has independent power, cooling and networking
- Region A
  - Zone 1
  - Zone 2
  - Zone 3
- There is data transfer charge if VMs interact with eachother, across zones.

## Azure Backup

- Azure Backup for Virtual Machines takes a snapshot of all managed disks attached to the VM, including:
  - ✅ OS disk
  - ✅ Data disks

- The backup data is stored in "Azure Recovery Service Vault" Service, in the same region

**Steps**

1. Install extension on VM (Windows/Linux)
2. The backup tool, first take snapshot of data of the VM locally
3. Copy the snapshot of data onto Azure Recovery Service Vault

## How to enable backup on VM

![alt text](images/{242A0C52-3326-4030-B079-928E6670787E}.png)

![alt text](images/{CE0E9DA8-A0CC-4784-BAE2-13F0CB8808B1}.png)

For EnhancePolcy, the backup Frequency is 4 hours, there is RPO : 4 hrs , RTO : Depends on VM size

## How to do VM restore using Azure backup

**Steps**

1. Go the the VM > Backup
2. There are 3 option
   - Restore VM
     - You need Azure Storage Account as a staging location
     - Create a new VM from the restore point
   - File Recovery

   ![alt text](images/{25D5122D-9806-4D7E-A356-D6BC5B0303E0}.png)

## Azure Key Vault HA

- High Availability (GRS) is built-in for Azure Key Vault
- So in case of primary region issue, a secondary region copy is used for restoration
- You can manually take backup of the key vault locally and them restore the backup it into another key vault in another region, but in same geography (europe)
  ![alt text](images/{22E88B3F-2EBC-49AE-B2D6-176DF085E19F}.png)
  ![alt text](images/{9A01ED6D-351F-45A9-B6BC-ECF5CDBAAE4F}.png)

## Azure Blob - Data Protection

- Blob Soft Delete
  - Retention : 1-365 days
- Versioning
- Snapshot
  ![alt text](images/{44953B76-CA79-49F6-B80C-31C72AD8F51B}.png)
  ![alt text](images/{2E6ECEB4-7BAF-44EB-B4C9-8DBF379F7AAC}.png)
- How to see deleted blobs
  ![alt text](images/{28314E4E-EA55-4124-8649-BC0483B4488E}.png)

## Azure File Share backup

Azure File share also uses Azure Recovery Vault for the backup

- Go go Storage Account > File Share
- Create a File Share
  ![alt text](images/{4F2BA933-94AD-46D6-875B-D72D56B9D7BB}.png)
  ![alt text](images/{2C754C8F-1F54-48ED-AD64-8756349F2F1E}.png)

## Azure Site Recovery

- Used for Business Continuity and Desaster Recovery for on-premise VMs
- Ensure you workload keep running during an unplanned maintenance on on-premise vm in one of a datacenter.
- With it you can have continuous replication of data from one datacenter to another datacenter
