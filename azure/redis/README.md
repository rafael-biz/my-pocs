# Redis Demo

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
az group create --name redis-demo --location brazilsouth
```

# Create Redis Cache

```
az redis create 
	--location "brazilsouth"
	--name "rbbredisdemo"
	--resource-group "redis-demo"
	--sku "Basic"
	--vm-size "c0"
```

# Configure

Open `App.config` and find `CacheConnection` entry.

Replace `<host-name>` with your cache host name.

You can find the host name on **Azure Portal** > **Azure Cache for Redis** > **rbbredisdemo** > **Properties**.

Replace `<access-key>` with the primary key for your cache.

You can find the access key on **Azure Portal** > **Azure Cache for Redis** > **rbbredisdemo** > **Access keys**.

# Run

Compile and run!

# Cleanup

```
az group delete --name redis-demo
```

# Reference

- [Quickstart: Use Azure Cache for Redis in .NET Core](https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-dotnet-core-quickstart)
