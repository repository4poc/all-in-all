## Overview

### Different Data Types

- Structured Data (Tables) : Azure SQL Database
- Semi-Structured Data (JSON) : Azure CosmosDB
- UnStructured Data (Images, Vidoes, Zip) : Azure Storage

### Azure SQL Database - Used For Strctured Data

- Use Tables (Rows and Columns)
- Tables with Fixed Schema
- Relational between Tables
- Good for Transactional Data (ACID Characteristics)

### Azure CosmosDB - Used For Unstrctured (JSON) Data

- No SQL, Relational and Vector Database
- No Fixed Schema (Data is in different forms)
- Much efficient in data storage and retrieval
- Examples
  - MongoDB (Open Source)
    - Database -> Container -> Documents (JSON)
  - CosmosDB (Azure Managed)
    - Database -> Container -> Documents (JSON)
- Support different APIs
  - NoSQL :
    - Data is stored as JSON
    - You can query using SQL
  - MongoDB
    - Data is stored as BSON
  - Table
    - Data is stored as key-value pair
  - Casandra
    - Data is stored as column-oriented schema
  - Gramlin
    - Graph based data

## Key Concepts

### Request Units

- In CosmosDB, you dont pay separately for Compute and Memory/IOPS, everything is bundled as a single Unit - RUs
- RU - represent cost of database operation
- when you fetch a single item with id and partition key
  - 1 KB read = 1 RU
- Free Tier support 1000 RU per second.
- So costing is in terms of RUs

### Partition Key

```
    - CosmosDB Account
        - Database
            -  ContainerA : Orders (Partition Key : OrderType)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
            - ContainerB : Complaints (Partition Key: ComplainType)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
                - Item (Item Id)
```

- Container : Hold your JSON documents
- CosmosDB divide data into different partitions depends on the partition key, so choose wisely
- Partition Key : helps in quick search

### Item Id

- Each item in your cosmosDB get item ID within the partition
- Partition Key + Item Id (Unique)
- Item ID = Table Primary Key

\***\*Imp** : To CREATE and UPDATE an Item, we need to provide both Partition Key and ID, without any system properties like \_ts.

## Arrays with JSON Object

![alt text](images/{FFA4BFA4-57F5-4F06-B666-EB3AB13EA858}.png)

**Query** : By flattening the structure using JOIN

```
SELECT o.id,o.courseId as orderId , i.courseName,i.courseId as courseId
FROM Orders o Join i in o.items
WHERE i.courseName = 'C#'
```

## Objects with Objects

![alt text](images/{204553D5-2E42-4D84-956C-5B0F3C915188}.png)

```
SELECT *
FROM Orders o
WHERE o.payment.transationid = 'tx-001'
```

### Physical Partitions

- managed by azure for scaling.

### Create Azure CosmosDB Account

- API Type : No SQL
  - No SQL (Default)
  - MongoDB
  - Casandra
  - Table
  - Gramlin
- Workload Type : Learning
  - Learning
  - Development/Testing
  - Production
- Subscription
- Resource Group
- Account Name : < Unique >
- Availability Zone :
  - Disabled
  - Enabled
    - LRS
    - ZRS
    - GRS
- Region
- Capacity Throughput
  - Serverless
    - Unpredictable workload
    - Billed only for consumed RUs
    - Scaling on-demand
  - Provisioned Throughput
    - Preconfigured RUs
      - Manual
      - Autoscale (Min, Max)
    - Billed per hour
    - Guaranteed Thorughput

## Create Database in CosmosDB Account

- Database ID : < DB Name>

## Create Container in the Database

- Database ID
- Container ID : < Container Name > (Orders)
- Partition Key : /< Partition Key > (/CustomerId)
- Container RU : Only visible for CosmosDB Account with Capacity Throughput : Provisioned Throughput, not for serverless

  ![alt text](images/{53C2AD7C-9E60-4EE0-89BB-A9F2C204F1B3}.png)

## Create Item in the Container

- **Input**
  ![alt text](images/{2A039219-19D3-43C0-A13D-C1A432E020AC}.png)

- **Output**
  ![alt text](images/{130500F2-A83A-4BD4-9416-D7916A2BBC7B}.png)

  So few system based properties are added with the item
  - \_rid
  - \_attachments
  - \_ts
  - \_etag
  - \_self

## Running Query Against Containers

```
SELECT * FROM c where c.id = "ord-1001" (c is selected container)

SELECT * FROM orders c where c.id = "ord-1001"

SELECT * FROM users c where  c.customerName = "varinder gupta"

SELECT * FROM users c where  c.customerName = "varinder gupta" AND c.rating > 3
```

## CosmosDB Query Types

