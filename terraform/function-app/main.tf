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
  name     = "rbbfunctionappdemo"
  location = var.location
}

resource "azurerm_storage_account" "sa" {
  name                     = "rbbfunctionappdemo"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_log_analytics_workspace" "app_insights_workspace" {
  name                = "workspace-test"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_application_insights" "app_insights" {
  name                = "rbb-functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  workspace_id        = azurerm_log_analytics_workspace.app_insights_workspace.id
  application_type    = "web"
}

resource "azurerm_app_service_plan" "plan" {
  name                = "functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Windows"

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_function_app" "demo" {
  name                       = "rbbfunctionappdemo"
  location                   = azurerm_resource_group.rg.location
  resource_group_name        = azurerm_resource_group.rg.name
  app_service_plan_id        = azurerm_app_service_plan.plan.id
  storage_account_name       = azurerm_storage_account.sa.name
  storage_account_access_key = azurerm_storage_account.sa.primary_access_key
  os_type                    = "" /* Windows */
  version                    = "~4"
  https_only                 = false
  auth_settings {
      enabled                        = false
      runtime_version                = "~1"
      unauthenticated_client_action  = "AllowAnonymous"
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"              = "node",
    "WEBSITE_NODE_DEFAULT_VERSION"          = "~14"
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = azurerm_application_insights.app_insights.instrumentation_key,
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = azurerm_application_insights.app_insights.connection_string
  }

  site_config {
    always_on             = false
    cors {
      allowed_origins     = [
        "https://portal.azure.com",
      ]
      support_credentials = false
    }
  }
}