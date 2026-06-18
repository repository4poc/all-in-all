## Overview

It is a Security Monitoring Tool

Sentinel is a service that provide

- SIEM - Security Information Event Management
- SOAR - Security Orchestration Automated Response
  Solutions

Organizations use Microsoft Sentinel to collect, detect, investigate, and respond to security threats across their IT environment.

- It collects data across users, devices, application and infrastructure (Cloud or on-premise)

- It can automate response to security threats

## How to add MS Sentinel to a workspace

- Go to MS Sentinel
  - Add to Workspace : < Choose LAW resource >

## What is the role of Data Connector in MS Sentinel

Data Connector is to connect to variety of sources for data collection.
![alt text](images/{271A66BE-AA60-4666-B08A-A994A42E516D}.png)

**Data Connector Types**

- Windows Security Events
  - Create Data Collection Rule
    - Rule Name:
    - Subscription
      - Resource Group
    - Resouces : < Choose the Windows VM resource >
    - Collect events
      - All Security Events (Default)
      - Common
      - Minimal
      - Custom
        ![alt text](images/{C66E2FE8-2220-4F6A-BF1D-98C104315AC7}.png)
- Azure SQL Database
- Azure EntraID

**Log Analytics Workspace Table**

```
SecurityEvent

```

![alt text](images/{5A218E5C-3FC6-4339-B9C8-C969B84665C4}.png)

## How to create a scheduled Query Rule in MS Sentinel

The main idea behind MS Sentinel is to collect security data from various resources into Log Analytics Workspace, and can find any suspicious activity using KQL

![alt text]({1BFF4C6C-3B66-459C-9FC3-E435CFD742CB}.png)
![alt text]({4BC51911-8E23-403C-9EA5-E9A9D6A9A2FD}.png)
