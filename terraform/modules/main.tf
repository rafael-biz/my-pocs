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
  name     = "terraform-module-demo"
  location = var.location
}

module "storage" {
  source  = "./storage"

  resource_group = azurerm_resource_group.rg
}

module "webapps" {
  source  = "./webapps"

  resource_group = azurerm_resource_group.rg

  storage = {
      connection_string = module.storage.connection_string
      container_name = module.storage.container_name
  }
}