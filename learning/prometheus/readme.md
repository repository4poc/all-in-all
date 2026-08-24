## Metrix

- Metrics are based on numeric information
- that measurements based on data points what is happening about a specific aspect
  - Aspects like
    - Node CPU
    - Node Heart Beat
    - Node Memory
    - Node Disc Utalization
    - Request Processing time
    - Request Count

## Monitoring

- It is the process of keeping an eye on these metrics over time to understand is everything normal or there is any change happened.
- It helps identifying abnormality and problems.

**Conceptually**

Monitoring = Metrics + Dashboards + Alerts

## Prometheus

- An opensource tool/solution for monitoring
- Offer features like
  - Metrices
    - Node CPU
    - Pod Status
      - How many time a pod went into Crashloopbackoff throughout a day.
      - At what time Pod went through Crashlookbackoff state
    - Deployent status
      - Replica status throuhout the day.
    - Application specific metrics
      - No. of http request received in last 5 min.
      - Total number of req. received in a day
      - Users signed up count
  - Dashboard
  - Alerts

All the metrics you want to collect depends on the company and project, you can collect 100s of matrics

The main purpose is to monitor the health of your application and system.

These metrics are either

- Fed to the monitoring system (Push Mechanism) or
- monitoring system automatically pull these metrics (Pull/Scraping mechanism).

The monitoring solution provide use the capability to fire an alert on a specific threshold breach for a specfic metrics called **Alert rules**.

**Alert rule specify**

- when to fire the alert
- where to fire the alert

```
Metrics Source
|
V
Prometheus
|
V
Alert Manager
|
V
Alert
```

## Prometheus Architecture

![alt text](images/{BB018C0E-EE65-4FE1-873B-9230491F0B15}.png)

Key Components

- Prometheus Server
  - Retrival : Pull the info from exporters. so all the metrics data collected is stored in the TSDB
  - TSDB : Store the metrics information into a Time Serier Database
    - Timestamp -> Key / Value
  - HTTP Server : To get the information from Prometheus using PromQL
- Service Discovery : help specifying targs to fetch metrics from custom applications deployed onto kubernetes
- Alertmanager :
  - responsible for managing alerts
  - It takes care of deduplicating, grouping, and routing alerts to the appropriate notification channels such as PagerDuty, email, or Slack.
- Exporters :
  - Exporters are small applications/add-ons/plugins that collect metrics from various third-party systems and expose them in a format Prometheus can scrape.
  - They are essential for monitoring systems that do not natively support Prometheus.
  - Types of Exporters:
    - Node Exporter
      - For Infrastrcture Monitoring
      - Runs on your kubernetes cluser as a pod and collect information from all your kubernetes nodes. Information like
        - CPU Utalization
        - Memory Utalization
        - Disk Utalization
    - Kube-state-metrics Exporter
      - For Kubernetes Objects Monitoring
      - Runs on your kubernetes cluser as a pod and collect information regarding kubernetes objects like Pod events, Config Maps, Secrets via API Server
    - Custom metrics
      - For Application Monitoring
      - Developed by the developer as /metrics endpoint collecting and returning information like
        - Number of HTTP requests in last 5 minutes
        - Average Request processing time
        -
      - For application level metrics
    - MySQL Exporter (for MySQL database metrics)
    - various other application-specific exporters.

![alt text](images/{6BC52B60-041E-42BD-BFB0-DA88CDAE83ED}.png)

![alt text](images/{3445D37B-8907-4F78-927E-3F7A5F4F5E50}.png)

As clusterIP services can only be accessible from inside the Kubernetes, not from outside like your local browser, so you can login to the kubernetes cluster node and then access it from there.

![alt text](images/{E25863A3-A09E-4D30-B011-3348C3FE0066}.png)

![alt text](images/{4BA18E81-15EE-4B91-9210-B8D7EA768C10}.png)

Below is the list of information your node exporter collecting periodically.

The information is in the format, prometheus understand. Thats the purpose of an exporter.

![alt text](images/{8CC31B04-99EB-441A-B72D-E6342CB44757}.png)

**For kube-state-exporter**

![alt text](images/{F031D77C-43BE-4860-A2C5-FDE23E9570D0}.png)

From within the kubernetes node.
![alt text](images/{CB8C2BAA-D0AC-4E8D-9FA0-A00FAA67BBE9}.png)

![alt text](images/{6AE6C5E7-EA44-460A-B8E5-DECF4ADF46CD}.png)

## How to create a pod the crash for testing pod crash scenarios

![alt text](images/{210B11B0-FDAF-4698-912D-7A4903C54419}.png)

![alt text](images/{EF1CA9E1-592F-4C87-B6C6-8E9AD84F9117}.png)

