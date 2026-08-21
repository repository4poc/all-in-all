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
  - Retrival : Pull the info from exporters.
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
      - For VMs metrics via system files - CPU, Memory, RAM
    - Kube-state-metrics Exporter
      - For Kubernetes Monitoring
      - Via Kubernets API Server - Pod events, Config Maps, Secrets)
    - Custom developed /metrics API endpoing
      - For Application Monitoring
      - For application level metrics
    - MySQL Exporter (for database metrics)
    - various other application-specific exporters.

![alt text](images/{6BC52B60-041E-42BD-BFB0-DA88CDAE83ED}.png)

## Grafana

- Grafana is a powerful dashboard and visualization tool that integrates with Prometheus to provide rich, customizable visualizations of the metrics data.
- Both Prometheus and Grafana used together as a monitoring stack.

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
