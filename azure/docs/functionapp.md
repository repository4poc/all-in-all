### Type

PaaS

- Serverless Service , You are charged based on the running time of the function app.

- Azure App Service is not Serverless, as it has App Service Plan (Free/Basic/Standard/Premium/Isolated)

### Configuration

- Hosting Option : Flex Consumption

| Feature               | Flex Consumption  | Functions Premium | App Service   | Container Apps    | Consumption  |
| --------------------- | ----------------- | ----------------- | ------------- | ----------------- | ------------ |
| Scale to zero         | ✅                | ❌                | ❌            | ✅                | ✅           |
| Scaling               | Fast event-driven | Event-driven      | Metrics-based | KEDA event-driven | Event-driven |
| VNet support          | ✅                | ✅                | ✅            | ✅                | ❌           |
| Cold start prevention | Optional          | Yes               | Yes           | Optional          | ❌           |
| Max instances         | 1000              | 100               | 30            | 300               | 200          |

- Subscription
- Resource Group
- Function Name : < unique-name >.azurewebsites.net
- Region : Sweden Central
- Runtime Stack : .net
  - .Net
  - Java
  - NodeJS
  - Python
  - Powershell
  - custom handler
- version : 10 (LTS)
- Instance Size: The amount of memory allocated to each instance of the function app
  - 512 MB
  - 2048 MB
  - 4096 MB
- Zone Redundancy
  - Enabled
- Storage Account: Select a storage account or create a new one. Accounts must support blobs, queue, and Table storage
- Access
  - Public Access : Public access is applied to both main site and advanced tool site. Deny public network access will block all incoming traffic except that comes from private endpoints.
    - Enabled (Default)
    - Disabled
  - vNet Integration
    - Enabled
      - Virtual Network : Select or create a virtual network that is in the same region as your new app.
    - Disabled (Default)
- Application Insight: Azure Monitor application insights is an Application Performance Management (APM) service for developers and DevOps professionals. Enable it below to automatically monitor your application. It will detect performance anomalies, and includes powerful analytics tools to help you diagnose issues and to understand what users actually do with your app. Your bill is based on amount of data used by Application Insights and your data retention settings.
  - Disabled
  - Enabled
    - Application Insight : < Select or Create Application Insight Resource>
- Authentication: For best security practice, use managed identity authentication when available (some resources may only use secrets).
 ![alt text](images/{AF000BAB-8E9E-4E1B-98A5-C2734150F387}.png)
- Tags

