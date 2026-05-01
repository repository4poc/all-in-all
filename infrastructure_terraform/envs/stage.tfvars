appname = "allinall"
environment = "stage"
region      = "swedencentral"

sku_tier = "Standard"

aks_sys_nodepool_vm_size = "Standard_D2s_v2"
system_node_count = 2

aks_app_nodepool_vm_size = "Standard_D2s_v2"
apps_min     = 2
apps_max     = 4
backend_min     = 2
backend_max     = 4
kube_version_upgrade = patch

enable_monitoring = true
