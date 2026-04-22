appname = "allinall"
environment = "prod"
region      = "swedencentral"

# AKS Cluster
sku_tier = "Standard"
vnet_address_space = ["10.244.0.0/16"]
subnet_address_space = ["10.244.1.0/22"]

# AKS System node pool
aks_sys_nodepool_vm_size = "Standard_D2s_v2"
system_node_count = 3

# AKS User node pool (app node pool)
aks_app_nodepool_vm_size = "Standard_D4s_v5"
apps_min     = 3
apps_max     = 10
backend_min     = 3
backend_max     = 10
kube_version_upgrade = patch

enable_monitoring = true
