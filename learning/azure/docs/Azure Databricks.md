## Overview

- Apache Spark is an Open Source Distributed Compute Engine used for Big Data and ML Projects
- Databricks is based on Apache Spark, and developed by Apache Spark Developers, to make working with Apache Spark Easier by providing the esential management layers.
- Databricks is available on all major cloud providers, Azure Databricks is managed Databricks Service on Azure Cloud.

![alt text](images/{AD0CE7B4-3B7C-4744-AA45-032E50B5C980}.png)

- Azure hosts Databricks as a First party service, that mean you get unified billing and direct support from MicroSoft for all your service including databricks.

## Create a databricks Service

**Project Details**

- subscription - resource group

**Instance Details**

- Workspace Name:
- Region: < Choose Closest Region>
- Tier : Trial
  - Trial (Premium - 14 days)
  - Premium
    - Serverless
    - Hybrid (Custom Compute)
- Managed Resource Group name:

**Networking**

- Deploy Azure Databricks workspace with Secure Cluster Connectivity (No Public IP) : Enabled (Default) - Disabled it to access
  - With secure cluster connectivity enabled, customer virtual networks have no open ports and Databricks Runtime cluster nodes have no public IP addresses.

- Deploy Azure Databricks workspace in your own Virtual Network (VNet): Disabled (Default)

**Encryption**

- Azure Databricks encrypt data by default

**Tags**

- Key/Value

## Databricks UI

![alt text](images/{887AA18C-2B2D-4FA7-9657-FBA2E38D4074}.png)
![alt text](images/{52BA6125-4A86-4386-947C-920AA22EDEA5}.png)

Each user has its own workspace folder

![alt text](images/{7D8A50BA-00A3-4492-AC7F-7F7E280EED2B}.png)

## Databricks Architecture

![alt text](images/{D5AF2924-1005-4700-A4D4-1341482F66D7}.png)
![alt text](images/{0752843F-57AF-495B-9112-EF92180E5C63}.png)
![alt text](images/{354CA403-BAA1-4E2C-B8A3-710941A57E8F}.png)

## Databricks Compute

Two type of databricks compute

    - Serverless Compute
        - available on-demand and managed by databricks
    -  Classic Compute
        - configured and provisioned by the user

![alt text](images/{8B3BDAB0-E84A-48E3-829A-44F87D97C242}.png)
![alt text](images/{6F0805A8-BEC5-46B2-9928-6E0CF88102A7}.png)

**Benefits of Serverless Compute**

- Better startup time
- Better productivity
- Auto Scale
- Billed based on execution time.
- Reduce Operation Overhead.

**Drawback**

- Less control over the underlying compute resources

**Benefits of Classic Compute**

- You can choose databricks runtime version
- Can choose the machine types
- Configure the autoscaling behaviour
- Provide better control over configuration and advance customization

**Drawback**

- Operation Overhead

![alt text](images/{A073D3E6-FB05-4D26-9B99-4405558CB7D3}.png)

## Compute Configuration

![alt text](images/{115E6170-D702-4991-9B7B-26EFE8E6FE0C}.png)
![alt text](images/{B92993BE-F9D1-4D2A-8082-1A8648602143}.png)
![alt text](images/{3228EB34-E32A-4EFA-A4EE-0F2D2A30A482}.png)
![alt text](images/{35B084A8-5FF5-47ED-8B3F-D9D9475C62EC}.png)
![alt text](images/{586C5F44-B4C3-4149-B003-00C5871BE696}.png)
![alt text](images/{2BC47E28-CCBF-452A-9520-6ED63A38C727}.png)
![alt text](images/{5023BA1C-415E-4C8B-9743-6060D08B6830}.png)

## Create Databricks Cluster

![alt text](images/{BAEA303F-73A7-4DE9-B1FD-348E0507DAF0}.png)

- Turn off the simple form option, to see all the options
  ![alt text](images/{11EA919B-7A07-42D3-B2A9-C7D84671A686}.png)

