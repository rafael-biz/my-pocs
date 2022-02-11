# Azure Cosmos DB

Some of the features and functionalities covered in this demo include:
 * Create an Azure Cosmos DB Core (SQL) with Terraform.
 * CRUD Demo

# Prerequisites

 * Azure Cli Tool version 2.32.0 or higher.

 * An Azure subscription.

# Sign in

In your terminal, use the Azure CLI tool to setup your account permissions locally.

```
az login
```

# Create a Resource Group

Replace location variable string below with a region that makes sense for you.

```
az group create --name my-store-demo --location brazilsouth
```

# Create Cosmos DB account

Create a SQL API Cosmos DB account with session consistency and multi-region writes enabled.

```
az cosmosdb create
    --resource-group my-store-demo
    --name rbb-my-store-demo
    --kind GlobalDocumentDB
    --locations regionName="brazilsouth" failoverPriority=0
    --locations regionName="eastus2" failoverPriority=1
    --default-consistency-level "Session"
    --enable-multiple-write-locations true
```

# Create Redis Cache

```
az redis create 
	--location "brazilsouth"
	--name "rbbmystore"
	--resource-group "my-store-demo"
	--sku "Basic"
	--vm-size "c0"
```

# Configure

Open `launchSettings.json` and update `EndpointUri` and `PrimaryKey` settings.

You can find them on **Azure Portal** > **Azure Cosmos DB** > **rbb-cosmosdb-demo** > **Keys**.

Open `launchSettings.json` and find `Redis:ConnectionString` entry.

Replace `<host-name>` with your cache host name.

You can find the host name on **Azure Portal** > **Azure Cache for Redis** > **rbbredisdemo** > **Properties**.

Replace `<access-key>` with the primary key for your cache.

You can find the access key on **Azure Portal** > **Azure Cache for Redis** > **rbbredisdemo** > **Access keys**.

# Cleanup

```
az group delete --name cosmosdb-demo
```

# Reference

- [Azure Cosmos DB Documentation](https://docs.microsoft.com/azure/cosmos-db/index)
- [Azure Cosmos DB .NET SDK for SQL API](https://docs.microsoft.com/azure/cosmos-db/sql-api-sdk-dotnet)
- [Azure Cosmos DB .NET SDK Reference Documentation](https://docs.microsoft.com/dotnet/api/overview/azure/cosmosdb?view=azure-dotnet)
