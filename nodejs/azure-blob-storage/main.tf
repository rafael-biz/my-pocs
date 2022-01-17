terraform {
  required_version = ">= 1.1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.91"
    }
  }
}

provider "azurerm" {
  features {}
}

variable "location" {
  type    = string
  default = "brazilsouth"
}

resource "azurerm_resource_group" "rg" {
  name     = "rbb-nodejs-blob-demo"
  location = var.location
}

resource "azurerm_storage_account" "demo_account" {
  name                     = "rbbnodejsblobdemo"
  location                 = azurerm_resource_group.rg.location
  resource_group_name      = azurerm_resource_group.rg.name
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "container" {
  name                  = "rbb-nodejs-container"
  storage_account_name  = azurerm_storage_account.demo_account.name
  container_access_type = "private"
}

data "azurerm_storage_account_blob_container_sas" "container_sas" {
  connection_string = azurerm_storage_account.demo_account.primary_connection_string
  container_name    = azurerm_storage_container.container.name
  https_only        = true

  start  = "2022-01-01"
  expiry = "2025-01-01"

  permissions {
    read   = true
    add    = true
    create = true
    write  = true
    delete = true
    list   = true
  }

  content_disposition = "inline"
}

resource "azurerm_app_service_plan" "plan" {
  name                = "webapp-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "example" {
  name                = "rbbnodejsblobdemo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.plan.id

  site_config {
    always_on = false
    linux_fx_version = "node|14-lts"
  }

  app_settings = {
    "SOME_KEY" = "some-value"
    "AZURE_STORAGE_CONNECTION_STRING" = "${data.azurerm_storage_account_blob_container_sas.container_sas.connection_string}"
    "AZURE_STORAGE_CONTAINER" = "${azurerm_storage_container.container.name}"
  }
  
  https_only = true
}

output "connection_string" {
  value = nonsensitive(data.azurerm_storage_account_blob_container_sas.container_sas.connection_string)
}