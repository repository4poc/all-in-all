appname = "allinall"
environment = "dev"
region      = "swedencentral"

aks_sys_nodepool_vm_size = "Standard_B2s"
system_node_count = 1

aks_app_nodepool_vm_size = "Standard_B2s"
apps_min     = 1
apps_max     = 2
backend_min     = 1
backend_max     = 2

enable_monitoring = false

tags = {
    "appname" : "allinall",
    "env": "dev"
}