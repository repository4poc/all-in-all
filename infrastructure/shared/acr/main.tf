data "azurerm_resource_group" "rg_acr" {
  name     = "rg-shared"
}

module "acr" {
  source = "../../modules/containers/acr"

  name                = "allinallacr"
  resource_group_name = data.azurerm_resource_group.rg_acr.name
  location            = data.azurerm_resource_group.rg_acr.location

  tags = {
    app = "all-in-all"
    environment = "shared"
  }
}