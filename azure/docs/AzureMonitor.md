## Azure Monitor

A complete monitoring solution where you can collect, analyse and respond to the data collected

- Pre-existing, we don't create any resource
- You can monitor both cloud and on-premise resources

## Features

- Metrics : Numeric values collected over time
  - Scope : < Choose Resource >
  - Metric : Depends on resource - Aggregation : Average

    ![alt text](images/{AAB3E1D5-D889-49DA-99B0-FE9EBCF6B752}.png)

- Activity Log
  - Scope : < Choose Subscription >
    ![alt text](images/{F3ED9918-72EB-4DA6-954D-74A40CC2F8F6}.png)

- Logs
  - Pre-requisite : Need Log Analytics Workspace
- Alerts

## Setting Up Alert

### Create Alert Rule

- Scope : < Choose Resource >
- Condition
  - Signal : < List of Metric depends on Resource >
    ![alt text](images/{6BC44B74-AC39-4CA0-84F5-03153C9538F8}.png)
    ![alt text](images/{ADE635A8-2A02-43FF-8657-607BBAF5C154}.png)
- Action
  - Action Group
    ![alt text](images/{9E709CE2-CB91-4409-A78D-82E17990425D}.png)
    ![alt text](images/{B62E7A77-7198-4E55-AFF4-B348DB07A97F}.png)
    ![alt text](images/{2E2B35C0-A525-4FDA-B442-AEEEA4110988}.png)
    ![alt text](images/{F0629F76-893A-4E2C-AF50-CD2BD8659A35}.png)
- Details
  ![alt text](images/{72E32F54-99E8-4FD7-B822-E24B94C59617}.png)

## Alert Logic - Dynamic Threshold

While creating a alert rule - condition, we need to choose threshold type

- Static : We decides, what should be the threshold value
  ![alt text](images/{9B946376-EF88-4995-A921-EA571F7ACB95}.png)
- Dynamic : Azure decides based on the past data, what should be the threshold value
  ![alt text](images/{3D1010D2-D4C2-45C3-AFBC-B7381EF7F6AC}.png)

## Log Analytics Workspace

- Used for collect logs in one place
- Need to create resource (Doesn not pre-exists)
- Metix/Alerts can just tell us something went wrong, like CPU utalization is high, but to debug we need logs
- You use KQL (Kusto Query Language to Create Alerts/Dashboards)

- Web Apps can send logs to LAW via Diagnostic Logs
  - Steps
    - Choose Log Category
      - HTTP Logs
      - App Service Console Logs
      - App Service Application Logs
      - Access Audit Logs
      - App Servie Platform Logs
      - App Service Authentication Logs
    - Choose Destination
      - Log Analytics Workspace
      - Archieve to Storage Account
      - Stream to Event Hub
      - Send to Partner Solution

### Create Log Analytics Workspace

- Subscription
- Resource Group
- Name
- Location
  ![alt text](images/{AF86DCBC-D0CE-4052-9934-DA668012A891}.png)

## Application Insight

A tool inside Azure Monitor

- It is a performance monitoring tool, use to monitor
  - requests
  - dependencies
  - excpetions
  - traces
  - UI experience
- It supports Opentelemetry to collect telemetry data.
- Helps in Observability and Peformance Monitoring

Investigates

- Application Dashboard : At a-glance assessment of application health and performance
- Live Metrics : A real-time analytics dashboard for insights into application activity and performance
- Search View
- Availability View
- Failures View
- Performance View
- Agent Details

- Monitoring
  - Alerts
  - Metrics
  - Logs
  - Workbooks : Interactive reports and dashboards
  - Dashboard with Grafana
    ![alt text](images/{E0A6485D-70D3-43A8-92D8-16BB3F6ACBAE}.png)
    ![alt text](images/{9CB5648A-67B4-4E99-809B-5F9C7186803E}.png)

## How to enable Application Insight for Azure Web App

### Steps

- Choose an Azure Web App
  - Monitoring
    - Application Insights
      - Turn on Application Insights - Name : < Application Insight Resource Name >
      - Log Analytics Workspace Name < Choose/Create Log Analytics Workspace >

Application Insights collect data about your application, and need some place to store that data, so LAW is used for storing data.
![alt text](images/{FF645E33-9303-489C-A165-EB327679F651}.png)
![alt text](images/{15D17472-3B48-4593-905D-714A4A07A0E1}.png)
![alt text](images/{3C403BDF-A063-4E99-A68B-F25B8DDA9A14}.png)