- **In Partition Query** : If your query has partition key with equality filter (= only) specified, CosmosDB automatically optimizes the query, it routes the query to the physical partition

```
Select * from Orders o where o.CustomerId = "cus-101"
```

- **Cross Partition Query** :If your query has nopartition key specified,

```
Select * from Customer c where c.name = "cus-101"
```

## Costing

- RUs (Compute + RAM)
- Storage

## .Net 10

- Dotnet Libraries

```
dotnet new console -n cosmosdb.app

cd cosmosdb.app

dotnet add package Microsoft.Azure.Cosmos
dotnet add package Azure.Identity

Ref:
https://learn.microsoft.com/en-us/azure/cosmos-db/quickstart-dotnet
```

- Create Database and container

```
using Microsoft.Azure.Cosmos;
using Azure.Identity;

const string END_POINT = "https://mycosmosdb2284.documents.azure.com:443/";
const string ACCESS_KEY = "";

CosmosClient client = new CosmosClient(END_POINT, new DefaultAzureCredential());
// CosmosClient client = new CosmosClient(END_POINT, ACCESS_KEY);

static async Task CreateDBContainer(CosmosClient client)
{
    Database database = await client.CreateDatabaseIfNotExistsAsync("order-db");
    Container container = await database.CreateContainerIfNotExistsAsync("orders", "/customerId");

    Console.WriteLine("Database and container created successfully.");
}

await CreateDBContainer(client);
```

- Adding an Item into the container

## Pre-requisite of using DefaultAzureCredential()

**IMP** To use new DefaultAzureCredential(), instead of Access Key, Assign cosmosdb sql role "Cosmos DB Built-in Data Contributor" to the user
Note: This role can not create cosmosdb database and containers.

```
Azure CLI ✅
PowerShell ✅
ARM/Bicep ✅
Terraform ✅
Portal IAM ❌ usually not visible in your current UI
```

1. Assign cosmosdb sql role "Cosmos DB Built-in Data Contributor" to the user
   Note: This role can not create database and containers.

```
az cosmosdb sql role assignment create \
  --resource-group "dev" \
  --account-name "mycosmosdb2284" \
  --role-definition-name "Cosmos DB Built-in Data Contributor" \
  --principal-id "7b83bf86-8ca7-41d4-a300-cb53f61f773d" \
  --scope '//'
```

**principal-id** is Object ID of user logged in

2. Verify the role

```
$ az cosmosdb sql role assignment list --resource-group "dev" --account-name "mycosmosdb2284"


[
  {
    "id": "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23/resourceGroups/dev/providers/Microsoft.DocumentDB/databaseAccounts/mycosmosdb2284/sqlRoleAssignments/2e8c27bd-c2cf-4dfa-88cf-b3f456d76b1c",
    "name": "2e8c27bd-c2cf-4dfa-88cf-b3f456d76b1c",
    "principalId": "7b83bf86-8ca7-41d4-a300-cb53f61f773d",
    "resourceGroup": "dev",
    "roleDefinitionId": "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23/resourceGroups/dev/providers/Microsoft.DocumentDB/databaseAccounts/mycosmosdb2284/sqlRoleDefinitions/00000000-0000-0000-0000-000000000002",
    "scope": "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23/resourceGroups/dev/providers/Microsoft.DocumentDB/databaseAccounts/mycosmosdb2284",
    "type": "Microsoft.DocumentDB/databaseAccounts/sqlRoleAssignments"
  }
]
```

Terraform

```
resource "azurerm_cosmosdb_sql_role_assignment" "data_contributor" {
  resource_group_name = azurerm_resource_group.rg.name
  account_name        = azurerm_cosmosdb_account.cosmos.name

  role_definition_id = "${azurerm_cosmosdb_account.cosmos.id}/sqlRoleDefinitions/00000000-0000-0000-0000-000000000002"
  principal_id       = "<your-object-id>"
  scope              = azurerm_cosmosdb_account.cosmos.id
}
```

## Connect to cosmosDB

```
CosmosClient client = new CosmosClient(END_POINT, new DefaultAzureCredential());
```

## Single Objects - Add items

```
static async Task CreateItem(CosmosClient client)
{
    Container container = client.GetContainer("order-db", "orders");

    Order order = new Order(
        id: Guid.NewGuid().ToString(),
        customerId: "gear-surf-surfboards", // must match PartitionKey
        name: "Yamba Surfboard",
        quantity: 1,
        price: 450.00m,
        clearance: false
    );

    ItemResponse<Order> response = await container.UpsertItemAsync<Order>(
        order,new PartitionKey(order.customerId)
    );

    Console.WriteLine($"Item created with id: {response.Resource.id}");
}

await CreateItem(client);

```

## Arry of Objects - Add items

