| AWS              | Azure equivalent                           | Notes                                                                                     |
| ---------------- | ------------------------------------------ | ----------------------------------------------------------------------------------------- |
| AWS CodeCommit   | Azure Repos                                | Correct                                                                                   |
| AWS CodeBuild    | Azure Pipelines                            | Correct for CI/build                                                                      |
| AWS CodeDeploy   | Azure Pipelines / Releases / Environments  | “Azure Release” is classic; modern Azure DevOps uses multi-stage YAML pipelines           |
| AWS CodePipeline | Azure Pipelines                            | Correct for end-to-end CI/CD orchestration                                                |
| AWS KMS          | Azure Key Vault / Managed HSM              | Key Vault is usually right; Managed HSM for stricter HSM needs                            |
| AWS IAM Role     | Azure Managed Identity / Service Principal | Managed Identity is best for Azure resources; Service Principal for app/workload identity |
| AWS IAM User     | Microsoft Entra ID User                    | Correct                                                                                   |
| AWS IAM Policy   | Azure RBAC Role Assignment / Custom Role   | Not “Entra ID Role” usually; Azure RBAC is closer for resource permissions                |
| AWS EventBridge  | Azure Event Grid                           | Direct equivalent for event routing and event-driven architectures                        |
