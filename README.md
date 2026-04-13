# 🚀 All-In-All

Full-stack cloud-native application using:

- React (Frontend)
- FastAPI (Backend)
- AKS (Kubernetes)
- Terraform (Infrastructure)

---

## 📁 Project Structure

```bash
all-in-all/
│
├── frontend/ # React UI
│ ├── public/
│ ├── src/
│ │ ├── app/ # App entry, routing
│ │ ├── components/ # Reusable UI components
│ │ ├── features/ # Feature-based modules
│ │ ├── hooks/
│ │ ├── services/ # API calls
│ │ ├── store/ # Redux/Zustand
│ │ ├── utils/
│ │ └── styles/
│ ├── tests/
│ ├── package.json
│ └── Dockerfile
│
├── backend/ # Python API (FastAPI recommended)
│ ├── app/
│ │ ├── api/ # Route definitions
│ │ │ ├── v1/
│ │ │ └── dependencies/
│ │ ├── core/ # Config, security, settings
│ │ ├── models/ # DB models
│ │ ├── schemas/ # Pydantic schemas
│ │ ├── services/ # Business logic
│ │ ├── repositories/ # DB access layer
│ │ ├── workers/ # Background jobs
│ │ └── main.py
│ │
│ ├── tests/
│ ├── alembic/ # DB migrations
│ ├── requirements.txt / pyproject.toml
│ └── Dockerfile
│
├── infrastructure/ # Terraform (IaC)
│ ├── modules/ # Reusable modules
│ │ ├── aks/
│ │ ├── networking/
│ │ ├── database/
│ │ └── monitoring/
│ │
│ ├── environments/
│ │ ├── dev/
│ │ ├── staging/
│ │ └── prod/
│ │ ├── main.tf
│ │ ├── variables.tf
│ │ └── outputs.tf
│ │
│ └── global/
│ ├── backend.tf # remote state config
│ └── providers.tf
│
├── k8s/ # Kubernetes manifests (AKS)
│ ├── base/
│ │ ├── frontend-deployment.yaml
│ │ ├── backend-deployment.yaml
│ │ ├── service.yaml
│ │ └── ingress.yaml
│ │
│ ├── overlays/ # Kustomize or Helm
│ │ ├── dev/
│ │ ├── staging/
│ │ └── prod/
│
├── docker/ # Optional central Docker configs
│ ├── frontend/
│ └── backend/
│
├── scripts/ # Automation scripts
│ ├── deploy.sh
│ ├── migrate.sh
│ └── seed.sh
│
├── .github/ or .azuredevops/ # CI/CD pipelines
│ ├── workflows/
│ │ ├── frontend.yml
│ │ ├── backend.yml
│ │ ├── terraform.yml
│ │ └── deploy.yml
│
├── docs/ # Architecture & ADRs
│ ├── architecture.md
│ ├── decisions/
│ └── runbooks/
│
├── .env.example
├── README.md
└── Makefile
```

---

## 🛠️ Setup

Run locally

Backend

```bash
cd backend
pip install -r requirements.txt
uvicorn app.main:app --reload
```

Frontend

```bash
cd frontend
npm install
npm run dev
```

🐳 Docker

```bash
docker build -t backend ./backend

docker build -t frontend ./frontend
```

☸️ Deploy to local

```bash
docker run -p 3000:80 frontend
```

☸️ Deploy to AKS

```bash
helm upgrade --install my-app ./helm/my-app
```

🔐 Environment Variables

```bash
Create .env files for:

Backend
Frontend
```
