## Overview

It is a data integration and orchestration platform.

- You can connect to variety of data sources and destinations.

- Here you have Pipelines, consist of activities
  - Clean Data
  - Transform data

For modern Azure data platforms, that is one of the most common and recommended architectures.

```
Source Systems
(SQL, SAP, APIs, SFTP)
        |
        v
Azure Data Factory
        |
        v
Azure Data Lake Storage
        |
        v
Azure Databricks
        |
        v
Power BI / Synapse / Other Systems
```

1. Data Lake Becomes the Central Storage

   **Benefits:**

- Cheap storage
- Scalable
- Keeps raw data for auditing and reprocessing
- Supports structured, semi-structured, and unstructured data

2. Databricks Handles All Processing

   **Benefits:**

- Cleans data
- Validates data
- Applies business rules
- Joins datasets
- Creates curated datasets

A common Lakehouse structure is:

```
Raw Data (Bronze)
      |
      v
Clean Data (Silver)
      |
      v
Business Data (Gold)
```

This pattern is extremely common in Databricks environments.

3. Consumption Layer

   After transformation, data is consumed by:

- Microsoft Power BI dashboards
- Azure Synapse Analytics SQL analytics
- APIs
- Machine learning models
- Operational applications

## Linked Services

Used to connect to differnt data sources and destination

## How to create Azure Data Factory

**Project Details**

- Subscription
  - Resource Group

**Instance Details**

- Name
- Region

**Git Configuration**

- Repository Type
  - Azure Repo (Default)
    - Azure DevOps Account :
    - Project Name :
    - Repo Name :
    - Branch Name :
    - Root Folder :
  - Github

**Networking**

- Public Endpoint (Default)
- Private Endpoint

**Encryption**

- Use encryption with Customer Manager Key : Disabled

**Tags**

- Name/Value

## How to copy data from Azure SQL Database to Azure Synapse using Azure Data Factory.

**Steps**

1. Go to Azure Data Factory resouorce
2. Go to Azure Data Factory Studio
   ![alt text](images/azuredatafactorystudio.png)
3. Go to Ingest > Build-in Copy Task
   ![alt text](images/{9B566D20-1E5A-423B-B5D5-0391FB532714}.png)
4. Specify the sources, as SQL Database
   ![alt text](images/{CEB8C1F5-DDF7-49E6-B4D5-CCCA53AF8A6E}.png)
   ![alt text](images/{EC8A825D-A1A9-4836-BA76-7B49C96CB039}.png)
5. Once connection is established, choose tables as dataset
   ![alt text](images/{9DD76D40-BACD-4FC8-8470-9B0A04DF267E}.png)
6. Choose Destination as Azure Synapse, SQL Pool
   ![alt text](images/{B1F64DE9-4558-4C3C-A3D9-CB6D1936CE65}.png)
   ![alt text](images/{B62B09B1-62B0-4797-B341-723397520D7D}.png)
7. As destination dataset, It will map the tables in the destination datawarehouseDB to create them automatically
   ![alt text](images/{70116D09-65C4-46CA-AD54-3A20A90C3231}.png)
8. Under settings, choose "bulk insert"
   ![alt text](images/{C609DCF6-A9DA-442A-8038-F2AF3983BC38}.png)
9. Skip staging link
10. Done, This will create and run the pipeline to ingest data from SQL Database to Azure Synapse.
    ![alt text](images/{5ECF044E-58D1-4E0B-8C51-AAC7939034B2}.png)

## Azure Data Factory - Mapping Data Flow

Useful when you want to create few additional columns in the dataset while migrating the data from Azure SQL Database to Azure Synapse SQL Pool.

- It helps you visualize the entire transformation process

## Polybase

Under settings, instead of "bulk insert", we can choose "Polybase". More afficient approach for large data sets.

- Useful in transfering data from Azure data lake Gen2 storage account
  ![alt text](images/{C609DCF6-A9DA-442A-8038-F2AF3983BC38}.png)

## Azure Data Factory - Self Hosted Runtime

- Useful while loading data from Azure VM like log data or data files onto the VM
  **Steps**

1. Integrate the VM to the Azure Data Factory usign Self Hosted Runtime

- Go to Azure Data Factory Studio > Integration Runtime
  ![alt text](images/{42663D25-56EA-4E6F-B3E1-80BA94353E07}.png)
- Create new runtime with self-hosted type
  ![alt text](images/{1A3BEBE4-256E-4B4D-877C-D2AE38691EF0}.png)
  ![alt text](images/{20CCE865-D1ED-4878-BF5E-2E8834FF9CAC}.png)
- On the VM, use this link and install with the key
- It will show then as integrated to the Azure Data Factory
  ![alt text](images/{7CA2798E-B314-4569-B6FA-A662EB125D1B}.png)

2. Create a Pipeline in Azure DataFactory, to ingest the data from the VM to destination (Azure Synapses Dedicated SQL Pool)

- Go to Azure Data Factory > Author
  ![alt text](images/{10F77E95-DD54-408E-96A7-E0325A0FBE5E}.png)
- New Pipeline
  ![alt text](images/{45199B34-9725-4865-B30C-7222DB008DDF}.png)
  - "Copy Data" Activity
    ![alt text](images/{6E656015-0770-422C-8FF2-C82C5E07AE94}.png)
    - Source : FileSystem  
      ![alt text](images/{B4DD418B-587A-4E9F-87D1-2E89924C7255}.png)
      ![alt text](images/{A23A945E-BAA9-450B-95AD-87DB85601337}.png)
    - Disable localfolderpathvalidation on the server, incase you face connection issue
      ![alt text](images/ConnectionIssue.png)
