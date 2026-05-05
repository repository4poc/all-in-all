appname = "allinall"
environment = "dev"
region = "swedencentral"

# AKS Cluster
sku_tier = "Free"
private_cluster_enabled = false

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

tenant_id = "be6f99f0-eabe-46e9-8b0a-6a270e401649"