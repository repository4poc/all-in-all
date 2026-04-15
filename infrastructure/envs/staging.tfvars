appname = "allinall"
environment = "stage"
region      = "swedencentral"

aks_sys_nodepool_vm_size = "Standard_DS2_v2"
system_node_count = 2

aks_app_nodepool_vm_size = "Standard_DS2_v2"
apps_min     = 2
apps_max     = 4
backend_min     = 2
backend_max     = 4

enable_monitoring = true
