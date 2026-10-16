terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "6.67.0"
    }
  }
}

provider "aws" {
  region = var.regions[0]
}

provider "aws" {
  alias  = "west"
  region = var.regions[1]
}

