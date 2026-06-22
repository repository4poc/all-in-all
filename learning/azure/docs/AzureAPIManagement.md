## API Management

- In case you have multiple APIs
  - Customer API (Port:8000)
  - User API (Port:9000)

  ![alt text](images/{1F2A003B-C5E0-43A1-AC00-CC45007AD9C0}.png)

## Azure API Management vs Azure Application Gateway

| Feature                        | Azure API Management                           | Azure Application Gateway                      |
| ------------------------------ | ---------------------------------------------- | ---------------------------------------------- |
| Primary Purpose                | API lifecycle management                       | Layer 7 load balancing and web traffic routing |
| Target Audience                | API consumers and developers                   | Web applications and infrastructure teams      |
| Protocols                      | REST, SOAP, GraphQL, WebSocket                 | HTTP, HTTPS, WebSocket                         |
| API Security                   | OAuth, JWT validation, API keys, rate limiting | WAF, SSL termination, URL filtering            |
| Developer Portal               | Yes                                            | No                                             |
| API Versioning                 | Yes                                            | No                                             |
| API Transformation             | Yes (rewrite requests/responses)               | Limited URL/header rewrite                     |
| Analytics                      | Detailed API usage analytics                   | Traffic metrics and diagnostics                |
| Load Balancing                 | No (not primary purpose)                       | Yes                                            |
| Web Application Firewall (WAF) | No                                             | Yes                                            |
| Backend Routing                | Basic                                          | Advanced path-based routing                    |
| Monetization/Subscriptions     | Yes                                            | No                                             |

Common Enterprise Architecture: Use Both

```
Internet
    ↓
Application Gateway (WAF)
    ↓
API Management
    ↓
Backend APIs / AKS / App Services
```

**Application Gateway**

- Blocks malicious traffic
- Provides WAF protection
- Handles SSL termination
- Routes traffic

**API Management**

- Authenticates API consumers
- Applies throttling and quotas
- Validates JWTs
- Handles API versioning
- Provides developer portal and analytics

| Service                   | Network Component | VNet Integration | Primary Purpose              |
| ------------------------- | ----------------- | ---------------- | ---------------------------- |
| Azure Load Balancer       | Yes               | Native           | Layer 4 load balancing       |
| Azure Application Gateway | Yes               | Native           | Layer 7 load balancing + WAF |
| Azure API Management      | No                | Supported        | API gateway and management   |

**Key Difference from Application Gateway**

**Application Gateway**

- Lives inside your VNet.
- Requires a dedicated subnet.
- Processes traffic directly as a network appliance.

**API Management**

- Is a PaaS service.
- Can be connected to a VNet.
- Focuses on API policies, authentication, throttling, versioning, and developer experience rather than traffic distribution.

## When to Use Azure API Management

Use Azure API Management when:

- Exposing APIs to internal, partner, or external developers
- Enforcing API policies
- Managing API versions
- Applying rate limits and quotas
- Validating JWT tokens
- Transforming requests and responses
- Providing a developer portal

## Flow

UI(FrontEnd App) --> API Management --> APIs

Features

- Authenticate requests before they reach to APIs
- Rate Limit of requests
- Publish set of APIs as a Products to customers
  - Customer A can consume Product A (Customer API and Order API)
  - Custeomr B can consume Product B (Order API and Pricing API)

## Create Azure API Management

**Project Details**

- Subscription
  - Resource Group

**Instace Details**

- Region:
- Resource Name:
- Organization Name:
- Administrator Email:

**Pricing Tier**

- Pricing Tier:
  - Developer (No SLA)
  - Basic (99.95% SLA)
  - Basic v2 (99.95% SLA)
  - Standard (99.95% SLA)
  - Standard v2 (99.95% SLA)
  - Premium (99.95% or 99.99% SLA)
  - Premium (99.95%)
  - Consumption (99.95% SLA)

    ![alt text](images/{9C3015AC-F362-4C3D-83BC-D683D5CF8220}.png)

Azure API Management Developer tier, the good news is that Microsoft currently lists the Developer tier deployment as free of charge.

**Monitor + Secure**