![alt text](images/{E0A6485D-70D3-43A8-92D8-16BB3F6ACBAE}.png)

```
If you're looking for Insights under Monitoring for an Azure SQL Database/Managed Instance and can't find it, that's likely because SQL Insights was retired on 31 December 2024 and has been removed from the Azure portal. Microsoft now recommends Database Watcher for advanced SQL monitoring.

What you should see instead

For an Azure SQL Database, the common monitoring options are:

Monitoring → Metrics
Monitoring → Alerts
Monitoring → Logs (if Log Analytics is configured)
Intelligent Performance → Query Performance Insight
Intelligent Performance → Performance Recommendations
```

```
In case you enabled Application Insight on Web App, after choosign the LAW, you need to need to tell what all to collect

**Instrument you application**

Choose Runtime : Java/.Net/Python

- Collection level
- Profiler and Cost Optimization
- Snapshot debugger
- SQL Commands
```

## Application insight - Live Metrix View

![alt text](images/{FD1F1C17-4D2C-4862-9EA4-ED7468E6DC74}.png)

```
Enable live metrics with Azure Monitor OpenTelemetry by following language-specific guidelines:

ASP.NET: Not supported.
ASP.NET Core: Enabled by default.
Java: Enabled by default.
Node.js: Enabled by default.
Python: Enabled by default.`
```

## Application insight - Performance View

- Request Count
  - URL : /api/orders
  - Average Duration : x seconds
  - Count : 100
- CPU
- Storage
- Available Memory

Here if you click the request details, it gives you also which resourc it is hitting like SQL and what query it is passing (if 'SQL Commands' is enabled )

![alt text](images/{9C319416-A9CD-4487-9431-0F5106CF7D5D}.png)

## Application insight - Application Map

Highlevel view to show, how application components are integrated like webapp, database, storage etc.

- who calling which component
- Calls made with avg. call time

## Application insight - Availability

Useful to execute schedules availability tests for multiple endpoints with average response time.
![alt text](images/{139139C9-008C-4E3D-AB87-29875A6B42C3}.png)

Test Types

- Classic Test
  ![alt text](images/{3934F1F8-E442-4528-BDD5-DD09F7A34C4B}.png)
- Standard Test
  ![alt text](images/{C72F9500-48BB-4F44-9F55-463BACFF828B}.png)
  ![alt text](images/{962A393D-9D34-4EC0-AAA2-50D1AD4C5E12}.png)

  These alerts are to any action group or email.

## Application insight - Other Features

- Users : Track and Analyse user interaction with your application
- Session : Session Trends
- Funnels : How users progress through a series of steps in your application
- User Flow : To identify most common routes and area where users are most engaged and encoutered issues
- Cohorts : Group Users or Events by common characteristics to analyse behaviour pattern, feature usage and impact of changes over time
- Active Users
- Authenticated User Timeline
- User Retention Analysis
- User Impact Analysis
- Usage Calander
  ![alt text](images/{1BE99848-3995-4E41-B520-AA6171B0A4C9}.png)
  ![alt text](images/{E47E002B-06E9-4580-8145-E48739BA774F}.png)

##

| Resource Type               | Application Insights Support           |                        |
| --------------------------- | -------------------------------------- | ---------------------- |
| Azure App Service           | Native integration                     |                        |
| Azure Functions             | Native integration                     |                        |
| Azure Kubernetes Service    | Via OpenTelemetry / Container Insights |                        |
| Azure Container Apps        | Supported                              |                        |
| Azure Spring Apps           | Supported                              |                        |
| Azure Service Fabric        | Supported                              |                        |
| Java Applications           | Supported                              |                        |
| .NET Applications           | Supported                              |                        |
| Node.js Applications        | Supported                              |                        |
| Python Applications         | Supported                              |                        |
| Applications running on VMs | Supported via SDK/OpenTelemetry        |                        |
| On-premises applications    | Supported via SDK/OpenTelemetry        |                        |
| Applications in AWS/GCP     | Supported via SDK/OpenTelemetry        | ([Microsoft Learn][1]) |

[1]: https://learn.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview?utm_source=chatgpt.com "Application Insights OpenTelemetry observability overview"
