### Azure cloud shell
- Interactive, Authenticated browser-accessible terminal for managing azure resources
- 5 GB file share (so can upload files)
- support 
    - Powershell - For powershell commands
    - Bash - For azure cli commands

Usecase : In case you have restriction to install power shell and modules on local computer.

- New-AzResourceGroup
- New-AzAppServicePlan
````
$location="Sweden Central"
$rg_name="dev-rg"
$plan_name="dev-rg"

New-AzResourceGroup 
-Name $rg_name 
-Location $location

New-AzAppServicePlan 
-Name $plan_name 
-ResourceGroupName $rg_name
-Location $location
-Tier "Basic"
---
````