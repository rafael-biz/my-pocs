terraform {
  required_version = ">= 1.1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.91"
    }
  }
  backend "azurerm" {
    resource_group_name  = "terraform-remote-state-demo"
    storage_account_name = "rbbterraformstatedemo"
    container_name       = "terraform-state"
    key                  = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

variable "location" {
  type    = string
  default = "brazilsouth"
}

# Create a resource group
resource "azurerm_resource_group" "demo" {
  name     = "my-terraform-demo"
  location = var.location
}