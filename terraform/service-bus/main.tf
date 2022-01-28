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
  name     = "service-bus-demo"
  location = var.location
}

resource "azurerm_servicebus_namespace" "demo" {
  name                = "rbb-functions-demo"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku                 = "Standard"
}

resource "azurerm_servicebus_queue" "demo" {
  name         = "my_queue"
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
  name         = "my_topic"
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