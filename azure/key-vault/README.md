# Key Vault Demo

In this project, you will find a demo for create, set and retrieve a secret from
Azure Key Vault.

# Key Vault

Azure Key Vault is a cloud service for securely storing and accessing secrets. A
secret is anything that you want to tightly control access to, such as API keys,
passwords, certificates, or cryptographic keys.

# Authentication

Authentication with Key Vault works in conjunction with Azure Active Directory
(Azure AD), which is responsible for authenticating the identity of any given
**security principal**.

A **security principal** is an object that represents a **user**, **group**,
**service**, or **application** that's requesting access to Azure resources.

For applications, there are two ways to obtain a service principal: with a
*managed identity* or an *application principal* with your Azure AD tenant.

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
az group create --name key-vault-demo --location brazilsouth
```

# Create a Key Vault

Replace `name` variable with a globaly unique name across Azure.

```
az keyvault create --name my-key-vault --resource-group key-vault-demo --location brazilsouth
```

# Add a secret

Replace `vault-name` variable with the name of the Key Vault.

```
az keyvault secret set --vault-name my-key-vault --name "MyPassword" --value "Hello!"
```

# Show the secret

Replace `vault-name` variable with the name of the Key Vault.

```
az keyvault secret show --name "MyPassword" --vault-name my-key-vault
```

# Create a Service Principal

Create a *Service Principal* and configure its access to Azure resources.

```
az ad sp create-for-rbac -n "MyConsoleApp"
```

Save the returned `appid`, `password`, and `tenant` fields for later.

# Authorizing

To authorize `MyConsoleApp` to access the key or secret in the vault, replace `spn` variable
with `appId` returned on last command.

```
az keyvault set-policy -n rbb-my-key-vault --key-permissions get --secret-permissions get --spn "fc09a75d-0e71-4cc1-8328-63c7cbe15dd5"
```

# Run

Open the console application, replace the variables and run.

The expected result is:

```
Retrieving secret...
The secret value is: Hello!
```

# Cleanup

```
az group delete --name key-vault-demo
```

# Reference

 * [Azure Key Vault basic concepts](https://docs.microsoft.com/en-us/azure/key-vault/general/basic-concepts)
 * [Tutorial: Use a managed identity to connect Key Vault to an Azure web app in .NET](https://docs.microsoft.com/en-us/azure/key-vault/general/tutorial-net-create-vault-azure-web-app)
 * [Authentication in Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/general/authentication)
