data "azurerm_policy_definition" "allowed_locations" {
  display_name = "Allowed locations"
}

resource "azurerm_subscription_policy_assignment" "allowed_locations" {
  name                 = "allowed-locations"
  subscription_id      = data.azurerm_subscription.current.id
  policy_definition_id = data.azurerm_policy_definition.allowed_locations.id

  parameters = jsonencode({
    listOfAllowedLocations = {
      value = [
        "westeurope",
        "northeurope"
      ]
    }
  })
}

data "azurerm_subscription" "current" {}
