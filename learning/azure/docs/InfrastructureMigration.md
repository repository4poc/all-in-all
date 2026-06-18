## Overview

## Migration

- Web App on-prem Virtual Machine
  - Azure Virutal Machine (If need access to underlying infrastructure or legacy application with socket )
  - PaaS with Azure Web Apps (Support only speciif languages)
  - Container based AKS

- MS SQL Server on-prem Virtual Machine
  - MS SQL Server Hosted on Azure VM (For legacy version - Operation Overhead)
  - Azure SQL Managed Instance (100% Compatible with latest enterprise edition - provide all feature - bakcup)
  - Azure SQL Database (Not 100% Compatible)

Or Redesign your application to use NoSQL database with Azure Storage.

## Azure WebApp - Deployment Slots

- An independent environment within the web app
- Use for blue-green and Canary deployment
- Helps in quick Rollback via SLots Swapping
- Minimize the downtime.
- RTO : few seconds
- RPO : Zero
- Pre-requisite : Standard Service Plan or Higher

## Azure Dedicated Host

- To have a dedicated physical server on a datacenter
- Quite expensive.
- You have control over the maintenanc events on the physical server

# Need of Containers

- Package once and run anywhere
- Image - package application code with dependencies
