variable "storage" {
  type = object({
    connection_string = string
    container_name = string
  })
}

variable "resource_group" {
  type = object({
    name = string
    location = string
  })
}

resource "azurerm_app_service_plan" "plan" {
  name                = "module-demo"
  location            = var.resource_group.location
  resource_group_name = var.resource_group.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "example" {
  name                = "rbbmobuledemo"
  location            = var.resource_group.location
  resource_group_name = var.resource_group.name
  app_service_plan_id = azurerm_app_service_plan.plan.id

  site_config {
    always_on = false
    linux_fx_version = "node|14-lts"
  }

  app_settings = {
    "SOME_KEY" = "some-value"
    "AZURE_STORAGE_CONNECTION_STRING" = "${var.storage.connection_string}"
    "AZURE_STORAGE_CONTAINER" = "${var.storage.container_name}"
  }
  
  https_only = true
}