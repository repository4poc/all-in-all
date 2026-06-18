## Overview

In most new Azure architectures, Private Endpoints are preferred over Service Endpoints.

| Feature                                     | Service Endpoint | Private Endpoint                  |
| ------------------------------------------- | ---------------- | --------------------------------- |
| Uses Private IP?                            | ❌ No            | ✅ Yes                            |
| Resource accessible over Internet endpoint? | ✅ Yes           | ❌ No (can disable public access) |
| Traffic stays on Azure backbone?            | ✅ Yes           | ✅ Yes                            |
| Requires DNS changes?                       | ❌ No            | ✅ Usually                        |
| Most secure option?                         | ❌               | ✅                                |
| Cost                                        | Free             | Additional cost                   |
| Recommended for new deployments?            | Sometimes        | Usually                           |
