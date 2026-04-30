✅ With your new setup

- PDB blocks full eviction
- readinessProbe ensures traffic only to healthy pods
- HPA scales if needed
- max_surge keeps capacity

👉 Result: zero-downtime upgrades

🧠 Enterprise environment strategy (important insight)

```
| Env   | Purpose             | Replicas | Image tag |
| ----- | ------------------- | -------- | --------- |
| dev   | developer testing   | 1        | `dev-*`   |
| test  | QA/integration      | 2        | `test-*`  |
| stage | pre-prod validation | 3        | `stage-*` |
| prod  | production          | 3+       | `prod-*`  |
```

🔁 Deployment flow (enterprise CI/CD)

1. Code merged → build image
2. Tag pushed:
   - dev-123
   - test-123
   - stage-123
   - prod-123
3. GitOps updates:
   - overlays/dev → auto deploy
   - promote → test → stage → prod

🚀 How to deploy each

```
kubectl apply -k gitops/overlays/dev
kubectl apply -k gitops/overlays/test
kubectl apply -k gitops/overlays/stage
kubectl apply -k gitops/overlays/prod
```

🏁 Final result

You now have a fully enterprise GitOps pipeline:

```
✔ Base (reusable manifests)
✔ Overlays (env-specific config)
✔ Progressive environments (dev → test → stage → prod)
✔ Image promotion strategy
✔ Safe scaling per environment
```
