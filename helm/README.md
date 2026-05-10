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

5. helm search repo bitnami | findstr argocd (grep for linux)

6. helm install my-nginx bitnami/nginx -n <namespace> (my-nginx = release)

7. kubectl get pods -n <namespace>
```

```
For ArgoCD

1. helm repo add argo https://argoproj.github.io/argo-helm

2. helm search repo argo | findstr argocd

3. helm install argocd-deployment argo/argocd-apps -n argocd

4.kubectl port-forward service/argocd-server 8080:443 -n argocd

5. kubectl get secret argocd-initial-admin-secret -n argocd -o jsonpath="{.data.password}" | base64 -d && echo
```

### How to use Kustomization.yaml, but when using ArgoCD we donot use Kustomization

To execute kustomization.yaml

1. cd all-in-all/helm/frontend/templates
2. $ kubectl apply -k .

### How to use helm for manual deployment

```
For Dev

helm upgrade --install frontend ./helm/frontend -n apps -f ./helm/frontend/values-dev.yaml

---For AI Application---

kubectl create secret generic frontend-secret \
  -n apps \
  --from-literal=VITE_AZURE_API_KEY="<real-api-key>"

helm upgrade --install backendexpress ./helm/backendexpress -n apps -f ./helm/backendexpress/values-dev.yaml



For Prod

helm upgrade --install frontend ./helm/frontend -n apps -f ./helm/frontend/values-prod.yaml


helm uninstall frontend -n apps
```

### ArgoCD Implementation

```
apiVersion: argoproj.io/v1alpha1
kind: Application
metadata:
  name: frontend
  namespace: argocd
spec:
  source:
    repoURL: https://github.com/your-org/gitops-repo.git
    targetRevision: main
    path: apps/frontend
    helm:
      valueFiles:
        - values-dev.yaml
  destination:
    server: https://kubernetes.default.svc
    namespace: apps
  syncPolicy:
    automated:
      prune: true
      selfHeal: true
```

Your helm chart contains

```
apps/frontend/
  Chart.yaml
  values-dev.yaml
  templates/
    deployment.yaml
    service.yaml
    ingress.yaml
    pdb.yaml
```

| Workload          | Recommended    |
| ----------------- | -------------- |
| Stateless APIs    | HPA            |
| Memory-heavy apps | VPA            |
| Databases         | Usually manual |
| AI inference      | Sometimes VPA  |
| Java apps         | VPA helpful    |
| Batch jobs        | KEDA + VPA     |

| Autoscaler | Scales What        |
| ---------- | ------------------ |
| HPA        | Number of pods     |
| VPA        | Pod size/resources |
