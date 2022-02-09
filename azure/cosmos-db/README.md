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
az group create --name cosmosdb-demo --location brazilsouth
```

# Create Cosmos DB account

Create a SQL API Cosmos DB account with session consistency and multi-region writes enabled.

```
az cosmosdb create
    --resource-group cosmosdb-demo
    --name rbb-cosmosdb-demo
    --kind GlobalDocumentDB
    --locations regionName="brazilsouth" failoverPriority=0
    --locations regionName="eastus2" failoverPriority=1
    --default-consistency-level "Session"
    --enable-multiple-write-locations true
```

# Configure

Open `launchSettings.json` and update `EndpointUri` and `PrimaryKey` settings.

You can find them on **Azure Portal** > **Azure Cosmos DB** > **rbb-cosmosdb-demo** > **Keys**.

# Cleanup

```
az group delete --name cosmosdb-demo
```

# Reference

- [Azure Cosmos DB Documentation](https://docs.microsoft.com/azure/cosmos-db/index)
- [Azure Cosmos DB .NET SDK for SQL API](https://docs.microsoft.com/azure/cosmos-db/sql-api-sdk-dotnet)
- [Azure Cosmos DB .NET SDK Reference Documentation](https://docs.microsoft.com/dotnet/api/overview/azure/cosmosdb?view=azure-dotnet)
