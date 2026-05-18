# C# vs Dotnet vs ASP Dotnet Core

[Go Back Home](../README.md)

- .NET is a development platform ecosystem
  - It includes:
    - Runtime (CLR)
    - SDK and CLI (dotnet)
    - Base libraries
    - Web frameworks
    - App frameworks
    - Package management
    - Tooling
- C# Programming Language
- Inside .NET, there are actual frameworks such as:
  - ASP.NET Core → web APIs/web apps
  - Entity Framework Core → ORM/database access
  - Blazor → frontend web UI
  - MAUI → mobile/desktop apps

#### You can run application without a web server, but best practices is to deploy you application behind a web server like Nginx(Linux) or IIS (Windows)

```
cd webapp
1. dotnet publish
2. dotnet publish/webapp.dll -urls http://0.0.0.0:5000
```
