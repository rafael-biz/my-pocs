variable "resource_group" {
  type = object({
    name = string
    location = string
  })
}

resource "azurerm_storage_account" "demo" {
  name                     = "rbbmoduledemo"
  location                 = var.resource_group.location
  resource_group_name      = var.resource_group.name
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "demo_container" {
  name                  = "rbbcontainer"
  storage_account_name  = azurerm_storage_account.demo.name
  container_access_type = "private"
}

data "azurerm_storage_account_blob_container_sas" "container_sas" {
  connection_string = azurerm_storage_account.demo.primary_connection_string
  container_name    = azurerm_storage_container.demo_container.name
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

output "connection_string" {
  value = data.azurerm_storage_account_blob_container_sas.container_sas.connection_string
}

output "container_name" {
  value = azurerm_storage_container.demo_container.name
}