- Cluster Name : Your Name Cluster (Default)
- Policy : Unrestricted (Default)
  ![alt text](images/{10478340-A1F5-4B11-AA96-5B34A9EEB0D2}.png)
  ![alt text](images/{B0A437CB-21E5-4D25-8D33-3985E35393EE}.png)
  ![alt text](images/{4B402DEC-376B-4ED3-A8A5-1F6183F3BC4D}.png)

**Access Mode**

![alt text](images/{981D2773-140E-440E-8CD0-E4AF72682D1B}.png)

**Performance**

![alt text](images/{8FC323EA-9230-46D5-AE70-E430D17A79D6}.png)

**Node Types**

![alt text](images/{6544C4CB-B315-47EC-8CC8-0F0CBDBB8BF7}.png)

![alt text](images/{CAF4B78C-0108-4D10-BEBE-4472AC5F25A6}.png)

You can update the

- Performance
- Runtime
- Cluster Type - single/Multi Node (Not Possible)
- Node Type

  Then "Restart and Update"

**Pricing**

- Pricing based on DBU(DataBricks Units)

## Troubleshoot - Databricks cluster creation issue

![alt text](images/{DC673C57-0167-4094-A0E1-2A95A7405180}.png)
![alt text](images/{5728D3E1-1E3D-4EDE-B166-31DE9A7B911F}.png)

## Databricks Notebooks

Databricks offer a jupiter style notebook environment with additional feature to streamline your development work.

A notebook is a collection of cells, that execute commands on the databricks cluster

In notebook, we have

- code cell
- text cell
  - %md : Markdown

![alt text](images/{F664F8B5-E2C0-4C0A-9F32-064B30A54FF4}.png)

## Databricks Magic Commands

![alt text](images/{D3F5D230-C6CC-4041-B894-83D7E1E698D9}.png)

![alt text](images/{EA550C17-2FBF-45E0-9F93-CD1A72644436}.png)

![alt text](images/{C4BE79CF-1AEA-4100-B807-B74BDAF0540C}.png)

![alt text](images/{14EAE017-D4B6-4D49-A32D-1220D2E3ABA5}.png)

![alt text](images/{1F7FFE3F-74BF-44C9-85EA-C11CF16DDED7}.png)

![alt text](images/{1780B317-5630-4448-A0DD-6E7B92E1A66B}.png)

![alt text](images/{462B45EB-B0AC-486A-BDA0-95BA4C71A5A2}.png)

## Databricks Utilities

- File System Utility (dbutils.fs)
  - More than magic commands
- Secret Utility
- Widget Utility
- Notebook Workflow Utility

![alt text](images/{F912D525-E657-498B-812D-389668B863AB}.png)

![alt text](images/{DFB3042A-10C7-4343-8434-CA987FB532DE}.png)

![alt text](images/{1F334E2A-4045-48EF-9680-E49A22FB7995}.png)

![alt text](images/{C060E745-4531-4D50-861E-1B80DFCA6522}.png)

![alt text](images/{6CE950F9-8300-4135-89FE-A6AE41BA6FCD}.png)

## Debugging Databricks Notebooks

![alt text](images/{102342AF-4372-4FD8-B7D8-01046FB59203}.png)

![alt text](images/{4151B8CC-F20A-4618-B1E3-6FFEC08730AA}.png)

![alt text](images/{4C7F190B-EAD5-4329-952A-F115E9E28520}.png)

![alt text](images/{BB5FB5DC-6B07-4E39-AFC2-109EF18AA9D3}.png)

![alt text](images/{2DECCEB0-3D25-4936-87E6-769D258A6224}.png)

## Utility Catelog

A central governance solution for all your data in databricks

![alt text](images/{733AAE3D-642A-4DA6-B5FC-EBBB79699ABD}.png)

## Unity Catelog Object Model

![alt text](images/{9E1EA4BF-A688-453A-BA53-304353E65DD5}.png)
![alt text](images/{E56451B6-AFAC-431B-A475-4739A6AD4FF9}.png)
Metastore can have one or more catalog, you can have one catalog per business domain or per environment
![alt text](images/{087689A4-50F7-456C-8B83-33A995D13F32}.png)

Each Catalog can have one or more schema.
![alt text](images/{FD3FFC3E-FFBD-4432-9B44-352D381A58F1}.png)

