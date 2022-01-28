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
  name     = "function-app-demo"
  location = var.location
}

resource "azurerm_storage_account" "sa" {
  name                     = "rbbfunctionappdemo"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "demo" {
  name                  = "blob-demo"
  storage_account_name  = azurerm_storage_account.sa.name
  container_access_type = "private"
}

resource "azurerm_storage_queue" "example" {
  name                 = "queue-demo"
  storage_account_name = azurerm_storage_account.sa.name
}

resource "azurerm_storage_table" "cars" {
  name                 = "cars"
  storage_account_name = azurerm_storage_account.sa.name
}

resource "azurerm_log_analytics_workspace" "demo" {
  name                = "function-app-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_application_insights" "app_insights" {
  name                = "rbb-functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  workspace_id        = azurerm_log_analytics_workspace.demo.id
  application_type    = "web"
}

resource "azurerm_servicebus_namespace" "demo" {
  name                = "rbb-functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "Standard"
}

resource "azurerm_servicebus_queue" "demo" {
  name         = "queue_demo"
  namespace_id = azurerm_servicebus_namespace.demo.id

  enable_partitioning = true
}

resource "azurerm_servicebus_queue_authorization_rule" "demo" {
  name     = "Send_Read"
  queue_id = azurerm_servicebus_queue.demo.id

  listen = true
  send   = true
  manage = false
}

resource "azurerm_servicebus_topic" "demo" {
  name         = "topic_demo"
  namespace_id = azurerm_servicebus_namespace.demo.id

  enable_partitioning = true
}

resource "azurerm_servicebus_topic_authorization_rule" "demo" {
  name     = "Send_Read"
  topic_id = azurerm_servicebus_topic.demo.id

  listen = true
  send   = true
  manage = false
}

resource "azurerm_servicebus_subscription" "client_1" {
  name               = "client_1"
  topic_id           = azurerm_servicebus_topic.demo.id
  max_delivery_count = 1
}

resource "azurerm_servicebus_subscription" "client_2" {
  name               = "client_2"
  topic_id           = azurerm_servicebus_topic.demo.id
  max_delivery_count = 1
}

resource "azurerm_app_service_plan" "plan" {
  name                = "functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Windows"
  reserved            = false

  sku {
    capacity = 1
    tier     = "Standard"
    size     = "S1"
  }
}

resource "azurerm_function_app" "demo" {
  name                       = "rbbfunctionappdemo"
  location                   = azurerm_resource_group.rg.location
  resource_group_name        = azurerm_resource_group.rg.name
  app_service_plan_id        = azurerm_app_service_plan.plan.id
  storage_account_name       = azurerm_storage_account.sa.name
  storage_account_access_key = azurerm_storage_account.sa.primary_access_key
  version                    = "~3"
  https_only                 = false

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"              = "dotnet",
    "WEBSITE_RUN_FROM_PACKAGE"              = "1"
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = azurerm_application_insights.app_insights.instrumentation_key,
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = azurerm_application_insights.app_insights.connection_string
    "CONNECTION_STRING_SB_QUEUE"            = trimsuffix("${azurerm_servicebus_queue_authorization_rule.demo.primary_connection_string}", ";EntityPath=${azurerm_servicebus_queue.demo.name}")
    "CONNECTION_STRING_SB_TOPIC"            = trimsuffix("${azurerm_servicebus_topic_authorization_rule.demo.primary_connection_string}", ";EntityPath=${azurerm_servicebus_topic.demo.name}")
    "CONNECTION_STRING_SA_QUEUE"            = azurerm_storage_account.sa.primary_connection_string
  }

  site_config {
    app_scale_limit           = 1
    elastic_instance_minimum  = 1
    always_on                 = true
    min_tls_version           = "1.2"
    use_32_bit_worker_process = true
  }
}

output "sas_url_query_string" {
  value = nonsensitive(azurerm_storage_account.sa.primary_access_key)
}