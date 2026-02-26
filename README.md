# ShopNow — Full-Stack E-Commerce App

A full-stack e-commerce application built with **Next.js 14** (frontend) and **ASP.NET Core 8** (backend).

## Run Locally

### 1. Configure the database

Update `backend/ECommerceApp.API/appsettings.json`:
```json
"DefaultConnection": "Server=localhost;Port=3306;Database=e-commerce;User=root;Password=YOUR_PASSWORD;"
```

### 2. Run the backend
```bash
cd backend/ECommerceApp.API
dotnet run
```
API → `http://localhost:5110`  
Swagger → `http://localhost:5110/swagger`

### 3. Run the frontend
```bash
cd frontend
npm install
npm run dev
```
App → `http://localhost:3000`

---

## Default Admin Account

| Email | Password |
|---|---|
| admin@ecommerce.com | Admin@123 |

---



## Docker

```bash
docker-compose up -d
```
