resource "kubernetes_namespace_v1" "argocd" {
  count = 0 # Skipped , put it to 1
  metadata {
    name = "argocd"
  }

  depends_on = [
    module.aks
  ]
}

resource "helm_release" "argocd" {
  count = 0 # Skipped , put it to 1

  name      = "argocd"
  namespace = kubernetes_namespace_v1.argocd[0].metadata[0].name

  repository = "https://argoproj.github.io/argo-helm"
  chart      = "argo-cd"

  values = [
    file("${path.module}/argocd/values.yaml")
  ]

  depends_on = [
    kubernetes_namespace_v1.argocd
  ]
}
