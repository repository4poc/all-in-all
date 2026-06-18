## Overview

Azure Traffic Manager is DNS based - Global Load Balancer

Azure Frontdoor is Proxy based - Global Load Balancer

By a Global Loadbalancer : It means while you create the resource, you need not to mention any location.

| Feature               | Azure Traffic Manager      | Azure Front Door     |
| --------------------- | -------------------------- | -------------------- |
| OSI Layer             | DNS (Layer 3/4 concept)    | HTTP/HTTPS (Layer 7) |
| Traffic Routing       | DNS-based                  | Proxy-based          |
| Supports TCP/UDP      | ✅ Yes                     | ❌ No                |
| Supports HTTP/HTTPS   | ✅ Yes                     | ✅ Yes               |
| Global Load Balancing | ✅ Yes                     | ✅ Yes               |
| Failover Speed        | Slower (DNS TTL dependent) | Faster (real-time)   |
| WAF                   | ❌ No                      | ✅ Yes               |
| SSL Termination       | ❌ No                      | ✅ Yes               |
| URL-based Routing     | ❌ No                      | ✅ Yes               |
| Caching/Acceleration  | ❌ No                      | ✅ Yes               |
| Best For              | Any protocol               | Web applications     |