- When you delete Managed table both metadata along with underlying data is deleted, But when External table is deleted, only meta data is deleted, but underlying data stored in ADLS (Azure Data lake Storage) will remain intact. This is quite useful when data is produced by datafactory or Firtron, and databricks is only consuming the data.
- All managed tables use data lake format, you cant create managed table using csv, JSON etc format. But these formats can be used in External tables.
- Views allow us to save query logic and provide simplified and restricted version of the data.
- Functions are used for reusable transformation logic.
- Volumne : provide Governed way to work with files stored in cloud object storage. commonly used for unstructured and semi-structured data.Used to access files in modern databricks projects.

## Access databricks Account Console

![alt text](images/{2FA6C5B9-2E0A-4774-930A-18828505007D}.png)

**Databricks workspace**
![alt text](images/{3108E326-EBC6-442D-861E-0E7B6A6885AA}.png)

![alt text](images/{9BE29F2F-21B2-447C-ABE6-654DFB661C95}.png)
![alt text](images/{09A6AC20-7B64-4272-9F91-2F79053E433A}.png)

## Configure Access to Cloud Storage

![alt text](images/{62AE3F4E-8726-4BC8-94C9-5E7476E6D36E}.png)
![alt text](images/{FB7AB414-B242-465F-A5CB-49D2E0726329}.png)
![alt text](images/{781C630F-36DA-4241-9006-D4AE6EFAB127}.png)
![alt text](images/{3754ADE6-A7BC-4DED-A0DB-703FCAF11A73}.png)

## Configure Access to Azure Cloud Storage

**Create Access Connnector for Data Bricks**
![alt text](images/{F15BE262-6916-4E55-8B2D-1F6D6A1BB6C8}.png)

**Create Azure Data Lake Storage Account**
![alt text](images/{789F9487-372D-4F00-93A7-62730DC939CE}.png)
![alt text](images/{D5A0B590-237D-4A0F-AC73-586077E0D2DC}.png)

**Provide access of ADLS to the Storage Connector**

- RBAC Role : Storage Blob Data Contributor
  ![alt text](images/{539ECFF1-6291-4F55-9B92-5EC32387748C}.png)
  ![alt text](images/{6D53BC76-17C6-4543-ADCD-45D643F6D3CF}.png)

**Create Catelog > Credential**

- `Storage Credential` wrap the `Access Connnector for Data Bricks`, so `Storage Credential` has access to the `Azure Data Lake Storage`
  ![alt text](images/{991F059A-F6DA-4237-9622-3D9E6F232FB3}.png)
  ![alt text](images/{6145A15F-69A0-4774-9DF9-08D120664FD5}.png)
  ![alt text](images/{C1ED4880-0BA0-4E31-8CBA-C6749325A710}.png)

**Create Catelog > External Location**

- Create a container in the Azure Data Lake Storage Account
  ![alt text](images/{7920C375-E933-4B66-9EB4-F39476CD7281}.png)

- So the `External Location` will give access to this `container` via `Storage Credential`
  ![alt text](images/{814A61D4-367D-47C3-B88F-B6878D786287}.png)
  ![alt text](images/{33436014-256D-4AF3-B3C2-13ADBE60F903}.png)

  **OR**

  ![alt text](images/{E07F3E2A-243C-469B-80F9-77B5B613F222}.png)

**Test Access**

- Test access to `Storage Account Container` using `External Location`
  ![alt text](images/{0CCD894C-B732-4AA9-8B02-61A7B0ACC6C7}.png)
  ![alt text](images/{6EFC3F2F-1C9B-436F-82D0-741699D7F247}.png)

## Project - Data Source

![alt text](images/{21EE9A69-35C0-45E4-96DF-31548B1D456B}.png)
![alt text](images/{9C1DB63E-C8EB-4EB9-81B3-126A2071834E}.png)
![alt text](images/{AC19E4CF-1295-4235-ADDA-C6AC05EDFD7E}.png)
![alt text](images/{4D451CB6-0C88-4F72-B776-A7AC00B509EB}.png)
![alt text](images/{2DAF9A23-5160-4109-B9D7-47EE1CC707E9}.png)

