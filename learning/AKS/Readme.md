## Source

Udemy Course

A Comprehensive and Practical Course: Containers, K8s, Networking, Security, Scaling, Storage, Monitoring, HA and others

## 47. Start/Stop Feature

![alt text](images/{550A3853-50E1-47D2-9359-923F3E125AE8}.png)

![alt text](images/{65A29C55-2BDC-454D-98BA-3C6105B55555}.png)

![alt text](images/{1B7658BF-3391-45A0-849F-3F88AAAEF105}.png)

On EKS Stop

- The VMSS instances will be deleted
- The Control plan will also be stopped

  ```
    tnc appcluster-k5v5ivih.hcp.swedencentral.azmk8s.io -Port 443
  ```

  The abvoe command will return "Service not found"

  ![alt text](images/{B20D5524-8BBE-490D-8394-5B796D636586}.png)

## 47. About kubeconfig and how to work with multiple AKS clusters

![alt text](images/{D96E5226-5A44-497C-B860-AC73A2C8DF77}.png)

![alt text](images/{72529E69-BE57-486B-BD9D-629D81BE7244}.png)

![alt text](images/{D766EA59-2DB1-43D6-8E23-9443BA163735}.png)

![alt text](images/{F69EC3C4-A994-4C53-BCA8-204489520D11}.png)

![alt text](images/{0DD4EECC-1B05-4CB1-B250-8580A4BC1619}.png)

![alt text](images/{53243DA9-131D-4D57-A7D9-FA58E2602657}.png)

![alt text](images/{79099518-695A-4F54-AD96-A0BE20238EE3}.png)

![alt text](images/{CA487DA9-372B-4FD6-94B7-CE0CB1F045F4}.png)

![alt text](images/{075D7DE9-47DB-4C67-960F-792F543BDC65}.png)

![alt text](images/{99F47665-5A70-4918-8467-333DF06D73C0}.png)

![alt text](images/{89F4F969-0573-4B0D-9B55-C86FFFE9CBB9}.png)

![alt text](images/{D8C9BFFF-A3F6-43B3-A24C-8867678F026F}.png)

### --> If on same server dev and prod cluster is acccessed

![alt text](images/{118E280E-1B3F-4B55-85F7-C7EB6A1F9F7B}.png)

## Azure Marketplace - For Containers

![alt text](images/{EEE5987C-8ECD-4580-BEF8-764DC53E9B6A}.png)

![alt text](images/{BAD84DBA-6E27-42D6-86D5-433F6294BCFA}.png)

On 'add', it will list all the container extensions

![alt text](images/{1E76064C-B397-4E3F-BBF1-1E9D242D671E}.png)

![alt text](images/{2444B54D-A74C-4EA0-AAF2-AF733857A21F}.png)

![alt text](images/{933A21E7-28FC-4E7D-B67B-64EF5B78FF78}.png)

![alt text](images/{FD0356A5-6401-4FBD-B5A9-85EBA6457FD7}.png)

![alt text](images/{3790CBB2-FDD2-44BD-848C-FED5221F6F77}.png)

![alt text](images/{DF101AD5-8BBC-406A-8771-11CA2BD93806}.png)

### --> How to expose an application using Traefik

![alt text](images/{B89C97C3-D7FA-4731-A2C3-295AEF080D69}.png)

![alt text](images/{53679BCC-F218-45C3-9F10-5C2EACEA06F3}.png)

![alt text](images/{BB727B9D-6267-499C-B40C-866BBBDC9E4C}.png)

![alt text](images/{F43D667A-8072-4932-B3B8-6BC7A8726BDF}.png)

### --> How to remove an extension

![alt text](images/{79BD8D7F-14AF-45A4-8323-CBC868FAAB3C}.png)

## Install Kubectl plugins with krew

![alt text](images/{C663EF79-F376-4AEA-8760-F70C46FBD4DD}.png)

- kubectl krew -h

## 124. Stroage Options in AKS

![alt text](images/{54CCE186-C119-470C-A7C0-26601D626B3C}.png)

![alt text](images/{42CC92E0-2C10-407F-B297-6A3CBB0767B5}.png)

![alt text](images/{A21B511A-2AA1-4918-805F-D20977E886AA}.png)

## 125. Dynamically create azure disk PVs by built-in storage classes

![alt text](images/{E2098CB7-ED19-4ACC-A4C0-655F6F0DEC0D}.png)

![alt text](images/{039BCA85-DA85-4B4B-966A-02E294C75D41}.png)

```
k get storageclasses
```

![alt text](images/{F8A67896-6F12-4F34-B54F-CE098ACC06EB}.png)

```
waitforfirstconsumer means it will create the disk after a pod requires to mounth that one or more exactly after a VM Node needs to attach
```

```
k get storageclasses managed-csi -o yaml
```

![alt text](images/{A7C4D5EB-361B-40DA-9640-6D49851D5C47}.png)

![alt text](images/{6C6A6EE1-8E7A-4783-AC90-343ACCA4B388}.png)

![alt text](images/{F4DCE2AA-E96F-4DD0-BB3A-EF078C4A0D70}.png)

A pvc is creates in the infrastructure resource group

![alt text](images/{E3C3FBA0-11D2-40A7-812F-C319418ACAAE}.png)

![alt text](images/{E29E1754-B063-4648-8C8B-5437C494969D}.png)

To check the Pod's Node

![alt text](images/{4D746354-D27D-4327-9F1D-D7AC17032D5A}.png)

Create a test file in the disk

![alt text](images/{85E1DB73-7109-49E3-A958-E6F9ABE09CF4}.png)
