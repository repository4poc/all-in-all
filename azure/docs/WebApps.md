## Type

PaaS

We don't have access to underlying infrastructure.

No need to patch and upgrade server

## For Application Types

- Web API
- Web Apps
- Batch

## Support Runtime

- NodeJS
-

## Configuration

- Subscription : subscription Id
- Resource Group : Resource group name
- Region : Sweden Central
- Name : DNS name
  - < unique name > .azurewebsites.net
- Publish
  - Code
    - Runtime stack : NodeJS 10/ Java 25 / PHP 8.5 / Python 3.14
  - Container
- Operating System :
  - Linux
    - Cheap as no Licensing cost
  - Windows
- Pricing Plans
  - Name : Name of Pricing Plan
  - Plan :
    - Hardware View : vCPU/Memory (RAM)/Remote Storage/Scale (Instance)
    - Feature View : Custom Domain/Auto Scale/Daily Backup/Staging Slots/Zone Redundancy/vNet Integration/
      - Free F1 - No feature
      - Basic B1 - only custom domain feature and vNet Integration
      - Standard (Legacy) - No Zone Redundancy feature
      - Premium (V2/V3) - All features

| Environment  | Recommended Plan           | Why                                           |
| ------------ | -------------------------- | --------------------------------------------- |
| Dev          | Basic B1                   | Cheap, enough for developers                  |
| QA/Test      | Standard S1/S2             | Supports staging slots + autoscale testing    |
| Pre-Prod/UAT | Premium V3 (or Premium V2) | Mirrors production behavior                   |
| Prod         | Premium V3 (or Premium V2) | High availability, autoscale, networking, SLA |

- Zone Redundancy : For Premium Plans only
  - Enabled : Your App Service Plan and Apps will be zone redundant, Minimum App Service plan instance count will be 2
  - Disabled
