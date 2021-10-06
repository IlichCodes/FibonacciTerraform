terraform {
   backend "azurerm" {
    resource_group_name  = "slitvinovstates"
    storage_account_name = "slitvinovsa"
    container_name       = "terraform-state"
    key                  = "terraform.tfstate"
  }
  required_providers{
      azurerm = {
          source = "hashicorp/azurerm"
          version = "~>2.31.1"
      }
  }
}

provider "azurerm" {
  skip_provider_registration = true
    features {      
    }  
}

resource "azurerm_resource_group" "rg" {
  name     = "fibonacci-terraform"
  location = "West Europe"
}

resource "azurerm_storage_account" "sa" {
  name                     = "fibonacciterraformsa"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "sp" {
  name                = "fibonacci-terraform-sp"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_function_app" "example" {
  name                       = "fibonacci-terraform-functions"
  location                   = azurerm_resource_group.rg.location
  resource_group_name        = azurerm_resource_group.rg.name
  app_service_plan_id        = azurerm_app_service_plan.sp.id
  storage_account_name       = azurerm_storage_account.sa.name
  storage_account_access_key = azurerm_storage_account.sa.primary_access_key
}