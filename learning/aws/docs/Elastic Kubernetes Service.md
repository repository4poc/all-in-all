## Kubernetes

A container orchestration platform

## Why we need Kubernetes

- Limited Resource (Fix : Auto-Scaling)
  - Run on single host, so container no. 100 is dying due to lack of resources by host kernel
- No Auto Healing (Fix : Auto-healing)
  - Someone Killed the container Need Manual intervesion to restart it
  - In case of any issue like image not found,as container are ephemeral (short lived) in nature, it will immediately die
- So there are 100 of reasons the containers can be down

DevOps Engineer can not manually monitor 100s of containers, So kubernetes auto-healing feature helps us in this problem.
