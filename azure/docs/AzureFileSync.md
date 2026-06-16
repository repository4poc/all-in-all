## Azure File Sync

- It is used with Azure File share, to sync the file from File share to the File Server (on-premise)
- Both Users and Machines can connect to File shares
- Used to sync files on differnet File servers
- File Server(On-Premise) -- File Sync -- Azure File Share
- File sync can
  - either hold all the files inthe file share
  - or using cloud tiering can cache of frequency access files in the file share.
- In case you File Server goes down, you can just replace the File server by insalling Azure File Sync agent

## How to implement Azure File Sync

1. Create a File share in Azure Storage Account - General Purpose type
   ![alt text](images/{8FCC3552-2A98-47E6-AD8A-1F04A2D97930}.png)
   ![alt text](images/{6DF56217-5BD2-4063-B520-89EA89A2FEA5}.png)
2. Create a File Sync Resource
   **Project Details**
   - Subscription
     - Resource Group
   - Storage Sync Service :
   - Region :

   **Network connectivity**
   - Allow access from
     - All Networks (Default)
     - Private Endpoint only

   **Tags**
   - Name/Value

   ![alt text](images/{222A2192-8CFD-4D6B-9EEF-C8BE422912CA}.png)

3. Create a sync group in the Azure sync resource
   ![alt text](images/{64105E97-B8F3-4309-96B2-24B96DCBF7AB}.png)
   ![alt text](images/{29D2E015-AF5F-4EDE-8D82-F99224F20478}.png)
4. Install the Azure File Sync Agent on the Virtual Machine
   ![alt text](images/{79C2B47C-69DA-4261-8A18-17B2F574AD04}.png)
   ![alt text](images/{F2D99013-C4EC-4DB7-8588-ABC71D8CF78A}.png)
   ![alt text](images/{9845B921-4743-4EA8-BC90-C6FBDD7484A7}.png)
   ![alt text](images/{B247C328-E707-4F58-B488-81F748FC4CC1}.png)
5. Verify the server registration in the azure sync resource
   ![alt text](images/{6A1F7E13-FBF2-4865-9572-B4AED3D7DA30}.png)
6. Now the File Server will save the files in File Share as local copy.
7. If you want the files to be stored into a seprate data disk
   - Create and attach new data disk to the Server.
     ![alt text](images/{5FA1C201-1512-4BBA-8CA9-0EA6E57C8C1C}.png)
   - Create Volumn out of the disk
     - First Initialize
     - Create New Volumn
       ![alt text]images/({CD5A6A81-93BB-4925-B0CD-269EE89F6A88}.png)
   - Go to Sync Group > Add Server Endpoint
     ![alt text](images/{BA3DC89D-E80B-4BE4-AC32-E89E3F6C4634}.png)
     ![alt text](images/{3621DA6C-7C10-467A-9D83-C8E977848F5E}.png)

```
Azure File Share (Cloud Endpoint)
           ↕
     Azure File Sync
           ↕
On-Prem Windows Server (Sync Endpoint)
```

| Operation               | On-Prem → Azure | Azure → On-Prem          |
| ----------------------- | --------------- | ------------------------ |
| Add File                | ✅              | ✅                       |
| Modify File             | ✅              | ✅                       |
| Delete File             | ✅              | ✅                       |
| Rename File             | ✅              | ✅                       |
| Create Folder           | ✅              | ✅                       |
| Delete Folder           | ✅              | ✅                       |
| ACL/Permissions Changes | ✅              | ✅ (NTFS ACLs supported) |

**Common Misunderstanding**

**Azure File Sync is not a backup solution.**

If a user deletes 1000 files on-prem:

- Azure File Sync detects the deletion.
- Azure File Share deletes the files.
- Other servers delete the files.

To recover, you need:

- Azure File Share snapshots
- Azure Backup
- Third-party backup solution

**Azure File Sync is bidirectional**

Changes can originate from either:

- On-premises Windows Server (sync endpoint) ➜ Azure File Share ➜ Other servers
- Azure File Share (cloud endpoint) ➜ On-premises server(s)
