resource "azuread_group" "aks_admins" {
  description      = "AllInAll Azure Kubernetes Service RBAC Cluster Developer"
  display_name     = "allinall_admin"
  security_enabled = true
}

resource "azuread_group" "aks_developers" {
  description      = "AllInAll Azure Kubernetes Service RBAC Cluster Developer"
  display_name     = "allinall_developer"
  security_enabled = true
}

resource "azuread_group" "aks_readers" {
  description      = "AllInAll Azure Kubernetes Service RBAC Cluster Reader"
  display_name     = "allinall_reader"
  security_enabled = true
}
