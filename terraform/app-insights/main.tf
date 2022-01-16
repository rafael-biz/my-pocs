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
  name     = "terraform-appinsights-demo"
  location = var.location
}

resource "azurerm_log_analytics_workspace" "example" {
  name                = "workspace-test"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_application_insights" "app_insight" {
  name                = "tf-test-appinsights"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  workspace_id        = azurerm_log_analytics_workspace.example.id
  application_type    = "web"
}

output "instrumentation_key" {
  value = nonsensitive(azurerm_application_insights.app_insight.instrumentation_key)
}

output "app_id" {
  value = azurerm_application_insights.app_insight.app_id
}