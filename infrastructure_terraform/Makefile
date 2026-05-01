dev:
terraform init -backend-config=backend/dev.hcl
terraform apply -var-file=envs/dev.tfvars
staging:
	cd infrastructure/environments/staging && terraform apply

prod:
	cd infrastructure/environments/prod && terraform apply