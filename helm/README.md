### Helm

Helm is a package manager for kubernetes, allow you to package/install/upgrade/uninstall kubernetes controllers and 3rd Party/custom applications

```
- JavaScript --> NPM
- C#         --> Nuget
- Java       --> Maven
- Python     --> Pip
- Kubernetes --> Helm
- Linux      --> Apt
- Window     --> winget
```

### Kubernetes Packages

```
kubernetes controllers
- grafana
- ArgoCD
- prometheus

3rd Party applications
- nginx
- busybox
```

### Steps to install package with helm

```
1. Add repository that holds helm packages (called - charts)
2. helm install <package/chart/application>
```

### Why Helm

Else you have to create custom scripts for insall/upgrade/uninstall along with manage dependencies for each application and keep it upto date.

### How Helm know which kubernetes cluster to interact with

The way kubectl look for the current context on the `~/.kube/config´ file, sane is with helm

```
When using AKS, you usually don’t manually create contexts. Instead, use Azure CLI:

az login
az aks get-credentials \
  --resource-group my-rg \
  --name my-aks-cluster

1. kubectl config get-contexts
2. kubectl config use-context aks-dev
3. kubectl config current-context
```

### Install helm

```
On Windows:

1. winget install Helm.Helm

2. helm version

3. helm repo add bitnami https://charts.bitnami.com/bitnami

4. helm repo list

5. helm search repo bitnami | findstr nginx (grep for linux)

6. helm install my-nginx bitnami/nginx -n <namespace> (my-nginx = release)

7. kubectl get pods -n <namespace>
```

```
For ArgoCD

helm repo add argo https://argoproj.github.io/argo-helm
```

### How to use Kustomization.yaml, but when using ArgoCD we donot use Kustomization

To execute kustomization.yaml

1. cd all-in-all/helm/frontend/templates
2. $ kubectl apply -k .
