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

variable "failover_location" {
  type    = string
  default = "eastus2"
}

resource "azurerm_resource_group" "rg" {
  name     = "cosmos-db"
  location = var.location
}

resource "azurerm_cosmosdb_account" "db" {
  name                = "rbb-cosmos-db"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  enable_automatic_failover = false

  backup {
    interval_in_minutes = 240
    retention_in_hours  = 8
    storage_redundancy  = "Zone"
    type                = "Periodic"
  }

  capabilities {
    name = "EnableServerless"
  }

  consistency_policy {
    consistency_level       = "Session"
    max_interval_in_seconds = 5
    max_staleness_prefix    = 100
  }

  geo_location {
    failover_priority = 0
    location          = "brazilsouth"
    zone_redundant    = false
  }
}