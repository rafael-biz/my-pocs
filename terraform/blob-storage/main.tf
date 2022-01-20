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

resource "azurerm_resource_group" "demo_resource_group" {
  name     = "terraform-blob-demo"
  location = var.location
}

resource "azurerm_storage_account" "demo_account" {
  name                     = "rbbblobdemo"
  location                 = azurerm_resource_group.demo_resource_group.location
  resource_group_name      = azurerm_resource_group.demo_resource_group.name
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "demo_container" {
  name                  = "rbbblobdemo"
  storage_account_name  = azurerm_storage_account.demo_account.name
  container_access_type = "private"
}

resource "azurerm_storage_blob" "demo_file" {
  name                   = "helloworld.txt"
  storage_account_name   = azurerm_storage_account.demo_account.name
  storage_container_name = azurerm_storage_container.demo_container.name
  type                   = "Block"
  source                 = "helloworld.txt"
}

data "azurerm_storage_account_blob_container_sas" "container_sas" {
  connection_string = azurerm_storage_account.demo_account.primary_connection_string
  container_name    = azurerm_storage_container.demo_container.name
  https_only        = true

  start  = "2022-01-01"
  expiry = "2025-01-01"

  permissions {
    read   = true
    add    = false
    create = false
    write  = false
    delete = false
    list   = false
  }

  content_disposition = "inline"
}

output "sas_token" {
  value = nonsensitive(data.azurerm_storage_account_blob_container_sas.container_sas.sas)
}

output "hello_world_url" {
  value = "${azurerm_storage_blob.demo_file.url}${nonsensitive(data.azurerm_storage_account_blob_container_sas.container_sas.sas)}"
}