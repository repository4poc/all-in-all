output "aks_admin_group_object_id" {
  value = azuread_group.aks_admins.object_id
}

output "aks_developer_group_object_id" {
  value = azuread_group.aks_developers.object_id
}

output "aks_reader_group_object_id" {
  value = azuread_group.aks_readers.object_id
}
