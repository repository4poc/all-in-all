appname = "allinall"
environment = "dev"

# AKS Cluster
sku_tier = "Free"

# AKS System node pool
aks_sys_nodepool_vm_size = "Standard_B2s"
system_node_count = 1

# AKS User node pool (app node pool)
aks_app_nodepool_vm_size = "Standard_B2s"
apps_min     = 1
apps_max     = 1
backend_min     = 1
backend_max     = 1
kube_version_upgrade = "rapid"


enable_monitoring = false

tags = {
    "appname" : "allinall",
    "env": "dev"
}