```
static async Task CreateItem(CosmosClient client)
{
    Container container = client.GetContainer("order-db", "Orders");
    Order[] orders = new Order[]
    {
        new Order
        {
            Id = Guid.NewGuid().ToString(),
            CustomerId = "gear-surf-surfboards",
            UserId = "user123",
            OrderDate = DateTime.UtcNow,
            CourseInfo = new CourseInfo
            {
                CourseId = "course123",
                CourseName = "Surfing 101",
                Price = 199.99m
            },
            PaymentInfo = new PaymentInfo
            {
                PaymentMethod = "Credit Card",
                PaymentStatus = "Completed"
            }
        },
        new Order
        {
            Id = Guid.NewGuid().ToString(),
            CustomerId = "mark",
            UserId = "user1",
            OrderDate = DateTime.UtcNow,
            CourseInfo = new CourseInfo
            {
                CourseId = "course1",
                CourseName = "Surfing 101",
                Price = 199.99m
            },
            PaymentInfo = new PaymentInfo
            {
                PaymentMethod = "Credit Card",
                PaymentStatus = "Completed"
            }
        },

    };

    foreach (var order in orders)
    {
        ItemResponse<Order> response = await container.CreateItemAsync<Order>(
            order, new PartitionKey(order.CustomerId)
        );

        Console.WriteLine($"Item created with id: {response.Resource.Id}, status code: {response.StatusCode}");
    }

}
```

```

public record Order(
    string id,
    string customerId,
    string name,
    int quantity,
    decimal price,
    bool clearance
);

```

## Using Data hierarchy

```
using Newtonsoft.Json;

public class Order
{
    [JsonProperty("id")]    // MUST MATCH THE CONTAINER ID
    public required string Id { get; set; }

    [JsonProperty("CustomerId")] // MUST MATCH THE CONTAINER PARTITON KEY
    public required string CustomerId { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; } = default!;

    [JsonProperty("orderDate")]
    public DateTime OrderDate { get; set; }

    [JsonProperty("courseInfo")]
    public CourseInfo CourseInfo { get; set; } = default!;


    [JsonProperty("paymentInfo")]
    public PaymentInfo PaymentInfo { get; set; } = default!;
}

public class CourseInfo
{
    [JsonProperty("courseId")]
    public required string CourseId { get; set; }

    [JsonProperty("courseName")]
    public required string CourseName { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}

public class PaymentInfo
{
    [JsonProperty("paymentMethod")]
    public required string PaymentMethod { get; set; }

    [JsonProperty("paymentStatus")]
    public required string PaymentStatus { get; set; }
}
```

## Read Data - With hierarchy

```

static async Task ReadItem(CosmosClient client)
{
    Container container = client.GetContainer("order-db", "orders");
    string query = "SELECT * FROM orders o WHERE o.customerId = @customerId";

    var queryDefinition = new QueryDefinition(query)
      .WithParameter("@customerId", "gear-surf-surfboards");

    using FeedIterator<Order> order = container.GetItemQueryIterator<Order>(
        queryDefinition
    );

    Console.WriteLine("Reading items from the container...");

    while (order.HasMoreResults)
    {
        FeedResponse<Order> response = await order.ReadNextAsync();
        foreach (Order o in response)
        {
            Console.WriteLine($"Read item with id: {o.id} customer id : {o.customerId} name : {o.name} quantity : {o.quantity} price : {o.price} payment method : {o.payment.method}");
        }
    }

}

await ReadItem(client);

```

## Update Data

Best approach is

- Fetch the record
- Set the Updated value
- Update the record

```

static async Task UpdateItem(CosmosClient client)
{
    Container container = client.GetContainer("order-db", "orders");

    Order order = new Order(
        id: "3a1d5c03-6a97-42a4-ba44-ed6b19a1727d",
        customerId: "gear-surf-surfboards",
        name: "Yamba Surfboard 1.0",
        quantity: 2,
        price: 450.00m,
        clearance: false
    );

    ItemResponse<Order> response = await container.UpsertItemAsync<Order>(
        order, new PartitionKey(order.customerId)
    );

    Console.WriteLine($"Item updated with id: {response.Resource.id}");
}

```

## Delete the record

```

static async Task DeleteItem(CosmosClient client)
{
    Container container = client.GetContainer("order-db", "orders");

    string id = "3a1d5c03-6a97-42a4-ba44-ed6b19a1727d";
    string partitionKey = "gear-surf-surfboards";

    ItemResponse<Order> response = await container.DeleteItemAsync<Order>(
        id, new PartitionKey(partitionKey)
    );

    Console.WriteLine($"Item deleted with id: {id}");
}
//await CreateDBContainer(client);

//await CreateItem(client);

//await ReadItem(client);

//await UpdateItem(client);

await DeleteItem(client);
```