- Log Analytics: Disabled (Default)
- Defender for APIs in MS Defender for Cloud : Disabled (Default)
- Application Insight : Disabled (Default)

**Networking**

- Private Link : Disabled (Default)
- vNet Integration : Disabled (Default)
- VNet Injection : Disabled (Defulat) ## Premium Tier Feature

**Authentication**

- System Assigned Managed Identity : Disabled (Default)

**Tags**

- Name : Value

```
Mobile App
    ↓
Azure API Management
    ↓
Microservices
```

## Defind APIs in Azure API Management

![alt text](images/{920B455C-B3DD-43A5-ACBE-448CC82BEA15}.png)

1. **Add API**

![alt text](images/{3AC00534-5BF8-48F7-B61F-DB3E22775E45}.png)

- APIs > APIs
  - Add API
    - gRPC API
    - Socket API
    - HTTP API (Choose)
      - Disply Name : **Order API**
      - Name : order.api
      - Web Service URL : https://< myapi >.azurewebsites.net
      - API Service Suffix : orders (Base URL : < api-management-url>/orders )
      - Products :
        ![alt text](images/{016DE9AF-7BFE-4490-ACA2-1613E90310B0}.png)

2. **Within Added API (Order API), add Operations**
   ![alt text](images/{EF17FBA5-C365-41AE-AFA5-B046448B497E}.png)
   - Choose (**Order API**)
     - Add Operation
       - Disply Name : Get Orders
       - Name: get.orders
       - URL : GET | /api/orders
       - Description
         ![alt text](images/{66E02C1C-D7B3-407C-8715-AE38EDEF831B}.png)
     - Add Operation
       - Disply Name : Get Order By Id
       - Name: get.orders.id
       - URL : GET | /api/orders/{id}
       - Description
     - Add Operation
       - Disply Name : Add Orders
       - Name: add.orders
       - URL : POST | /api/orders
       - Description
       - Request
         - Add Representation : application/json
         - Sample: JSON example
           ![alt text](images/{619DEB31-AF23-48D7-B4DF-99E4B815B111}.png)

## Allow Access Only via API Managerment

To access the Backend APIs via ApI managment, you need subscription key, that need to be passed as (Ocp-Apim-Subscription-Key) while making the call.

APIs > Subscription

- Primary Key
- Secondary Key

```
GET < API Management URL >/api/Orders
Key Name : Ocp-Apim-Subscription-Key
Key Value : < key value>
```

Restrict API access via Web App public URL,

1. Go to the Web App
2. Go to Networking
   - Public Network Access
     - Enabling access from all networks
     - Enabling access from selected vNets and Ips (**Enabled**)
     - None

   **Site Access and Rules**
   - Add Rule
     - Name : Allow APIM
     - Action : Allow
       - Allow
       - Deny
     - Priority
     - Description
     - Type : IPV4
     - Value : < **Public IP of APIM** >

## API Management Policy - IP Restriction

- Choose (**Order API**)

Policy can be applied at **API** level as well as **Operation** level

- Policy XML
  - Frontend Policy
  - InBound Policy
    - Add Caller-Filter Policy (Use Snippet)
      - Action : Allowed /Forbid
      - IP Range : _._._._ TO _._._._
  - Backend Policy
  - Output Policy
  - Onerror Policy

## API Management Policy - Rewrite URL

- Choose (**Order API**)

- Choose (**Order Operation**)

- Policy XML
  - Frontend Policy
  - InBound Policy
    - Add Custom Code here for pre-processing for re-write
  - Backend Policy
  - Output Policy
  - Onerror Policy

## API Management Policy - Rate-limit

- Choose (**Order API**)

- Policy XML
  - Frontend Policy
  - InBound Policy
    - Add rate-limit Policy (Use Snippet)
  - Backend Policy
  - Output Policy
  - Onerror Policy

## API Management Policy - Cache

- Choose (**Order API**)

- Policy XML
  - Frontend Policy
  - InBound Policy
    - Add cache-lookup Policy (Use Snippet)
      - Caching-Type : Internal
      - Downstread-caching-Type : None
      - Must-revalidate : True
  - Backend Policy
  - Output Policy
    - Add cache-store Policy (Use Snippet)
      - duration = 30
  - Onerror Policy