## Project - Requirements

![alt text](images/{365CD872-E040-4333-B620-AC0E87C8170B}.png)
![alt text](images/{D104C9B0-BCE8-4DA1-A1D3-737258CAF698}.png)
![alt text](images/{968FA1F9-FFDC-4906-BEEA-6FFF764C03FA}.png)
![alt text](images/{D1CE7796-253F-45C7-B94D-C425077E1B28}.png)
![alt text](images/{9E225590-18C7-4E27-A488-0B3847E1988D}.png)
**Steps**
![alt text](images/{2192025C-F3BB-40E6-82A8-46092CAFA2E7}.png)

## Data Lakehouse

![alt text](images/{31F7FE48-CD4B-40D9-BC71-634BB47833CF}.png)
![alt text](images/{B3B15504-A69C-4E82-B6FA-E121D84CCE09}.png)
![alt text](images/{04E88114-617A-4C15-B927-ABC8FDF2E9D6}.png)
Dataware house does not support unstructured data, only strcutred and semi-structured data
Datalake came into picture to resolved this drawback, support all 3 data types
![alt text](images/{3DEFC06E-D267-4542-B369-42F88419800B}.png)
Datalake lack in supporting streaming, data science and ML/AI workloads
![alt text](images/{E9A2A12D-872C-4F15-8587-D9FA8607DE88}.png)
![alt text](images/{8655F633-4BDF-45A8-9DE4-DD95858AF65A}.png)

## Medallion Architecture

- It is a data design pattern
- 3 layer structure, with each layer, the data quality improves
- Some use 4 layers like platinum and simple projects use 2 layers.
- Each layer has its own characteristics
  ![alt text](images/{22ED048D-DC57-4046-839F-3AFF94F921A8}.png)

## Solution Architecture Overview

![alt text](images/{CBD624F0-ABD6-4154-9570-60AD7CD2A771}.png)

## Setting up Data lake project environment

![alt text](images/{AB6A45E6-24A4-449F-809C-B67B5DD554FF}.png)

![alt text](images/{05C85A96-D395-4ACF-9876-A078DB121E76}.png)

```
%sql
CREATE EXTERNAL LOCATION IF NOT EXISTS databricks_external_location_2284
URL 'abfss://racingdata@racingstroageaccount2284.dfs.core.windows.net'
WITH (STORAGE CREDENTIAL `storage_credentials`)
COMMENT 'External location for the racing data container in the storage account'
```

```
%fs
ls 'abfss://racingdata@racingstroageaccount2284.dfs.core.windows.net'
```

![alt text](images/{485DFDC5-E544-4D05-B36A-0F2C460FE6F9}.png)

## Setting up Unity Catelog Project Environment

![alt text](images/{0C129FD3-DC6D-4624-AFFC-9E4EBEF816FE}.png)

![alt text](images/{9DFE360C-19C9-401B-8672-C72A357BF1BA}.png)

**Catelogs**
![alt text](images/{0AE0BFCF-FCCD-47AC-BFFE-9CF5B3BA7D40}.png)

Databricks create a catelog for every databricks workspace.
![alt text](images/{2FC73E6B-CA77-4408-AFAF-9D1A2E7A02D6}.png)

```
%sql
SHOW CATALOGS
```

![alt text](images/{16F4816F-4C3C-4C6D-9EC4-D9B2F8B4D072}.png)

## Create a new catalog

```
%sql
CREATE CATALOG IF NOT EXISTS formula1
    MANAGED LOCATION 'abfss://racingdata@racingstroageaccount2284.dfs.core.windows.net'
    COMMENT 'Catalog for formula1 dataset'
```

![alt text](images/{B1F11D51-5C79-434B-9DC0-B9C59C83A526}.png)

## Check schema using Command Line

![alt text](images/{3325BA90-9B62-4983-AA03-6F0F44586B5F}.png)

**Documentation Link**

https://learn.microsoft.com/en-us/azure/databricks/schemas/create-schema

![alt text](images/{F348D822-0CAA-4698-8C59-45F65E7D9B0D}.png)

