npm create vite@latest my-app

## 🛠️ Setup

Run locally

```bash
npm install
npm run dev
```

```
| Tool    | Role                | Default Port         |
| ------- | ------------------- | -------------------- |
| Vite    | Frontend dev server | 5173                 |
| Express | Backend API server  | 3000 (common choice) |
```

In real apps:

```
Vite → runs frontend (React/Vue UI)
Express → runs backend API
They communicate via HTTP:
Vite (5173)  --->  Express (3000)
```

```
| Tool    | What it does                        | Used for |
| ------- | ----------------------------------- | -------- |
| Vite    | Serves frontend + live reload (HMR) | Frontend |
| Nodemon | Restarts Node app on file changes   | Backend  |

If you're building a full-stack app, the combo is usually:

Vite → frontend
Express + Nodemon → backend
```
