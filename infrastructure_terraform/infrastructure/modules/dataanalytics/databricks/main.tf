
resource "azurerm_databricks_workspace" "dbx" {
  name                = "dbx-${var.appname}-${var.environment}"
  resource_group_name = var.resource_group_name
  location            = var.location

  sku = "premium" # options: standard, premium, trial

  tags = var.tags
}
