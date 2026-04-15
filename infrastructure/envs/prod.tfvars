appname = "allinall"
environment = "prod"
region      = "swedencentral"

aks_sys_nodepool_vm_size = "Standard_DS2_v2"
system_node_count = 2

aks_app_nodepool_vm_size = "Standard_D4s_v5"
apps_min     = 3
apps_max     = 10
backend_min     = 3
backend_max     = 10

enable_monitoring = true
