[Go Back Home](../README.md)

# Cloud Best Practices

- Delete Unnecessary Resources
- Set Budget Alerts

## Cost Saving

- Choose right region, yet closer to your application users.

## How to policy to enfore tags

Option 3: Modify policy (Enterprise Best Practice)

```
If tag missing
    ↓
Azure Policy adds tag automatically
    ↓
Resource created successfully
```

**Recommendation**

For most enterprises:

- Define mandatory tags at the Management Group level.
- Use Modify to inherit tags from Resource Groups.
- Use Deny only for truly mandatory tags that cannot be auto-populated.
- Monitor compliance through Azure Policy compliance dashboards.
