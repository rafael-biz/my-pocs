terraform {
  required_version = ">= 1.1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.91"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.15"
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
  name     = "ad-demo"
  location = var.location
}

resource "azurerm_aadb2c_directory" "demo" {
  data_residency_location = "United States"
  display_name            = "rbbizdemo"
  domain_name             = "rbbizdemo.onmicrosoft.com"
  resource_group_name     = azurerm_resource_group.rg.name
  sku_name                = "PremiumP1"
}