**PromQL Example**

[For specific namespace]
![alt text](images/{4B441C28-DD0D-499A-8AB0-899DC4F11747}.png)

[For all namespaces]
![alt text](images/{295A7A9E-A60F-4BA6-951E-1B906F6B6302}.png)

[For PromQL : refer the /metrics]
![alt text](images/{2AA9C512-ADDE-43FD-8E1C-66C8FECF4DC0}.png)

As if you see the prometheus visualization is not that good, so we prefer to go with Grafana for visualization

**Prometheus Autocomplete Feature**

![alt text](images/{8532C2FF-BC51-4E34-B7BE-18E07509B1EC}.png)

![alt text](images/{AD50630F-714A-4268-809C-665AAA1A85F0}.png)

## Common Matrics

- How many times the pod has crashed over 5 min
- How many times config maps are created over a period of time
- How many secrets are there in your kubernets cluster
- CPU / Memory / Disk utalization
- HTTP request, Users Created ,

## Grafana

- Grafana is a powerful dashboard and visualization tool that integrates with Prometheus to provide rich, customizable visualizations of the metrics data.
- Both Prometheus and Grafana used together as a monitoring stack.
- Grafana supports data sources like
  - prometheus
  - graphite
  - nagios
  - influxDB

![alt text](images/{33AB8BFC-ECE5-4FFD-B0BD-468AA6204661}.png)

So you can chenage the data source keepting the visualiation intact

- Grafana is a not a monitoring tool, it is a visualization tool.

- Grafana also support authentication and authorization. where prometheus does not has any authentication and authorization. You can integrate it with IAM, EntraID or SSO

- So you can have different dashboard for
  - Developers
  - QA
  - Managers
  - DevOps

- Grafana provide pre-defined graphs, which is not in the case of prometheus, you have to write PromSQL for the graph.

  ![alt text](images/{D07D4AF0-CEE1-4050-866A-F386B3C0FA76}.png)

- Grafana uses the same PromQL for custom graphs
  ![alt text](images/{2CB834D0-6A32-4966-BB70-7A46926D801D}.png)

## Install kube-prometheus-stack

```
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts

helm repo update

[Deploy the chart into new namespace]
kubectl create ns monitoring

helm install monitoring prometheus-community/kube-prometheus-stack \
-n monitoring \
-f ./values.yml

kubectl get all -n monitoring

[Prometheus UI]
kubectl port-forward service/prometheus-operated -n monitoring 9090:9090


[Grafana UI]
kubectl port-forward service/monitoring-grafana -n monitoring 8080:80

[Grafana Username]
kubectl get secret --namespace monitoring monitoring-grafana -o jsonpath='{.data.admin-user}' | base64 -d

[Grafana Password]
kubectl get secret --namespace monitoring monitoring-grafana -o jsonpath='{.data.admin-password}' | base64 -d

[Alertmanager UI]
kubectl port-forward service/alertmanager-operated -n monitoring 9093:9093
```

**values.yml**

![alt text](images/{75E9CCCD-F719-4590-8E49-98EFFADF40F8}.png)

## Uninstall helm chart

```
helm uninstall monitoring --namespace monitoring

kubectl delete ns monitoring
```

Note: Instead of port-forwarding, use ingress controller as best practice

## How to connect grafana to prometheus

1. Add Prometheus Connection
   ![alt text](images/{BF05BF1C-98B2-49CC-8BCE-8A442500503A}.png)

## Competetors to Prometheus

- Graphite
- InfluxDB
- Nagios

## Basic Examples of PromQL

1. Return all time series with the metric container_cpu_usage_seconds_total

   ```
   container_cpu_usage_seconds_total
   ```

2. Return all time series with the metric container_cpu_usage_seconds_total and the given namespace and pod labels.

   ```
   container_cpu_usage_seconds_total{namespace="kube-system",pod=~"kube-proxy.*"}
   ```

3. Return a whole range of time (in this case 5 minutes up to the query time) for the same vector, making it a range vector.

   ```
   container_cpu_usage_seconds_total{namespace="kube-system",pod=~"kube-proxy.*"}[5m]
   ```

## Aggregation & Functions in PromQL

1. Total increase in container restarts over the last hour.

   ```
   increase(kube_pod_container_status_restarts_total[1h])
   ```

2. Calculates the rate of CPU usage over 5 minutes

   ```
   rate(container_cpu_usage_seconds_total[5m])
   ```

3. aggregates the CPU usage across all nodes

   ```
   sum(rate(node_cpu_seconds_total[5m]))
   ```

4. Average memory usage grouped by namespace.

   ```
   avg(container_memory_usage_bytes) by (namespace)
   ```
