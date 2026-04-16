appname = "allinall"
environment = "prod"
region      = "swedencentral"

sku_tier = "Standard"

aks_sys_nodepool_vm_size = "Standard_D2s_v2"
system_node_count = 3

aks_app_nodepool_vm_size = "Standard_D4s_v5"
apps_min     = 3
apps_max     = 10
backend_min     = 3
backend_max     = 10
kube_version_upgrade = patch

enable_monitoring = true
