variable "state_resource_group_name" {
  description = "Resource group containing Terraform state storage"
  type        = string
  default     = "rg-terraform-state"
}

variable "storage_account_name" {
  description = "Globally unique Azure Storage Account name"
  type        = string
  default     = "terraform-state"
}

variable "location" {
  description = "Azure region"
  type        = string
  default     = "swedencentral"
}