## APIM - Virtual Network Integration

UseCase : Backend Private API hosted on a VM within a virtual network

## How to setup Authentication in API Management

I’ll give you an end-to-end Azure APIM + Microsoft Entra ID setup, including what to configure in Entra, APIM policies, and how clients call it.

Thought for a few seconds

Yes — implement OAuth2/JWT in Azure API Management like this.

Target flow
Client app → gets OAuth2 token from Microsoft Entra ID
Client app → calls APIM with Authorization: Bearer <token>
APIM → validates JWT
APIM → forwards request to backend only if token is valid

APIM does not issue OAuth tokens. It validates tokens issued by Microsoft Entra ID or another identity provider. Microsoft recommends validate-azure-ad-token for Entra tokens, or validate-jwt for generic identity providers.

Step 1: Register backend API in Microsoft Entra ID

Go to:

Microsoft Entra ID → App registrations → New registration

Create one app registration for your API, for example:

Name: my-private-api
Supported account type: Single tenant

Then configure:

Expose an API → Set Application ID URI

Example:

api://my-private-api

Add a scope:

Scope name: access_as_user
Who can consent: Admins and users

Final scope will look like:

api://my-private-api/access_as_user
Step 2: Register client application

Create another app registration for the caller:

Name: my-client-app

Then add API permission:

API permissions → Add permission → My APIs → my-private-api

Select:

access_as_user

Grant admin consent if required.

For service-to-service flow, create a client secret or certificate for this client app.

Step 3: Get a JWT access token

For client credentials flow:

curl -X POST "https://login.microsoftonline.com/<tenant-id>/oauth2/v2.0/token" \
 -H "Content-Type: application/x-www-form-urlencoded" \
 -d "client_id=<client-app-id>" \
 -d "client_secret=<client-secret>" \
 -d "scope=api://my-private-api/.default" \
 -d "grant_type=client_credentials"

This returns:

{
"access_token": "eyJ...",
"token_type": "Bearer",
"expires_in": 3599
}

Microsoft notes that access tokens should be validated by the resource server/API, not by the client application.

Step 4: Add APIM inbound JWT validation policy

In Azure Portal:

API Management → APIs → Select API → Design → All operations → Inbound processing → Code editor

Add:

<policies>
  <inbound>
    <base />

    <validate-azure-ad-token tenant-id="<tenant-id>">
      <audiences>
        <audience>api://my-private-api</audience>
      </audiences>
      <client-application-ids>
        <application-id><client-app-id></application-id>
      </client-application-ids>
      <required-claims>
        <claim name="roles" match="any">
          <value>Api.Read</value>
        </claim>
      </required-claims>
    </validate-azure-ad-token>

  </inbound>

  <backend>
    <base />
  </backend>

  <outbound>
    <base />
  </outbound>

  <on-error>
    <base />
  </on-error>
</policies>

Minimum version:

<validate-azure-ad-token tenant-id="<tenant-id>">
  <audiences>
    <audience>api://my-private-api</audience>
  </audiences>
</validate-azure-ad-token>
Step 5: Call APIM with token
curl https://<apim-name>.azure-api.net/my-api/orders \
  -H "Authorization: Bearer <access-token>" \
  -H "Ocp-Apim-Subscription-Key: <subscription-key>"

Subscription key is optional depending on your APIM product settings. It is useful for API access control, but OAuth/JWT is the real authentication layer.

Step 6: Secure APIM to backend

Best practice: APIM validates the caller token, then APIM authenticates to backend separately using Managed Identity. Microsoft’s APIM managed identity policy obtains an Entra token and sets it as a Bearer token to the backend.

<authentication-managed-identity resource="api://<backend-app-id>" />
Recommended production setup
OAuth2/JWT validation at APIM
+ Managed Identity from APIM to backend
+ Private endpoint / internal VNet
+ HTTPS only
+ Rate limit policy
+ Required claims/roles
+ Optional subscription key

This is the clean enterprise pattern.
