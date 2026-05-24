# ---------- Build stage ----------
FROM node:22-alpine3.22 AS build

WORKDIR /app

COPY package*.json ./

# Deletes existing node_modules and package-lock.json to ensure a clean slate for npm ci
# Uses package-lock.json exactly and fails if the package-lock.json and package.json are out of sync
RUN npm ci

COPY . .
RUN npm run build

# ---------- Runtime stage ----------
FROM nginx:1.29-alpine3.22

ARG NGINX_CONF=nginx.local.conf

COPY ${NGINX_CONF} /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]