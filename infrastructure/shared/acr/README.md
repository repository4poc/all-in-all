🧱 1. Architecture

```bash
Shared Infra (separate RG)
  └── ACR (single)

Environments
  ├── dev (AKS / App Service / etc.)
  ├── staging
  └── prod

```

👉 ACR is provisioned once, everything else consumes it

📁 2. Terraform structure (recommended)

```bash
terraform/
  modules/
    acr/
  shared/
    acr/
      main.tf
      backend.tf

  envs/
    dev/
    staging/
    prod/
```

🚀 4. Deploy shared ACR (only once)

🔐 5. Grant access to environments

Each environment (AKS, App Service, etc.) needs pull access.

```bash
resource "azurerm_role_assignment" "acr_pull" {
  principal_id         = var.principal_id   # Managed Identity / SPN
  role_definition_name = "AcrPull"
  scope                = var.acr_id
}
```

👉 Typical mapping:

```bash
| Environment | Access            |
| ----------- | ----------------- |
| Dev         | AcrPush + AcrPull |
| Staging     | AcrPull           |
| Prod        | AcrPull           |
```

🔁 6. CI/CD pipeline design (IMPORTANT)

Step 1 — Build & push (dev)

```bash
docker build -t myacr.azurecr.io/myapp:dev-<build-id> .
docker push myacr.azurecr.io/myapp:dev-<build-id>
```

Step 2 — Promote image (no rebuild!)

```bash
az acr import \
  --name myacr \
  --source myacr.azurecr.io/myapp:dev-123 \
  --image myapp:staging-123

```

Then

```bash
az acr import \
  --name myacr \
  --source myacr.azurecr.io/myapp:staging-123 \
  --image myapp:prod-123

```

👉 This ensures:

- Same artifact across envs
- No “works in dev but not prod” issues

🏷️ 7. Tagging strategy (CRITICAL)

Use immutable tags

Good:

```bash
myapp:1.0.0
myapp:1.0.0-build123
myapp:prod-1.0.0
```

Bad:

```bash
myapp:latest ❌
```

🧹 8. Cost optimization

Enable retention policy: Clean old images automatically

```bash
resource "azurerm_container_registry" "acr" {
  # ...

  retention_policy {
    days    = 30
    enabled = true
  }
}
```

🔐 9. Security best practices

✅ Do this:

- Disable admin user ✔
- Use Managed Identity ✔
- Use RBAC ✔
- Enable Private Endpoint (if enterprise) ✔

🧠 10. Referencing ACR in env Terraform

In envs/dev:

```bash
data "azurerm_container_registry" "acr" {
  name                = "mysharedacr123"
  resource_group_name = "rg-shared-acr"
}
```

🔥 Final recommended flow

1. Deploy ACR once (shared)
2. Build images in CI → push to ACR
3. Promote images across envs
4. Environments pull images (read-only)
5. Clean old images automatically
