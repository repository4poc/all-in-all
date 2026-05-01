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

For shared infra (like your ACR):

✔ Grant:

- Reader at subscription level
- Contributor at RG level

👉 This avoids these issues entirely

ACR Protection (FIRST line of defense)

```
✔ Terraform
✅ 1. Use lifecycle.prevent_destroy

✔ Azure
🛡️ 2. Azure Resource Lock (STRONG protection) as Terraform can be bypassed — Azure lock cannot.

✔ Architecture
Separate state for shared infra
```

Lock Types

```
| Type         | Effect                 |
| ------------ | ---------------------- |
| CanNotDelete | ✅ Prevent delete      |
| ReadOnly     | 🚫 Prevent all changes |

```

👉 Someone deletes ACR → all envs break

🔥 Pro tip (used in enterprises)

For shared resources:

Separate backend/state
Separate pipeline
Restricted RBAC

👉 Treat them like “platform layer”

---

📁 2. Architecture best practice (MOST important)

👉 Separate shared infra from env Terraform

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

🧱 Flow overview
Developer → Git push → CI/CD → Build image → Push to ACR → Deploy

👉 Humans don’t push images — pipelines do for all env.

🟢 DEV environment (what’s acceptable)

- Pipeline-based (recommended)
  - Dev pushes code
  - Pipeline builds Docker image
  - Pipeline pushes to Azure Container Registry
  - Dev environment deploys

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

🧠 Why enterprises avoid manual pushes

| Risk                   | Explanation                 |
| ---------------------- | --------------------------- |
| ❌ No traceability     | Who pushed what?            |
| ❌ Security risk       | Credentials exposed         |
| ❌ Inconsistent builds | Works on my machine problem |
| ❌ No audit trail      | Compliance issue            |

🔥 Final recommended flow

1. Deploy ACR once (shared)
2. Build images in CI → push to ACR
3. Promote images across envs
4. Environments pull images (read-only)
5. Clean old images automatically
