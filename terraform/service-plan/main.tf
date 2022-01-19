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
  name     = "service-plan-demo"
  location = var.location
}

##########################################################
# Windows
# Standard Tier - S1
# 100 total ACU
# 1.75 Gb Memory
# A-Series
##########################################################
resource "azurerm_app_service_plan" "plan_1" {
  name                = "demo-1"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "FunctionApp"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

##########################################################
# Windows
# Free Tier - F1
# 1 Gb Memory
# 60 minutes/day compute
##########################################################
resource "azurerm_app_service_plan" "plan_2" {
  name                = "demo-2"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Windows"

  sku {
    tier = "Free"
    size = "F1"
  }
}

##########################################################
# Linux
# Free Tier - F1
# 1 Gb Memory
# 60 minutes/day compute
##########################################################
resource "azurerm_app_service_plan" "plan_3" {
  name                = "demo-3"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Free"
    size = "F1"
  }
}

##########################################################
# Linux
# Standard Tier - S1
# 100 total ACU
# 1.75 Gb Memory
# A-Series
##########################################################
resource "azurerm_app_service_plan" "plan_4" {
  name                = "demo-4"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Standard"
    size = "S1"
  }
}