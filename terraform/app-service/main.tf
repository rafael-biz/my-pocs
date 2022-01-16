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

resource "azurerm_resource_group" "resource_group_demo" {
  name     = "terraform-webapp-demo"
  location = var.location
}

resource "azurerm_app_service_plan" "service_plan_demo" {
  name                = "webapp-demo"
  location            = azurerm_resource_group.resource_group_demo.location
  resource_group_name = azurerm_resource_group.resource_group_demo.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "example" {
  name                = "rbbnodejshelloworld"
  location            = azurerm_resource_group.resource_group_demo.location
  resource_group_name = azurerm_resource_group.resource_group_demo.name
  app_service_plan_id = azurerm_app_service_plan.service_plan_demo.id

  site_config {
    always_on = false
    linux_fx_version = "node|14-lts"
  }

  app_settings = {
    "SOME_KEY" = "some-value"
  }
  
  https_only = true
}

output "url" {
    value = "${azurerm_app_service.example.name}.azurewebsites.net"
}