![alt text](images/{FF6F22EE-1BC6-4BB3-B9D5-2998AF753AF0}.png)

- Enternal Location point to the container
- Schema uses the External location (Pointing to container) + Folder inside it (bronze/silver/gold). That we need to create manually before we create schema.

  ![alt text](images/{8906333D-9958-46D9-BD05-8EB849B82733}.png)

## Check schema from UI

![alt text](images/{647DAB08-3D56-4933-8304-CC976024992B}.png)

- default and information_schema are databricks managed schemas.

## Check schema using Command Line

![alt text](images/{3325BA90-9B62-4983-AA03-6F0F44586B5F}.png)

This command is not listing the gold, silver etc schema , because we are not using the formula1 catalog

![alt text](images/{01EF547A-19EC-4622-AE5F-8ED800DA05D5}.png)

## Use different catalog

![alt text](images/{510CB69E-49EB-4D9C-9F7C-A4B0D2BCD840}.png)

![alt text](images/showschema.png)

## Create Volumn

![alt text](images/{E373C019-5533-41F3-B10C-9DA5D4C841AE}.png)

```
%sql
CREATE EXTERNAL VOLUME formula1.landing.files
LOCATION 'abfss://racingdata@racingstroageaccount2284.dfs.core.windows.net/landing/files';
```

![alt text](images/{9C7790AF-B5B6-4FB9-9DE6-08A0405CE435}.png)

![alt text](images/{90EFC5A2-C783-4ADB-A26E-C024BA93E1A6}.png)

![alt text](images/{4071BDC4-2E3F-479E-AE9C-740CE04F28AA}.png)

| External Location                                         | Volume                                                                         |
| --------------------------------------------------------- | ------------------------------------------------------------------------------ |
| A secure pointer to cloud storage (S3, ADLS, GCS).        | A managed storage object within a Unity Catalog schema.                        |
| Used to grant governed access to existing cloud storage.  | Used to store and organize non-tabular files (images, PDFs, models, etc.).     |
| Doesn't itself contain data—it references a storage path. | Stores files under a catalog/schema and is accessed using Unity Catalog paths. |
| Typically created by data/platform admins.                | Used by data engineers, analysts, and ML users for file management.            |

## Data Ingestion - Bronze

![alt text](images/{2AAC0B77-9315-4157-BB7F-B65CEACE06BB}.png)

![alt text](images/{83A969B5-5684-4CF1-8D2B-142EE5F57C9C}.png)

**Delta** : Default format for managing tables in Databricks

- Allows us to interact with data as regular table, along with reliability and transactional guarantee.

## Requirements

![alt text](images/{347692B7-9C93-43A5-94A4-763B42B44480}.png)

![alt text](images/{559807E1-2893-42B5-8DB0-5B74705690EC}.png)

## Dataframe Reader

Sparks Documentation

https://spark.apache.org/docs/latest/api/python/reference/pyspark.sql/io.html

![alt text](images/{9C049B42-8AF0-4556-B6A0-C36980A3161E}.png)
![alt text](images/{81975B99-6846-4E9D-BA8B-7DE533AE4F5E}.png)
![alt text](images/{F9B2345F-9A2C-4651-9067-37C9905D141F}.png)

![alt text](images/{480DC4FF-35E9-4242-A9A4-0BAA6D51D27C}.png)
![alt text](images/{EECD2963-3180-42F9-9972-635D743AD7D6}.png)

- header
  ![alt text](images/{701EEA3C-BF51-4061-A8B5-DD73FF2732D3}.png)

- inferSchema
  - Good for dev, not for production. as datatype changes with data, so unpredictable. Use StructType for prod
    ![alt text](images/{B919FCB1-CDB0-4C94-9B6C-72D2B670BEBF}.png)
    ![alt text](images/{532F0CA4-576D-4B6F-BFB6-A5F15EF43221}.png)

## Define Schema

![alt text](images/{918D9C54-0008-45B9-98FD-D71295FB8A42}.png)
![alt text](images/{17738AB7-7C00-4D30-8BE7-7645E772A8C1}.png)
![alt text](images/{6D40E645-12B6-40B3-8430-DEBAA48B0C93}.png)
